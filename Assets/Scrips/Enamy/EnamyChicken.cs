using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyChicken : Enamy
{
    // Start is called before the first frame update

    [Header("Enamy Chicken Detail")]
    [SerializeField] private float agrroDuration; // thời gian hung hăng
    private float agrooTimer;
 
    [SerializeField] private float detectionRange;
    private bool canFlip = true;
    

    protected override void Update()
    {
        base.Update();
       
        if (isDeed)
            return;
        agrooTimer -= Time.deltaTime;

        if(isDetectPlayer) 
        {
            canMove = true;
            agrooTimer = agrroDuration;
        }

        HandelConllision();
        if(agrooTimer < 0) 
        {
            canMove = false;
        }
        HandelMovement();


        if (isGrounded)
        {
            HandelTurnAround();
        }
       
    }

    private void HandelTurnAround()
    {
        if (!isGroundFrontDectedd || isWallDectected)
        {
            Flip();
            canMove = false;
            rb.velocity = Vector2.zero;
        }
    }

    private void HandelMovement()
    {
        if (canMove == false)
            return;

        float xV = player.transform.position.x;
        if (xV < transform.position.x && fancingRight || xV > transform.position.x && !fancingRight)
        {
            if (canFlip)
            {
                canFlip = false;
                Invoke(nameof(Flip), .3f);

            }
        } 
       
        rb.velocity = new Vector2(moveSpeed * fancingDir, rb.velocity.y);
    }
    protected override void Flip()
    {
        base.Flip();
        canFlip = true;
    }
   
   
}
