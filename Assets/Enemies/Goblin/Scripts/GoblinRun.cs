using UnityEngine;

public class GoblinRun : StateMachineBehaviour
{
    public float chaseSpeed = 5f;
    public float meleeRange = 1f;
    Transform player;
    Rigidbody2D rb;
    GoblinBehavior gobbo;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        gobbo = animator.GetComponent<GoblinBehavior>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gobbo.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, chaseSpeed * Time.fixedDeltaTime);
        if (Vector2.Distance(player.position, rb.position) <= meleeRange)
            animator.SetTrigger("Attack");
        else
            rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
