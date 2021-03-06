using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiIdleState : AiState
{
    private AudioClip intruderAlert;

    public AiStateId GetId()
    {
        return AiStateId.Idle;
    }
    public void enter(AiAgent agent)
    {
       
    }

    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if (playerDirection.magnitude > agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;

        playerDirection.Normalize();

        float dotProduct = Vector3.Dot(playerDirection, agentDirection);
        if ( dotProduct > 0.0f)
        {
            intruderAlert = agent.config.intruderAlert;
            agent.audioSource.clip = intruderAlert;
            agent.audioSource.PlayOneShot(intruderAlert);

            agent.stateMachine.ChangeState(AiStateId.ChasePlayer);


        }    



    }
    public void Exit(AiAgent agent)
    {

    }
}
