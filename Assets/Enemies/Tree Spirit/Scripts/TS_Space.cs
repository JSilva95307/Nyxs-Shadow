using UnityEngine;

public class TS_Space : StateMachineBehaviour
{
    public float minDist;
    public float maxDist;
    float runTimer;
    float meleeTimer;
    TreeSpiritEnemy ts;
    Transform curPos;
    Transform player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        runTimer = 0;
        meleeTimer = 0;
        ts = animator.GetComponent<TreeSpiritEnemy>();
        curPos = ts.GetTransform();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(player.position, curPos.position) < minDist)
        {
            curPos.Translate(-Vector2.right * (ts.GetMoveSpeed() * 0.5f) * Time.deltaTime);
            meleeTimer += Time.fixedDeltaTime;
        }
        if (Vector2.Distance(curPos.position, player.position) > maxDist)
        {
            curPos.Translate(Vector2.right * (ts.GetMoveSpeed() * 0.5f) * Time.deltaTime);
            runTimer += Time.fixedDeltaTime;
        }
        if (meleeTimer >= 0.75)
        {
            animator.SetTrigger("Melee1");
        }
        else if (runTimer >= 0.5)
        {
            animator.SetTrigger("RunAttack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
