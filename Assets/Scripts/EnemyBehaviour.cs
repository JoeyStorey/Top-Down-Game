using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform playerLocation;
    private float distanceToPlayer;

    public float enemySightRadius;
    public float enemyAttackRadius;
    private bool isAttacking;
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerLocation = GameObject.Find("Player").transform;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the player is outside the enemy's sight radius
        distanceToPlayer = Vector3.Distance(navMeshAgent.transform.position, playerLocation.position);
        if (distanceToPlayer > enemySightRadius)
        {
            Patrol();
        }
        else if (distanceToPlayer < enemySightRadius && distanceToPlayer > enemyAttackRadius)
        {
            PursuePlayer();
        }
        else if (distanceToPlayer < enemyAttackRadius)
        {
            //Attack
        }
    }

    void Patrol()
    {

    }

    void PursuePlayer()
    {
        //Points the enemy towards the player and approaches them
        transform.LookAt(playerLocation.position);
        navMeshAgent.SetDestination(playerLocation.position);
    }

    void Attack()
    {

    }
}
