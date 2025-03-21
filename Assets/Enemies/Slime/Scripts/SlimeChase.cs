using UnityEngine;

public class SlimeChase : StateMachineBehaviour
{
    public float meleeRange = 1f;
    Transform player;
    Transform rb;
    SlimeBehavior slime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        slime = animator.GetComponent<SlimeBehavior>();
        rb = slime.GetTransform();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        slime.ChasePlayer();
        slime.LookAtPlayer();
        if (Vector2.Distance(player.position, rb.position) <= meleeRange)
            animator.SetTrigger("SitIdle");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("SitIdle");
    }
}
