using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMelee : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, WhatIsPlayer;

    // Enemy Stats
    public int enemyHealth = 100;

    // Patrolling
    public Vector3 walkPoint;
    bool walkpointSet;
    public float walkPointRange;
    bool isPatrolling;


    //Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    private bool attackPlayer = false;
    public int damage = 10;

    // States
    public float sightRange, attackRange, animAttackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check for sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {

            Patrolling();
            Debug.Log("Patrolling");
            return;

        } 
        
        if (playerInSightRange && !playerInAttackRange)
        {

            ChasePlayer();
            Debug.Log("Chasing Player");
            return;

        } 

        if (playerInSightRange && playerInAttackRange)
        {
            isPatrolling = false;
            attackPlayer = true;
            AttackPlayer();
            Debug.Log("Atacking Player");
            return;

        } 

        if (enemyHealth < 1)
        {
            Death();
        }
        
    }
    private void Patrolling()
    {
        if (!walkpointSet)
        {
            SearchWalkPoint();
        }

        if (walkpointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkpoint = transform.position - walkPoint;

        //Walkpoint Reached
        if (distanceToWalkpoint.magnitude < 1f)
        {
            walkpointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkpointSet = true;
        }
    }

    private void ChasePlayer()
    {

        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        GetComponent<Animator>().Play("thc4_arma|st_attack3");



        if (!alreadyAttacked)
        {
            //AttackCodehere

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()

        {
        alreadyAttacked = false;
        }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        var playerLifeController = collision.gameObject.GetComponent<PlayerLifeController>();
        if (playerLifeController != null)
            playerLifeController.GetDamage(damage);


    }

    private void Death()
    {
        GetComponent<Animator>().Play("thc4_arma|st_death");
        Destroy(gameObject);

    }
}
