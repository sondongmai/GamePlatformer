using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim => GetComponent<Animator>();

    private bool active;
    public bool canbeReactivated;

    private void Start()
    {
        canbeReactivated = GameManger.Instance.canReactivate;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active && canbeReactivated == false)
            return;

        Player player = collision.GetComponent<Player>();
        if (player != null) 
        {
            ActiveCheckPoint();
        }
    }

    private void ActiveCheckPoint()
    {
        active = true;
        //anim.SetBool("activate", active);
        anim.SetTrigger("activate");
        PlayerManager.instance.UpdateRespawnPosition(transform);
    }
}
