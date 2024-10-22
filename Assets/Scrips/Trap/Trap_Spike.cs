using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Spike : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float pushForce;
    void Start()
    {
        Vector2 pushVector = new Vector2(pushForce, 0);
        rb.AddForce(pushVector,ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
