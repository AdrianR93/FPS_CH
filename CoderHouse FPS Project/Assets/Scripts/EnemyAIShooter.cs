using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIShooter : MonoBehaviour
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
    private bool attackPlayer = false;
    public float damage = 10;
    public GameObject projectile;
    public Transform spawnBullet;

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
            AttackPlayer();
            Debug.Log("Atacking Player");
            return;

        } 

        if (enemyHealth < 1)
        {
            Death();
        }

        OnDrawGizmosSelected();



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
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, spawnBullet.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

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
        Destroy(gameObject);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    public void GetDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log($"Enemy hit for {damage} hit points");

    }
}
