using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAttackPlayer : MonoBehaviour
{

    [HideInInspector] public NavMeshAgent agent;

    private Transform player;

    public Transform spawnBullet;

    public LayerMask whatIsGround, whatIsPlayer;

    public static int damage = 20;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public Animator _animator;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        Target locomotion = gameObject.GetComponent<Target>();
        if (locomotion.isEnemyDead != true)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
            }
            return;
        }

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

    private void  Bullet()
    {
        Rigidbody rb = Instantiate(projectile, spawnBullet.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(-transform.up * 4f, ForceMode.Impulse);
    }
}
