using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();// 1 cách khác để kiểm tra va chạm
        if (player != null )
        {
            
            player.KnockBack(transform.position.x);
            player.Damage();
        }    
    }

}
