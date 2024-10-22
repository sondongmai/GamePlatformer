using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelButton : MonoBehaviour
{
    // Start is called before the first frame update
    private int levelIndex;
    [SerializeField] private TextMeshProUGUI levelNumberText;
    [SerializeField] private TextMeshProUGUI bestTimerText;
    [SerializeField] private TextMeshProUGUI fruits;
    public string sceneName;
    UI_MainMenu mainMenu;


    private void Start()
    {
        mainMenu = UI_MainMenu.instance;
    }
    public void LoadScene1()
    {
        mainMenu.fadeEfect.SceenFade(1, 0.75f, LoadLevel);
    }
    public void LoadLevel()
    {
        AudioManager.instance.PlaySFX(4);
        int diffycultIndex = ((int)DificultyManager.instance.difficulty);
        PlayerPrefs.SetInt("DifficultyIndex",diffycultIndex);
        SceneManager.LoadScene(sceneName);
    }  
    public void setUpButton(int newLevelIndex)
    {
        levelIndex = newLevelIndex;
        levelNumberText.text = "Level_" + levelIndex;
        sceneName = "Level_" + levelIndex;
        bestTimerText.text = BestTimer();
        fruits.text = FruitToCollect();
    }


    private string BestTimer()
    {
        float timeValue = PlayerPrefs.GetFloat("Level" + levelIndex + "BestTime",99);
        return "Best Time: " + timeValue.ToString("00") + "s";
    }

    private string FruitToCollect()
    {
        int fruitCollected = PlayerPrefs.GetInt("Level" + levelIndex + "FruitCollected");
        int totalFruitis = PlayerPrefs.GetInt("Level" + levelIndex + "totalFruitz");
        string intFruit = totalFruitis == 0 ? "?" : totalFruitis.ToString();
        return "Fruits: " + fruitCollected.ToString() + "/" + intFruit;

    }
}
