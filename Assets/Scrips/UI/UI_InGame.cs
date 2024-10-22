using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    [SerializeField] private TextMeshProUGUI ui_Timer;
    [SerializeField] private TextMeshProUGUI ui_Fruit;
    [SerializeField] private GameObject pauseUI;
    public UI_FadeEfect fadeEffect;
   
    

    private bool isPaused;
    private void Awake()
    {
        instance = this;
        fadeEffect = GetComponentInChildren<UI_FadeEfect>();
    }

    public void Start()
    {
        fadeEffect.SceenFade(0, 2f);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            PauseButton();
        }
    }
    public void UpdateFruit(int fruitcollect, int totalFruit)
    {
        ui_Fruit.text = fruitcollect + "/" + totalFruit;
    }

    public void UpdateTimer(float timer)
    {
        ui_Timer.text = timer.ToString("00")+"s";
    }
    public void PauseButton()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }    
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }    
    }
    public void GoToGameMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }    


}
