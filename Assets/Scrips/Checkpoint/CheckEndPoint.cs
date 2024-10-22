using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEndPoint : MonoBehaviour
{
    private Animator _animator => GetComponent<Animator>();
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null )
        {
            AudioManager.instance.PlaySFX(2);
            _animator.SetTrigger("setTriger");
             GameManger.Instance.LevelFinish();
        }    
    }
}
