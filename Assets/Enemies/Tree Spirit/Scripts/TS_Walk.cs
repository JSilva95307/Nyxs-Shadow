using UnityEngine;

public class TS_Walk : StateMachineBehaviour
{
    public float patrolDist = 3f;
    bool goingRight = false;
    Vector2 patrolPoint = Vector2.zero;
    TreeSpiritEnemy ts;
    Transform rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ts = animator.GetComponent<TreeSpiritEnemy>();
        rb = ts.GetTransform();
        if (goingRight)
        {
            patrolPoint = new Vector2(rb.position.x + patrolDist, rb.position.y);
        }
        else
        {
            patrolPoint = new Vector2(rb.position.x - patrolDist, rb.position.y);
        }
        ts.FlipEnemy();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(rb.position, patrolPoint) >= 0.25)
        {
            Vector2 target = new Vector2(patrolPoint.x, rb.position.y);
            ts.SetTarget(target);
        }
        else
            animator.SetTrigger("Idle");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        goingRight = !goingRight;
        animator.ResetTrigger("Idle");
    }
}
