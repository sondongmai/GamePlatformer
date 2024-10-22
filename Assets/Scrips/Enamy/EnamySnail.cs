using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamySnail : Enamy
{
    [Header("Enamy Snail Details")]
    public bool hasBody = true;
    [SerializeField] private Enamy_SnailBody bodyPreFab;
    [SerializeField] private float maxSpeed = 10f;
    protected override void Update()
    {
        base.Update();

        if (isDeed)
            return;


        HandelMovement();


        if (isGrounded)
        {
            HandelTurnAround();
        }

    }

    private void HandelTurnAround()
    {
        if (!isGroundFrontDectedd && hasBody || isWallDectected)
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

        if (canMove == false)
            return;

        rb.velocity = new Vector2(moveSpeed * fancingDir, rb.velocity.y);
    }
    public override void Die()
    {

        if (hasBody)
        {
            canMove = false;
            hasBody = false;
            animator.SetTrigger("hit");
            rb.velocity = Vector2.zero;
            idelDuration = 0;
        }
        else if (canMove == false && hasBody == false)
        {
            animator.SetTrigger("hit");
            canMove = true;
            moveSpeed = maxSpeed;
        }
        else
        {
            base.Die();
        }
        
    }
    public void CreateBody()
    {
        Enamy_SnailBody newBody = Instantiate(bodyPreFab,transform.position, Quaternion.identity);
        if (Random.Range(0, 100) < 50)
        {
            deathRotationDirection = deathRotationDirection * -1;
        }
        newBody.setUpBody(deathImpact, deathRotationDirection * deathRotationSpeed,fancingDir);
        Destroy(newBody.gameObject,10);
    }
    protected override void Flip()
    {
        base.Flip();
        if (hasBody==false)
        {
            animator.SetTrigger("wallHit");

        }
    }

}
