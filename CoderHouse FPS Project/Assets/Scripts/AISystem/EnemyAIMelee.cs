using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIMelee : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, WhatIsPlayer;

    Animator _animator;

    // Enemy Stats
   // [SerializeField] private float health = 100;
    private bool isEnemyDead;

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



    // bool to set trigger for animation
    public bool isAttacking;

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

        gameObject.GetComponent<Target>();
        if (!Target.isEnemyDead == true)
        {
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
    // Attack method to player
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        var playerLifeController = collision.gameObject.GetComponent<PlayerLifeController>();
        if (playerLifeController != null)
            playerLifeController.TakeDamage(damage);


    }
    // Trigger to set death animation
  /*  public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }

    }
    private void Die()
    {
        isEnemyDead = true;
        _animator.SetTrigger("dead");
        Destroy(gameObject, 5f);
        return;
    }*/
}

