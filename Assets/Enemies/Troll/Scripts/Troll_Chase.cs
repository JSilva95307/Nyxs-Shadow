using UnityEngine;

public class Troll_Chase : StateMachineBehaviour
{
    TrollBehavior troll;
    Transform curPos;
    Transform player;
    public float meleeRange;
    public float maxChaseTime;
    float elapsedTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        troll = animator.GetComponent<TrollBehavior>();
        curPos = troll.GetTransform();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        elapsedTime = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        troll.LookAtPlayer();
        troll.ChasePlayer();

        elapsedTime += Time.deltaTime;
        if (Vector2.Distance(curPos.position, player.position) <= meleeRange)
        {
            animator.SetTrigger("Melee1");
        }
        //else if (elapsedTime > maxChaseTime)
        //{
        //    animator.SetTrigger("Shoot");
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Melee1");
    }
}
