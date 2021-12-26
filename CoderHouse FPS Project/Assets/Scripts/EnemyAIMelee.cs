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


    //Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float damage = 10;

    // States
    public float sightRange, attackRange, animAttackRange;
    public bool playerInSightRange, playerInAttackRange;

    Animator _animator;

    // bool to set trigger for animation
    public bool isAttacking;
    public bool isDead
    {
        get
        {
            return enemyHealth == 0;
        }
    }


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


// Check for sight and Attack Range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        if (!isDead)
        {
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
                AttackPlayer();
                Debug.Log("Atacking Player");
                _animator.SetTrigger("attack");
                return;

            }

            if (enemyHealth < 1)
            {
                Death();
            }
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



        if (!alreadyAttacked)
        {
            //AttackCodehere

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        if (isAttacking)
        {
            _animator.SetTrigger("attack");
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

    private void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        if(enemyHealth < 0)
        {
            enemyHealth = 0;
        }

        if(isDead)
        {
            _animator.SetTrigger("death");
        }

    }

    private void Death()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject, 5f);
            return;
        }
        

    }
}
