using UnityEngine;

public class TS_Chase : StateMachineBehaviour
{
    TreeSpiritEnemy ts;
    Transform curPos;
    Transform player;
    public float meleeRange;
    public float maxChaseTime;
    float elapsedTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ts = animator.GetComponent<TreeSpiritEnemy>();
        curPos = ts.GetTransform();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        elapsedTime = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ts.LookAtPlayer();
        ts.ChasePlayer();

        elapsedTime += Time.deltaTime;
        if(Vector2.Distance(curPos.position, player.position) <= meleeRange)
        {
            animator.SetTrigger("Strike");
        }
        else if(elapsedTime > maxChaseTime)
        {
            animator.SetTrigger("Shoot");
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Strike");
        animator.ResetTrigger("Shoot");
    }
}
