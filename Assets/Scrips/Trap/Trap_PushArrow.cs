using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_PushArrow : Trap_trampoline
{
    [Header("Additional infor")]
    [SerializeField] private bool rotationRight;
    [SerializeField] private float rotationSpeed = 120;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float scaleUpTarget;
    [SerializeField] private Vector3 targetScale;
    private int direction = -1;

    private void Start()
    {
        transform.localScale = new Vector3(.3f, .3f, .3f);
    }

    private void Update()
    {
        if(transform.localScale.x <= targetScale.x)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scaleUpTarget * Time.deltaTime);
        }    
        HandelRotation();
    }

    private void HandelRotation()
    {
        direction = rotationRight ? -1 : 1;
        transform.Rotate(0, 0, (rotationSpeed * direction) * Time.deltaTime);
    }

    private void Destroy()
    {
        GameObject arrowPrefab = CreateObject.instance.ArrowPrefab;
        CreateObject.instance.CreateObeject(arrowPrefab, transform, delay);
        
        Destroy(gameObject);
    }




}
