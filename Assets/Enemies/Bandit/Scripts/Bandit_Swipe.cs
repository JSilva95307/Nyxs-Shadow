using UnityEngine;

public class Bandit_Swipe : StateMachineBehaviour
{
    public float meleeRange;
    Transform player;
    Transform curPos;
    BanditBehavior bandit;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        bandit = animator.GetComponent<BanditBehavior>();
        curPos = bandit.GetTransform();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bandit.LookAtPlayer();
        bandit.ChasePlayer();
        if (Vector2.Distance(player.position, curPos.position) <= meleeRange)
            animator.SetTrigger("Attack");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
