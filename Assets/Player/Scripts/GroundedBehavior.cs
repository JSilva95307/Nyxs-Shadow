using UnityEngine;

public class GroundedBehavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackController.instance.isAttacking && PlayerManager.Instance._playerController.isDashing)
        {
            AttackController.instance.animator.Play("DashAttack");
            //Debug.Log("DashAttack");
        }
        else if (AttackController.instance.isAttacking)
        {
            AttackController.instance.animator.Play("SwordAttack1");
        }
        else if (AttackController.instance.didUpAttackGround && PlayerManager.Instance._playerController.isGrounded)
        {
            AttackController.instance.animator.Play("SwordUpAttack");
            Debug.Log("Sword Up Attack Pt. 2");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackController.instance.isAttacking = false;
        AttackController.instance.didUpAttackGround = false;
    }

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
