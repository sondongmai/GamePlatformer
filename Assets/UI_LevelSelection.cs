using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelSelection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UI_LevelButton buttonPrefab;
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private bool[] levelsUnlocked;
    
    private void Start()
    {
     
        LoadLevelInfor();
        CreateButton();
    }
    private void CreateButton()
    {
        int levelAmount = SceneManager.sceneCountInBuildSettings - 1;
        
        for (int i = 1; i < levelAmount; i++)
        {
            if (levelUnlocked(i) == false)
                return;

            UI_LevelButton levelButton = Instantiate(buttonPrefab, buttonsParent);
            levelButton.setUpButton(i);
        }
        
    }   
    private bool levelUnlocked(int levelIndex)
    {
        return levelsUnlocked[levelIndex];
    }    
    private void LoadLevelInfor()
    {
        int levelsAmout = SceneManager.sceneCountInBuildSettings - 1;

        levelsUnlocked = new bool[levelsAmout]; 
        for(int i = 1;i < levelsAmout; i++) 
        {
            bool levelUnlock = PlayerPrefs.GetInt("Level" + i + "Unlocked", 0) == 1;
            if(levelUnlock) 
            {
                levelsUnlocked[i] = true;
            }
        }
        levelsUnlocked[1] = true;
       
    }    

    



}
