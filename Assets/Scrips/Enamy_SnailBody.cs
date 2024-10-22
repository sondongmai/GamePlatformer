using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy_SnailBody : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private float zRotation;
    private SpriteRenderer sp;

    public void setUpBody(float Yveloctiy,float zRotation, float fancing)
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        rb.velocity = Vector3.zero;
        rb.velocity = new Vector2 (rb.velocity.x, Yveloctiy);
        this.zRotation = zRotation;

        if (fancing == 1) 
        {
            sp.flipX = true;
        }
    }
    public void Update()
    {
        transform.Rotate (0,0,zRotation*Time.deltaTime);
       
    }
}
