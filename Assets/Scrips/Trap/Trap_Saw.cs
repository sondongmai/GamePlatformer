using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class Trap_Saw : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform[] waypoit;
    [SerializeField] private int waypointIndex = 1;

    public bool canMove = true;
    public float timeToDelayMove;
    public int moveDirection = 1;

    private Vector3[] wayPointPosition; 
    private Animator animator;
    private SpriteRenderer sr;
    void Start()
    {
        UpdatePointInfor();
        transform.position = wayPointPosition[0];
    }

    private void UpdatePointInfor()
    {
       
        List<TrapSaw_Waypoint> list_TrapSaw_Waypoints = new List<TrapSaw_Waypoint>(GetComponentsInChildren<TrapSaw_Waypoint>());

        if(list_TrapSaw_Waypoints.Count != waypoit.Length)
        {
            waypoit = new Transform[list_TrapSaw_Waypoints.Count];
            for(int i = 0; i < waypoit.Length; i++)
            {
                waypoit[i] = list_TrapSaw_Waypoints[i].transform;
            }
        }
        wayPointPosition = new Vector3[waypoit.Length];
        for (int i = 0; i < waypoit.Length; i++)
        {
            wayPointPosition[i] = waypoit[i].position;
        }
    }

    private void Awake()
    {   
       animator = GetComponent<Animator>();
       sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("active", canMove);
        if (canMove == false)
            return;
  
        transform.position = Vector3.MoveTowards(transform.position, wayPointPosition[waypointIndex],moveSpeed*Time.deltaTime);
    
        if (Vector2.Distance(transform.position, wayPointPosition[waypointIndex])<0.1f)
        {
            if (waypointIndex == waypoit.Length - 1 || waypointIndex == 0)
            {
                moveDirection *= -1;
                StartCoroutine(StopMovement(timeToDelayMove));
            }
            waypointIndex = waypointIndex + moveDirection;
        }    
    }
    private IEnumerator StopMovement(float delay)
    {
        canMove = false;
        yield return new WaitForSeconds(delay);
        canMove = true;
        sr.flipX = !sr.flipX;
    }
}
