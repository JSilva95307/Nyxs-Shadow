using UnityEngine;

public class PlayerHunt : StateMachineBehaviour
{
    public float meleeRange;
    public float runAttackRange;
    Transform player;
    Transform rb;
    WerewolfBehavior wolf;
    bool shouldMelee;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wolf = animator.GetComponent<WerewolfBehavior>();
        rb = wolf.GetTransform();
        if (Vector2.Distance(player.position, rb.position) > 10)
            shouldMelee = false;
        else
            shouldMelee = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf.ChasePlayer();
        if (shouldMelee)
        {
            if (Vector2.Distance(player.position, rb.position) <= meleeRange)
                animator.SetTrigger("Melee1");
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
        animator.ResetTrigger("Melee1");
        animator.ResetTrigger("RunAttack");
    }
}
