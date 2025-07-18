using UnityEngine;

public class PlayerHunt : StateMachineBehaviour
{
    public float meleeRange;
    public float runAttackRange;
    Transform player;
    Transform rb;
    WerewolfBehavior wolf;
    bool shouldRunAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wolf = animator.GetComponent<WerewolfBehavior>();
        rb = wolf.GetTransform();
        if (Vector2.Distance(player.position, rb.position) < 10)
            shouldRunAttack = false;
        else
            shouldRunAttack = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf.ChasePlayer();
        wolf.LookAtPlayer();
        if (!shouldRunAttack)
        {
            if (Vector2.Distance(player.position, rb.position) <= meleeRange)
                animator.SetTrigger("Space");
        }
        else
        {
            if (Vector2.Distance(player.position, rb.position) <= runAttackRange)
                animator.SetTrigger("RunAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Space");
        animator.ResetTrigger("RunAttack");
    }
}
