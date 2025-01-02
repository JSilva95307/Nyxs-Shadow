using System.Collections;
using UnityEngine;

public class TestMeleeEnemy : BaseEnemy
{
    public Animator animator;
    public float timeBetweenAttacks = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(AttackLoop());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            PlayAttackLoop();
    }


    public override void Attack()
    {
        animator.SetTrigger("Attack1");
    }

    public override void Attack2()
    {
        animator.SetTrigger("Attack2");
    }

    public override void Attack3()
    {
        animator.SetTrigger("Attack3");
    }

    private IEnumerator AttackLoop()
    {
        //yield return new WaitForSeconds(1f);
        Attack();

        //Wait until the transition is over
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);

        //Wait until the animation is over
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"));
        Attack2();


        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"));
        Attack3();

        yield return null;
    }

    private void PlayAttackLoop()
    {
        StartCoroutine(AttackLoop());
    }
}
