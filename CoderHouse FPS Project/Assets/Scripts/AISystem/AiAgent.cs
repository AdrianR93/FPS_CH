using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AiAgent : MonoBehaviour
{
    [HideInInspector] public AiStateMachine stateMachine;
    public AiStateId initialState;
    public NavMeshAgent navMeshAgent;
    public AiAgentConfig config;
    public Transform playerTransform;
    public AudioSource audioSource;
    public AudioClip intruderAlert;
    public Target locomotion;



    // Start is called before the first frame update
    void Start()
    {
        locomotion = gameObject.GetComponent<Target>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        stateMachine = new AiStateMachine(this);
        stateMachine.RegisterState(new AiChasePlayerState());
        stateMachine.RegisterState(new AiIdleState());
        stateMachine.ChangeState(initialState);
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();


    }
}
