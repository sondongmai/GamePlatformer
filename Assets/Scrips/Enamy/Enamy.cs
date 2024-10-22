using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class Enamy : MonoBehaviour
{ 
    
    protected Animator animator;
    protected Rigidbody2D rb;
    protected Collider2D[] cd;
    protected Transform player;

    [SerializeField] protected float moveSpeed;

    [Space]
    protected bool canMove = true;
    [SerializeField] protected float idelDuration;
    [SerializeField] protected float idelTimer;


    [Header("Basic Conllition")]
    [SerializeField] protected float groundCheckDistance = 1.1f;
    [SerializeField] protected float wallCheckDistance = .7f;
    [SerializeField] protected float playerDetectDistance;
    
    [SerializeField] protected LayerMask whatisGround;
    [SerializeField] protected LayerMask whatIsPlayer;
    [SerializeField] protected Transform groundCheck;
    

    [Header("DeathDetail")]
    [SerializeField] protected float deathImpact;
    [SerializeField] protected float deathRotationSpeed;
    [SerializeField] protected bool isDeed;
    protected float deathRotationDirection = 1;

    protected bool isWallDectected;
    protected bool isGroundFrontDectedd;
    protected bool isGrounded;
    protected bool isDetectPlayer;

    protected float fancingDir = -1;
    protected bool fancingRight = false;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponentsInChildren<Collider2D>();
    }
    protected virtual void Start()
    {
        //InvokeRepeating(nameof(UpdatepPlayerRef), 0, 1);
    }    
    protected virtual void UpdatepPlayerRef()
    {
        if(player == null) 
        {
            player = PlayerManager.instance.player.transform;
        }

    }    
    protected virtual void Update()
    {
        HandelConllision();
        HandelAnimator();
        
        idelTimer -= Time.deltaTime;
        if(isDeed)
        {
            HandelDeath();
        }    
    }

    protected virtual void HandleFlip(float xValue)
    {
        if (xValue < transform.position.x && fancingRight || xValue > transform.position.x && !fancingRight)
        {
            Flip();
        }
    }
    protected virtual void Flip()
    {
        fancingDir = fancingDir * -1;
        transform.Rotate(0, 180, 0);
        fancingRight = !fancingRight;
    }
    public virtual void Die()
    {
        animator.SetTrigger("hit");
        foreach(var collider in cd)
        {
            collider.enabled = false;
        }
      
        rb.velocity = new Vector2(rb.velocity.x, deathImpact);
        isDeed = true;
        if(Random.Range(0f, 100f)< 50)
        {
            deathRotationDirection = deathRotationDirection * -1;
        }    
    }    
    private void HandelDeath()
    {
        transform.Rotate(0,0,(deathRotationDirection*deathRotationSpeed)*Time.deltaTime);
    }    
    protected virtual void HandelAnimator()
    {
        animator.SetFloat("xVelocity", rb.velocity.x);
    }

    protected virtual void HandelConllision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatisGround);
        isGroundFrontDectedd = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
        isWallDectected = Physics2D.Raycast(transform.position, Vector2.right * fancingDir, wallCheckDistance, whatisGround);
        isDetectPlayer = Physics2D.Raycast(transform.position, transform.right * fancingDir, playerDetectDistance,whatIsPlayer); ;
    }
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + wallCheckDistance * fancingDir, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + playerDetectDistance * fancingDir, transform.position.y));

    }

}
