using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossBehaviour : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    public Animator animator;
    private Transform playerLocation;
    private float distanceToPlayer;
    private float turnVelocity;
    [SerializeField] private float enemySightRadius;

    //This is essentially a stripped down version of the regular enemy AI, simply chasing the player if they are in range.

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        playerLocation = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(navMeshAgent.transform.position, playerLocation.position);
        if (distanceToPlayer < enemySightRadius) //Player is within detection range
        {
            animator.SetBool("isChasing", true);
            Chase();
        }
        
    }

    void Chase()
    {
        //Points the enemy towards the player and approaches them
        navMeshAgent.SetDestination(playerLocation.position);
        //Again, reusing the code used to rotate the player smoothly
        float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
        float turningAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, 40f);
        transform.rotation = Quaternion.Euler(0f, turningAngle, 0f);
    }
}
