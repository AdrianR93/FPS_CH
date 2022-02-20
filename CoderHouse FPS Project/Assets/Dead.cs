using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : StateMachineBehaviour
{
    // Dissolve Shader Variables
    private bool dissolved;
    public int x;
    Renderer rend;
    public float dissolveSpeed = 1f;
    private float maxValue;
    private bool isShaderUp;
    [SerializeField] private GameObject bodyShader;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        maxValue = 0;
        dissolved = true;
        x = 0;
        rend = bodyShader.GetComponent<Renderer>();
        rend.enabled = true;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        maxValue += dissolveSpeed;
        rend.sharedMaterial.SetFloat("Dissolve", maxValue);

        if (maxValue >= 1.0f)
        {
            maxValue = 1.0f;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
