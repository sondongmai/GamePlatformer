using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManger gameManger;
    private DifficultyType gameDificulty;


    private Rigidbody2D rb;
    private Animator animator;

    private bool canBeController = false;
    private CapsuleCollider2D cd;

    [Header("Movement")]
    public float JumpForce;
    public float moveSpeed;
    private float defaultGravityScale;
    public float doubleJumpForce;
    public bool canDoubleJump;

    [Header("Conllision")]    
    private bool isGrounded;
    private bool isWallDetect;
    private bool isAirBorne = false;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask layerMask;
    [Space]
    [SerializeField] private Transform enamyCheck;
    [SerializeField] private float enamyCheckRadius;
    [SerializeField] private LayerMask whatIsEnamy;


    [Header("Wall interactions")]
    [SerializeField] private float wallJumpDuration = .6f;
    [SerializeField] private Vector2 wallJumpForce;
    private bool isWallJumpping;

    [Header("Knock")]
    [SerializeField] private float knockbackDuration = 1f;
    [SerializeField] private Vector2 knockBackPower;
    private bool isKnocked;
    private bool canBeKnocked;


    [Header("Player Visuals")]
    [SerializeField] private GameObject vfx_PlayerDeath;
    [SerializeField] private AnimatorOverrideController[] animators;
    [SerializeField] private float SkinID;
    [SerializeField] ParticleSystem dustFX;

    private Joystick joystick;
    public float xInput;
    public float yInput;
    public bool facingRight = true;
    private float fancingdir = 1;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        cd = GetComponent<CapsuleCollider2D>();
        //
        FindFirstObjectByType<UI_JumpButton>().UpdatePlayerRef(this);
        //
        joystick = FindFirstObjectByType<Joystick>();
    }
    void Start()
    {
        gameManger = GameManger.Instance;
        UpdateGameDifficulty();

        defaultGravityScale = rb.gravityScale;
        reSpawnFinished(false);
        ChooseSkin();


    }
    public void Damage()
    {
        if(gameDificulty == DifficultyType.Nomal)
        {
            
            gameManger.FruitsCollect();
            if(gameManger.FruitsCollect()<=0)
            {
                die();
                gameManger.Restart();
            }
            else
            {
                gameManger.RemoveFruit();
            }    
            return;
        }
        if(gameDificulty == DifficultyType.Hard)
        {
            die();
            gameManger.Restart();
        }
    }
  
    // Update is called once per frame
    void Update()
    {
        UpdateAirbornStatus();
        if (canBeController == false)
        {
            HandelConllision();
            HandelAnimation();
            return;
        }
       
        if (isKnocked)
            return;
        HandelEnamyDetection();
        HandelInput();
        HandelSlide();
        HandelMovement();
        HandleFlip();
        HandelConllision();
        HandelAnimation();
    }
    private void UpdateGameDifficulty()
    {
        DificultyManager dificultyManager = DificultyManager.instance;
        if (dificultyManager != null)
        {
            gameDificulty = dificultyManager.difficulty;
        }
    }
    public void ChooseSkin()//cai de chuyen doi 
    {
        SkinManager skinManager = SkinManager.instance;
        if (skinManager == null)
        {
            return;
        }    
        animator.runtimeAnimatorController = animators[skinManager.skinid];
    }    
    private void HandelEnamyDetection()
    {
        if (rb.velocity.y >= 0)
            return;

        Collider2D []collider2D = Physics2D.OverlapCircleAll(enamyCheck.position, enamyCheckRadius, whatIsEnamy);
        foreach(var enamy in collider2D)
        {
            Enamy newEnamy = enamy.GetComponent<Enamy>();
            if(enamy != null)
            {
                AudioManager.instance.PlaySFX(1);

                newEnamy.Die();
                
                Jump();
               
            }    
        }    
    }

    public void reSpawnFinished(bool finish)
    {
        if(finish)
        {
            rb.gravityScale = defaultGravityScale;
            canBeController = true;
            cd.enabled = true;
            AudioManager.instance.PlaySFX(11);
        }  
        else
        {
            rb.gravityScale = 0;
            canBeController = false;
            cd.enabled = false;
        }    
    }    
    private void HandelSlide()
    {
        float yModifer = .5f;
        bool canWallSlide = rb.velocity.y < 0 && isWallDetect;
        
        if (canWallSlide == false)
            return;
          
        if(yInput<0)
        {
            yModifer = 1f;
        }
        else 
        {
            yModifer = 0.5f;
        }
        
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * yModifer);
    }
    private void UpdateAirbornStatus()
    {
        if (isGrounded && isAirBorne)// nghĩa là ban đầu không ở trên không thì nó không thực hiện câu lệnh này nên nó vẫn là false như giá trị ban đầu có thể bỏ mẹ điều kiện isAir kia 
        {
            HandleLanding();
        }
        if (!isAirBorne && !isGrounded)
        {
            BecomeAirBorne();
        }
    }

    private void BecomeAirBorne()
    {
        isAirBorne = true;
        //canDoubleJump = true;
    }

    private void HandleLanding()
    {
        dustFX.Play();
        isAirBorne = false;
        canDoubleJump = true;
    }

    private void HandelInput()
    {
        //xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");
        xInput = joystick.Horizontal;
        yInput = joystick.Vertical; 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }
    }
    public void JumpButton()
    {
        if (isGrounded) 
        {
            Jump();
        }
        else if(isWallDetect && !isGrounded)
        {
            WallJump();
        }    
        else if(canDoubleJump)
        {
            DoubleJump();
        }    
    }    
    private void Jump()
    {
        dustFX.Play();

        AudioManager.instance.PlaySFX(3);
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
    }
    private void DoubleJump()
    {
        dustFX.Play();

        AudioManager.instance.PlaySFX(3);
        isWallJumpping = false;
        canDoubleJump = false;
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
    }
    private void WallJump()
    {
        dustFX.Play();
        AudioManager.instance.PlaySFX(12);
        rb.velocity = new Vector2(wallJumpForce.x*-fancingdir, wallJumpForce.y);
        Flip();
        StopAllCoroutines();
        StartCoroutine(WallJumpRoutines());
    }   
    private IEnumerator WallJumpRoutines() 
    {
        isWallJumpping = true;
        yield return new WaitForSeconds(wallJumpDuration);
        isWallJumpping = false;
    }
    private IEnumerator KnockBackRoutines() 
    {
        canBeKnocked = false;
        isKnocked = true;
        animator.SetBool("KnockBack", true);
        yield return new WaitForSeconds(knockbackDuration);
        
        isKnocked = false;
        canBeKnocked = true;
        animator.SetBool("KnockBack", false);
    }

    public void KnockBack(float sorceDameXPostion)
    {
        float knockBackDir = 1;
        if(transform.position.x < sorceDameXPostion)
        {
            knockBackDir = -1;
        }    
        if(isKnocked)
            return;

        AudioManager.instance.PlaySFX(9);
        CameraManager.instance.ScreenShake(knockBackDir);
        rb.velocity = new Vector2(knockBackPower.x * knockBackDir, knockBackPower.y);
        StartCoroutine(KnockBackRoutines());
    }    

    void HandelMovement()
    {
        if (isWallDetect)
            return;
        if (isWallJumpping)
            return;

        rb.velocity = new Vector2 (xInput*moveSpeed, rb.velocity.y);
    }    

    void HandelAnimation()
    {
        //isRunning = rb.velocity.x != 0;// cách đặt biến mặc định vận tốc khác không là true ngược lại là false
        animator.SetFloat("xVelocity", rb.velocity.x);
        animator.SetFloat("yVelocity",rb.velocity.y);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isDetectWall", isWallDetect);
    }

    void HandelConllision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, layerMask);
        isWallDetect = Physics2D.Raycast(transform.position, Vector2.right*fancingdir, wallCheckDistance, layerMask);
    }

    void HandleFlip()
    {
        if(xInput<0 && facingRight || xInput > 0 && !facingRight)
        {
            Flip();
        }    
    }   
    void Flip()
    {
        fancingdir = fancingdir * -1;
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
    }  
    

    //VFX
    public void die()
    {
        AudioManager.instance.PlaySFX(0);
        Destroy(gameObject);
        //gameObject.SetActive(false);
        GameObject VFX_player = Instantiate(vfx_PlayerDeath, transform.position, Quaternion.identity);
        
    } 
    public void Push(Vector2 direction, float duration = 0)
    {
        StartCoroutine(PushCoroutines(direction, duration));
    }    
    IEnumerator PushCoroutines(Vector2 direction, float duration) 
    {
        canBeController = false;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction,ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        canBeController = true;

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(enamyCheck.position, enamyCheckRadius);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x,transform.position.y-groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x+wallCheckDistance*fancingdir, transform.position.y) );
    }
}
