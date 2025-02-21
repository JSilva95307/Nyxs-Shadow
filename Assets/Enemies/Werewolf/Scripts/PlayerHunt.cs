using UnityEngine;

public class PlayerHunt : StateMachineBehaviour
{
    public float meleeRange;
    public float runSpeed;

    Transform player;
    Rigidbody2D rb;
    WerewolfBehavior wolf;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        wolf = animator.GetComponent<WerewolfBehavior>();
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, runSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (Vector2.Distance(player.position, rb.position) <= meleeRange)
            animator.SetTrigger("Melee1");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
