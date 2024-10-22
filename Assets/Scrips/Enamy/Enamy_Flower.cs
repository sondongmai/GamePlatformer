using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enamy_Flower : Enamy
{
    // Start is called before the first frame update
    [Header("Plant detail")]
    [SerializeField] private float lastTimeAttack;
    [SerializeField] private float attackCoolDown = 1.5f;
    [SerializeField] private float speedEnamyBullet = 7f;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Enamy_Bullet butletPrefab;
    

    
    protected override void Update()
    {
        base.Update();

       bool canAttack = Time.time > lastTimeAttack+attackCoolDown;
        if(isDetectPlayer && canAttack) 
        {
            Attack();
        }
    }

    protected void Attack()
    {
        lastTimeAttack = Time.time;
        animator.SetTrigger("attack");
    }

    protected void CreateBullet()
    {
        Enamy_Bullet bullet = Instantiate(butletPrefab, gunPoint.position, Quaternion.identity);
        Vector2 velocityOFbuttlet = new Vector2(speedEnamyBullet*fancingDir,transform.position.y);
        bullet.setVelocity(velocityOFbuttlet);
        Destroy(bullet.gameObject,10);
    }    
    protected override void HandelAnimator()
    {
        
    }
}
