using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private Animator _animator => GetComponent<Animator>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null )
        {
            _animator.SetTrigger("setTriger");
        }    
    }
}

  
