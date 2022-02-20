using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossDrone : MonoBehaviour
{
    public EnemyStatus enemyStatus;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnBullet;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, WhatIsPlayer;

    Animator _animator;

    private float currentHealth;


    private bool isEnemyDead;

    // Patrolling
    public Vector3 walkPoint;
    bool walkpointSet;
    public float walkPointRange;


    //Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public float damage;

    // States
    public float sightRange, attackRange, animAttackRange;
    public bool playerInSightRange, playerInAttackRange;



    // bool to set trigger for animation
    public bool isAttacking;

    // Serialized Inputs for floater
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    // Drone Shader
    public int x;
    Renderer rend;
    public float dissolveSpeed = 1f;
    private float maxValue = 1;
    private bool dissolved;


    private void Awake()
    {


        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }


    // Start is called before the first frame update
    void Start()
    {
        // Dissolve properties
        maxValue = 1;
        dissolved = true;
        x = 0;
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        _animator = GetComponent<Animator>();
        damage = enemyStatus.damage;
        currentHealth = GetComponent<Target>().health;

    }

    // Update is called once per frame
    void Update()
    {
        if (dissolved == true)
        {
            maxValue -= dissolveSpeed;
            rend.sharedMaterial.SetFloat("Dissolve", maxValue);

            if (maxValue <= 0.0f)
            {
                maxValue = 0f;
            }
        }


        Target locomotion = gameObject.GetComponent<Target>();
        if (locomotion.isEnemyDead == false)
        {

            // Check for sight and Attack Range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

            gameObject.GetComponent<Target>();
            {
                {
                    if (!playerInSightRange && !playerInAttackRange)
                    {

                        Patrolling();
                        //Debug.Log("Patrolling");


                    }

                    if (playerInSightRange && !playerInAttackRange)
                    {

                        ChasePlayer();
                        // Debug.Log("Chasing Player");


                    }

                    if (playerInSightRange && playerInAttackRange)

                    {
                        AttackPlayer();
                        // Debug.Log("Atacking Player");


                    }
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
            agent.SetDestination(walkPoint) ;
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
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(projectile, spawnBullet.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(-transform.up * 4f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Dissolve()
    {
        dissolved = false;
    }

}

