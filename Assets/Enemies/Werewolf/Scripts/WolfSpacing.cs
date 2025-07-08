using UnityEngine;

public class WolfSpacing : StateMachineBehaviour
{
    public float minFollowDist;
    public float maxFollowDist;
    WerewolfBehavior wolf;
    Transform wolfPos;
    Transform playerPos;
    float meleeTimer;
    float runATimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf = animator.GetComponent<WerewolfBehavior>();
        wolfPos = wolf.GetTransform();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        meleeTimer = 0;
        runATimer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf.LookAtPlayer();
        if (Vector2.Distance(wolfPos.position, playerPos.position) < minFollowDist)
        {
            wolfPos.Translate(-Vector2.right * (wolf.GetMoveSpeed() * 0.5f) * Time.deltaTime);
            meleeTimer += Time.fixedDeltaTime;
        }
        else if (Vector2.Distance(wolfPos.position, playerPos.position) > maxFollowDist)
        {
            wolfPos.Translate(Vector2.right * (wolf.GetMoveSpeed() * 0.5f) * Time.deltaTime);
            runATimer += Time.fixedDeltaTime;
        }
        else
            runATimer += Time.fixedDeltaTime;
        if(meleeTimer >= 0.75)
        {
            animator.SetTrigger("Melee1");
        }
        else if (runATimer >= 0.5)
        {
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
