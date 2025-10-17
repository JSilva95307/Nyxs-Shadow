using UnityEngine;

public class SlimePatrol : StateMachineBehaviour
{
    public float patrolDist = 3f;
    [SerializeField] Vector2 patrolPoint = Vector2.zero;
    [SerializeField] bool goingRight = true;
    Transform rb;
    SlimeBehavior slime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        slime = animator.GetComponent<SlimeBehavior>();
        rb = slime.GetTransform();
        if (goingRight)
        {
            patrolPoint = new Vector2(rb.position.x + patrolDist, rb.position.y);
        }
        else
        {
            patrolPoint = new Vector2(rb.position.x - patrolDist, rb.position.y);
        }
        slime.FlipEnemy();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(rb.position, patrolPoint) >= 1)
        {
            Vector2 target = new Vector2(patrolPoint.x, rb.position.y);
            slime.SetTarget(target);
        }
        else
            animator.SetTrigger("SitIdle");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        goingRight = !goingRight;
        animator.ResetTrigger("SitIdle");
        slime.SetTarget(slime.transform.position);
    }
}
