using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RenegadeBoss : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform spawnBullet;
    [SerializeField] private Transform _drones;
    [SerializeField] private GameObject _forceField;
    [SerializeField] private GameObject _gameOverscreen;
    public static bool endGame;
    private RenegadeStates _states;

    public EnemyStatus enemyStatus;

    public float currentHealth;
    public float maxHealth = 1000;
    public NavMeshAgent agent;
    private float healing = 10;
    private float healSpeed = 1;
    private bool alreadyDeployed;
    [HideInInspector] public static bool shieldHeal;
    public HealthBar healthBar;

    public Transform player;

    public LayerMask whatIsGround, WhatIsPlayer;

    Animator _animator;


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
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool pointsToAdd;



    // bool to set trigger for animation
    public bool isAttacking;

    //sounds
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip bossOver;

    // Dissolve Shader Variables
    private bool dissolved;
    public int x;
    Renderer rend;
    public float dissolveSpeed = 1f;
    private float maxValue;
    private bool isShaderUp;
    [SerializeField] private GameObject bodyShader;
    private float tpTimer;
    private float time;




    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }


    // Start is called before the first frame update
    void Start()
    {
        // Getting Components
        _animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Declaring and Starting Variables
        time = 0;
        tpTimer = 8;
        healthBar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        damage = enemyStatus.damage;


        // Initiating bools
        endGame = false;
        shieldHeal = false;
        alreadyDeployed = false;
        isEnemyDead = false;

        // Dissolve properties
        maxValue = 1;
        dissolved = true;
        x = 0;
        rend = bodyShader.GetComponent<Renderer>();
        rend.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {

        time +=  Time.deltaTime;
        
        if (dissolved == true)
        {
                    maxValue -= dissolveSpeed;
                    rend.sharedMaterial.SetFloat("Dissolve", maxValue);

                    if (maxValue <= 0.0f)
                    {
                        maxValue = 0f;
                    }                    
        }

        if (!dissolved) 
        {
                maxValue += dissolveSpeed;
                rend.sharedMaterial.SetFloat("Dissolve", maxValue);

                if (maxValue >= 1.0f)
                {
                    maxValue = 1.0f;
                }
        }
       
        

        if (!isEnemyDead)
        {

            // Check for sight and Attack Range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
            if (agent.hasPath)

            {
                _animator.SetFloat("Speed", agent.velocity.magnitude);
            }
            else
            {
                _animator.SetFloat("Speed", 0);
            }

            if (time > 7.5)
            {
                dissolved = false;
                if (time >= tpTimer)
                {
                    int spawnPointX = Random.Range(-15, 15);
                    int spawnPointZ = Random.Range(-15, 15);

                    Vector3 spawnPosition = new Vector3(spawnPointX, 0, spawnPointZ);
                    gameObject.transform.localPosition = spawnPosition;
                    time = 0;
                    dissolved = true;
                }
            }

            switch (_states)
            {
                case RenegadeStates.Idle:
                    {
                        if (!playerInSightRange && !playerInAttackRange)
                        {
                            agent.SetDestination(transform.position);
                            Debug.Log("Boss idle!");
                            if (currentHealth <= 800)
                            {
                                ForceField();
                            }


                        }

                        if (playerInSightRange && !playerInAttackRange)
                        {
                            _states = RenegadeStates.Chasing;
                        }

                        if (playerInSightRange && playerInAttackRange)
                        {
                            _states = RenegadeStates.Shooting;
                        }


                    }
                    break;

                case RenegadeStates.Chasing:
                    {
                        if (playerInSightRange && !playerInAttackRange)
                        {

                            ChasePlayer();
                            Debug.Log("Chasing Player");

                            if (currentHealth <= 800)
                            {
                                ForceField();
                            }


                        }
                        if (playerInSightRange && playerInAttackRange)
                        {
                            _states = RenegadeStates.Shooting;
                        }

                    }
                    break;

                case RenegadeStates.Shooting:
                    {
                        if (playerInSightRange && playerInAttackRange)

                        {
                            AttackPlayer();
                            //Debug.Log("Atacking Player");
                            if (currentHealth <= 800)
                            {
                                ForceField();
                            }
                        }
                        if (playerInSightRange && !playerInAttackRange)
                        {
                            _states = RenegadeStates.Chasing;
                        }

                        if (!playerInSightRange && !playerInAttackRange)
                        {
                            _states = RenegadeStates.Idle;
                        }

                        if (currentHealth <= 800)
                        {
                            ForceField();
                        }
                    }
                    break;
                case RenegadeStates.ForceField:
                    {
                        if (currentHealth <= 800)
                        {
                            ForceField();
                        }

                        if (currentHealth > 800)
                        {
                            _states = RenegadeStates.Chasing;
                        }
                    }
                    break;
            }

            if (currentHealth <= 800.0)
            {
                ForceField();
            }

            if (shieldHeal == true)
            {
                currentHealth += healing * (healSpeed * Time.deltaTime);
                if (currentHealth > 1000)
                {
                    currentHealth = maxHealth;
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
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            _animator.SetTrigger("Shoot");
            //StartCoroutine(Bullet());


            ///End of attack code
            //Invoke(nameof(BulletOut), 0.5f);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void Bullet()
    {
        Rigidbody rb = Instantiate(projectile, spawnBullet.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(-transform.up * 4f, ForceMode.Impulse);
    }

    private void Teleport()
    {
        Debug.Log("Nothing to see here yey");
    }

    private void ForceField()
    {
        if (alreadyDeployed == false)
        {

            _forceField.gameObject.SetActive(true);
            SpawnDrones();
            currentHealth += healing * (healSpeed * Time.deltaTime);
            healthBar.SetHealth(currentHealth);
            alreadyDeployed = true;
            shieldHeal = true;
            _drones.gameObject.SetActive(true);
            if (shieldHeal == true)
            {
                agent.SetDestination(transform.position);
            }

        }    


    }

    public void TakeDamage(float amount)
    {
        if (shieldHeal == false)
        {
            currentHealth -= amount;
            healthBar.SetHealth(currentHealth);
            Debug.Log($"Enemy hit for {amount}, current Enemy Health is {currentHealth}");

        }
        if (currentHealth <= 0f)
        {
            if (isEnemyDead == false)
            {
                Die();
                isEnemyDead = true;

            }
        }



    }

    private void Die()
    {
        isEnemyDead = true;
        _animator.SetTrigger("dead");
        audioSource.clip = bossOver;
        audioSource.PlayOneShot(bossOver);
        AddScore();
        dissolved = false;
        //Destroy(gameObject, 2.5f);
        Invoke("EndGame", 3f);


    }

    private void AddScore()
    {
        if (pointsToAdd == false)
        {
            GameManager.instance.AddPoints();
            pointsToAdd = true;

        }
    }

    private void SpawnDrones()
    {
        Vector3 bossPos = transform.position;
        Vector3 bossDirection = transform.forward;
        Quaternion bossRotation = transform.rotation;
        float spawnDistance = 2.5f;

        Vector3 spawnPos = bossPos + bossDirection * spawnDistance;

        Instantiate(_drones, spawnPos, bossRotation);
    }

    private void EndGame()
    {
        endGame = true;
        _gameOverscreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        
    }

 
    enum RenegadeStates
    {
        Idle,
        Chasing,
        Shooting,
        ForceField,
        Teleport,
        Dead
    }
}