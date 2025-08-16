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
        }
        else if (AttackController.instance.downAttackLv1 && PlayerManager.Instance._playerController.isGrounded)
        {
            AttackController.instance.animator.Play("SwordD1");
            Debug.Log("Down attack level 1");
        }
        else if (AttackController.instance.downAttackLv2 && PlayerManager.Instance._playerController.isGrounded)
        {
            AttackController.instance.animator.Play("SwordD2");
            Debug.Log("Down attack level 2");
        }
        else if (AttackController.instance.downAttackLv3 && PlayerManager.Instance._playerController.isGrounded)
        {
            AttackController.instance.animator.Play("SwordD3");
            Debug.Log("Down attack level 3");
        }
        else if (AttackController.instance.airUpAttack)
        {
            AttackController.instance.animator.Play("SwordAirUA");
            Debug.Log("Aerial Up Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackController.instance.isAttacking = false;
        AttackController.instance.didUpAttackGround = false;
        AttackController.instance.downAttackLv1 = false;
        AttackController.instance.downAttackLv2 = false;
        AttackController.instance.downAttackLv3 = false;
        AttackController.instance.downAttacking = false;
        AttackController.instance.airUpAttack = false;
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
