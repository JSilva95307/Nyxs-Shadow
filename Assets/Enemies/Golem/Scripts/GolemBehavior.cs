using UnityEngine;

public class GolemBehavior : BaseEnemy
{
    public Animator animator;
    public float timeBetweenAttacks = 2f;
    public GameObject projectile;
    public Transform spawnLocation;

    //public float speed;
    //public float stoppingDistance;
    //public float attackRange = 1;

    private bool flip;

    private GameObject player;

    private bool canAttack = false;
    private float timer = 0f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();

        if (timer > timeBetweenAttacks)
        {
            //canAttack = false;
            Attack();
            timer = 0f;
        }
    }


    public override void Attack()
    {
        animator.SetTrigger("AttackB");
    }

    public override void Attack2()
    {
        
    }

    public override void Attack3()
    {
        
    }

    private void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        Vector3 target = player.transform.position;

        //if (transform.position.x > target.x - attackRange && transform.position.x < target.x + attackRange)
        //{
        //    canAttack = true;
        //    return;
        //}



        //if (target.x > transform.position.x)
        //    target.x = target.x - stoppingDistance;
        //else
        //    target.x = target.x + stoppingDistance;

        if (target.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            //transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            //transform.Translate(x: speed * Time.deltaTime * -1, y: 0, z: 0);
        }

        transform.localScale = scale;
    }

    public void SpawnProjectile()
    {
        Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
        Debug.Log("Spawned Projectile");
    }
}
