using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy_Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private string playLayerName = "Player";
    private string groundLayerName = "Ground";
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void setVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(playLayerName))
        {
            collision.GetComponent<Player>().KnockBack(transform.position.x);
        }    
        if(collision.gameObject.layer == LayerMask.NameToLayer(groundLayerName))
        {
            Destroy(gameObject,1.5f);
        }    
    }
}
