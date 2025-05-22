using UnityEngine;

public class GoblinRetreat : StateMachineBehaviour
{
    Transform curPos;
    GoblinBehavior gobbo;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gobbo = animator.GetComponent<GoblinBehavior>();
        curPos = gobbo.GetTransform();
        gobbo.ReturnToSpawn();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("Idle");
    }
}
