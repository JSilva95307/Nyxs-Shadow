using UnityEngine;

public class Bandit_Wait : StateMachineBehaviour
{
    public float playerDetectRange;

    Transform curPos;
    Transform playerPos;
    BanditBehavior bandit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bandit = animator.GetComponent<BanditBehavior>(); 
        curPos = bandit.GetTransform();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(curPos.position, playerPos.position) <= playerDetectRange)
        {
            animator.SetTrigger("Rerun");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Rerun");
    }
}
