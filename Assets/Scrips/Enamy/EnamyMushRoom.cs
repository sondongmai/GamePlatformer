using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyMushRoom : Enamy
{
    
   
    protected override void Update()
    {
        base.Update();

        if (isDeed)
            return;
        
     
        HandelMovement();


        if(isGrounded)
        { 
           HandelTurnAround(); 
        }    
        
    }

    private void HandelTurnAround()
    {
        if (!isGroundFrontDectedd || isWallDectected)
        {
            Flip();
            idelTimer = idelDuration;
            rb.velocity = Vector2.zero;
        }
    }

    private void HandelMovement()
    {
        if (idelTimer > 0)
            return;
        

        rb.velocity = new Vector2(moveSpeed*fancingDir,rb.velocity.y); 
    }
  
}
