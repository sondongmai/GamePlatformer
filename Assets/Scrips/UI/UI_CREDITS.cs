using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_CREDITS : MonoBehaviour
{
    [SerializeField] private RectTransform reacT;
    [SerializeField] private float Mspeed = 200f;
    [SerializeField] private float offSceen = 1000f;
    public void Update()
    {
        reacT.anchoredPosition += Vector2.up*Mspeed*Time.deltaTime;
        if(reacT.anchoredPosition.y > offSceen) 
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
