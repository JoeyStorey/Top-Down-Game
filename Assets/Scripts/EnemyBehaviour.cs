using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    public Animator animator;
    private Transform playerLocation;
    private float distanceToPlayer;

    //Used for when a unit patrols between its starting position and a given position
    private Vector3 startPosition;
    [SerializeField] private Vector3 patrolPosition;
    private Vector3 currentDestination;

    [SerializeField] private float enemySightRadius;
    [SerializeField] private float enemyAttackRadius;
    private bool isAttacking;
    private float turnVelocity;

    public GameObject projectile;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    public Transform projectileSpawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        startPosition = navMeshAgent.transform.position;
        currentDestination = patrolPosition;

        playerLocation = GameObject.Find("Player").transform;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the player is outside the enemy's sight radius
        distanceToPlayer = Vector3.Distance(navMeshAgent.transform.position, playerLocation.position);
        if (distanceToPlayer > enemySightRadius) //Player is outside the detection radius
        {
            Patrol();
        }
        else if (distanceToPlayer < enemySightRadius && distanceToPlayer > enemyAttackRadius) //Player is inside detection range but not close enough to attack
        {
            PursuePlayer();
        }
        else if (distanceToPlayer < enemyAttackRadius) //Player is in attack range
        {
            transform.LookAt(playerLocation);
            StartCoroutine(Attack());
        }

        if (isAttacking == true)
        {
            animator.SetBool("isMoving", false);
        }
        else {
            animator.SetBool("isMoving", true);
        }
    }

    void Patrol()
    {
        navMeshAgent.SetDestination(currentDestination);

        //Slightly altered the code from the player movement script for smooth turning and rotation
        float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
        float turningAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, 20f);
        transform.rotation = Quaternion.Euler(0f, turningAngle, 0f);
        
        //Checks if the enemy has reached their desired patrol point.
        if (Vector3.Distance(currentDestination, transform.position) < 1)
        {
            if (currentDestination == patrolPosition)
            {
                currentDestination = startPosition;
            }
            else
            {
                currentDestination = patrolPosition;
            }
        }
    }

    void PursuePlayer()
    {
        if (isAttacking == false)
        {
            //Points the enemy towards the player and approaches them
            navMeshAgent.SetDestination(playerLocation.position);
            //Again, reusing the code used to rotate the player smoothly
            float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;
            float turningAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref turnVelocity, 40f);
            transform.rotation = Quaternion.Euler(0f, turningAngle, 0f);
        } else 
        {
            //Mid-attack, snaps aim at the character instead of smooth aiming to make sure they aim at the player
            transform.LookAt(playerLocation);
        }
        
        
    }

    IEnumerator Attack()
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            animator.SetBool("draw", true);
            //Stops moving to prepare to attack
            navMeshAgent.SetDestination(transform.position);
            yield return new WaitForSecondsRealtime(1f);

            animator.SetBool("aim", true);
            yield return new WaitForSecondsRealtime(0.5f);

            //Creates an arrow, applies force to it in the direction the enemy is looking.
            GameObject shotProjectile = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
            Rigidbody projectileRB = shotProjectile.GetComponent<Rigidbody>();
            projectileRB.AddForce(transform.forward * projectileSpeed);
            animator.SetBool("fire", true);
            yield return new WaitForSecondsRealtime(0.5f);

            //Resets all the animation booleans and allows the unit to attack again
            animator.SetBool("draw", false);
            animator.SetBool("aim", false);
            animator.SetBool("fire", false);
            isAttacking = false;
        }
    }

}
