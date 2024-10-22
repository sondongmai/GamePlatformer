using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DifficultyType { Easy = 1, Nomal, Hard}
public class DificultyManager : MonoBehaviour
{
    public static DificultyManager instance;
    public DifficultyType difficulty;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance == null)
        {
            instance = this;
        }    
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetDifficultType(DifficultyType newdifficultyType)
    {
        difficulty = newdifficultyType;
    }   
    public void LoadDifficulty(int difficultyindex)
    {
        difficulty = (DifficultyType)difficultyindex; 
    }    


}

