using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public void finishRespawn()
    {
        player.reSpawnFinished(true);
    }    
}


    
