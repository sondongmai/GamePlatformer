using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public static UI_MainMenu instance;
    // Start is called before the first frame update
    [SerializeField] private string scene;
    [SerializeField] private GameObject []uiElements;
    [SerializeField] private GameObject continuosButton;

    public UI_FadeEfect fadeEfect;

    private void Awake()
    {
        instance = this;
        fadeEfect = GetComponentInChildren<UI_FadeEfect>();  
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Screen.SetResolution(Screen.width, Screen.height, FullScreenMode.FullScreenWindow, new RefreshRate() { numerator = 60,denominator =1 }); 

    }
   public void LoadScene1()
    {
        fadeEfect.SceenFade(1, 1.5f, LoadScene);
    }    
    public void Start()
    {
        fadeEfect.SceenFade(0, 1.5f);
        if (HasLevelProgession())
        {
            continuosButton.SetActive(true);
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
        AudioManager.instance.PlaySFX(4);
    }    
    public void switchUI(GameObject uitoEnale) 
    {
        foreach (GameObject ui in uiElements) 
        {
            ui.SetActive(false);// an di cai hien tai di 
        }
        uitoEnale.SetActive(true);// bat cai minh kich hoat di

        AudioManager.instance.PlaySFX(4);
    }
    private bool HasLevelProgession()
    {
        bool hasLevelProgession = PlayerPrefs.GetInt("NextLevel", 0) > 0;
        return hasLevelProgession;
    }
    public void LoadContinueScene()
    {
        fadeEfect.SceenFade(1, 1.5f, LoadSceneContinue);
    }
    public void LoadSceneContinue()
    {
        int difficultyIndex = PlayerPrefs.GetInt("DifficultyIndex",1);
        DificultyManager.instance.LoadDifficulty(difficultyIndex);
        int loadToScene = PlayerPrefs.GetInt("NextLevel", 0);
        SceneManager.LoadScene(loadToScene);
        AudioManager.instance.PlaySFX(4);
    }
    
}
