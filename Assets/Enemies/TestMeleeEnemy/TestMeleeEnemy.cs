using System.Collections;
using UnityEngine;

public class TestMeleeEnemy : BaseEnemy
{
    public Animator animator;
    public float timeBetweenAttacks = 2f;

    public float speed;
    public float stoppingDistance;
    public float attackRange = 1;

    //private bool flip;

    private GameObject player;

    private bool canAttack = false;
    private float timer = 0f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        ChasePlayer();

        if (canAttack == true && timer > timeBetweenAttacks)
        {
            canAttack = false;
            PlayAttackLoop();
        }


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
        if (timer < timeBetweenAttacks)
            yield return null;



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
        timer = 0;
        StartCoroutine(AttackLoop());
    }

    private void ChasePlayer()
    {
        Vector3 scale = transform.localScale;
        Vector3 target = player.transform.position;

        if (transform.position.x > target.x - attackRange && transform.position.x < target.x + attackRange)
        {
            canAttack = true;
            return;
        }
            


        if (target.x > transform.position.x)
            target.x = target.x - stoppingDistance;
        else
            target.x = target.x + stoppingDistance;

        if (target.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(x:speed * Time.deltaTime, y: 0, z: 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(x:speed * Time.deltaTime * -1, y: 0, z: 0);
        }

        transform.localScale = scale;
    }
}
