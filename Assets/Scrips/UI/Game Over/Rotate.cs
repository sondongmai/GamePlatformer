using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speedRotate = 50f;


    private void Update()
    {
       transform.Rotate(0,0,speedRotate*Time.deltaTime);
    }
}
