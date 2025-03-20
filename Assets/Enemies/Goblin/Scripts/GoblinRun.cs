using UnityEngine;

public class GoblinRun : StateMachineBehaviour
{
    public float chaseSpeed = 5f;
    public float meleeRange = 1f;
    Transform player;
    Transform rb;
    GoblinBehavior gobbo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gobbo = animator.GetComponent<GoblinBehavior>();
        rb = gobbo.GetTransform();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gobbo.LookAtPlayer();
        gobbo.ChasePlayer();
        if (Vector2.Distance(player.position, rb.position) <= meleeRange)
            animator.SetTrigger("Attack");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
