using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public static GameManger Instance;
    UI_InGame uI_InGame;
 

    [Header("Player Management")]
    [SerializeField] private float levelTimer;
    [SerializeField] private int curentLevelIndex;
    private int nextLevelIndex;
    

    [Header("Fruit Manager")]
    public int fruitCollected;
    public bool haveRandomLook;
    public int totalFruit = 0;

    [Header("Check Point")]
    public bool canReactivate;
    // Start is called before the first frame update

    [Header("Partical")]
    [SerializeField] private ParticleSystem backFX;


    [Header("Manager")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private CreateObject createObject;
    [SerializeField] private DificultyManager DificultyManager;
    [SerializeField] private SkinManager skinManager;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        uI_InGame = UI_InGame.instance;
        curentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = curentLevelIndex + 1;
          

        CollectFruitInfor();
        CreateManagersIfNeed();

    }
    

    private void CreateManagersIfNeed()
    {
        if(AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }

        if (PlayerManager.instance == null)
        {
            Instantiate(playerManager);
        }
        if(CreateObject.instance == null)
        {
            Instantiate(createObject);
        }  
        if(DificultyManager.instance == null)
        {
            Instantiate(DificultyManager);
        }    
        if (SkinManager.instance == null)
        {
            Instantiate(skinManager);
        }    
    }    

    private void CollectFruitInfor()
    {
        Fruit[] allFruit = FindObjectsByType<Fruit>(FindObjectsSortMode.None);
        totalFruit = allFruit.Length;
        uI_InGame.UpdateFruit(fruitCollected, totalFruit);
        PlayerPrefs.SetInt("Level" + curentLevelIndex + "totalFruitz", totalFruit);
        

    }
    private void Update()
    {
        levelTimer += Time.deltaTime;
        uI_InGame.UpdateTimer(levelTimer);
        PlayBackFXPatical();
    }

    private void PlayBackFXPatical()
    {
        backFX.Play();
    }

    public void AddFruit()
    {
        fruitCollected+=1;
        uI_InGame.UpdateFruit(fruitCollected,totalFruit);
    }
    public void RemoveFruit()
    {
        fruitCollected--;
        uI_InGame.UpdateFruit(fruitCollected, totalFruit);
    }
    public int FruitsCollect() => fruitCollected;

    public bool HaveRandomLook() { return haveRandomLook; }

    public void LoadEndScene() => SceneManager.LoadScene("end");

    public void Restart()
    {
        uI_InGame.fadeEffect.SceenFade(1, 1.5f, LoadCurrentScene);
    }
    public void LoadCurrentScene()=> SceneManager.LoadScene("Level_" + curentLevelIndex);
    private void LoadLevel()
    {
        SceneManager.LoadScene("Level_"+nextLevelIndex);
    }
    
    public void LevelFinish()
    {
        SaveLevelProgession();
        SaveBestTime();
        SaveFruitInfor();
        uI_InGame.fadeEffect.SceenFade(1, 1.5f, LoadNextScene);
        
    }

    private void SaveLevelProgession()
    {
        PlayerPrefs.SetInt("Level" + nextLevelIndex + "Unlocked", 1);
        if (nomoLevel() == false)
        {
            PlayerPrefs.SetInt("NextLevel", nextLevelIndex);
        }
    }

    private void SaveBestTime()
    {
        float preveousTime = PlayerPrefs.GetFloat("Level" + curentLevelIndex + "BestTime", 99);
        if (levelTimer < preveousTime)
        {
            PlayerPrefs.SetFloat("Level" + curentLevelIndex + "BestTime", levelTimer);
        }
    }

    private void SaveFruitInfor()
    {
        int fruitCollectBefore = PlayerPrefs.GetInt("Level" + curentLevelIndex + "FruitCollected");

        if (fruitCollectBefore < fruitCollected)
        {
            PlayerPrefs.SetInt("Level" + curentLevelIndex + "FruitCollected", fruitCollected);
        }
        int totalFruitInBank = PlayerPrefs.GetInt("AmountFruitCollected");
        PlayerPrefs.SetInt("AmountFruitCollected", totalFruitInBank + fruitCollected);
    }    

    public void LoadNextScene()
    {
        if (nomoLevel())
        {
            LoadEndScene();
        }
        else
        {
            LoadLevel();
        }
    }
    public bool nomoLevel()
    {
        bool nomovLevel = curentLevelIndex + 2 == SceneManager.sceneCountInBuildSettings;
           return nomovLevel;
    }

}
