using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Difficultly : MonoBehaviour
{
    private DificultyManager dificultyManager;
    private void Start()
    {
        dificultyManager = DificultyManager.instance;
    }
    public void SetEasyMode()
    {
        dificultyManager.SetDifficultType(DifficultyType.Easy);
    }
    public void SetNomalMode()
    {
        dificultyManager.SetDifficultType(DifficultyType.Nomal);
    }
    public void SetHardMode()
    {
        dificultyManager.SetDifficultType(DifficultyType.Hard);
    }
}
