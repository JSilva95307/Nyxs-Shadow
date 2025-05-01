using UnityEngine;

public class Bandit_Retreat : StateMachineBehaviour
{
    public float avoidRange = 20f;
    Transform player;
    Transform rb;
    BanditBehavior bandit;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bandit = animator.GetComponent<BanditBehavior>();
        rb = bandit.GetTransform();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(rb.position, player.position) < avoidRange)
        {
            bandit.AvoidPlayer();
        }
        bandit.LookAwayFromPlayer();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
