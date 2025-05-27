using System.Collections;
using UnityEngine;

public class GolemBehavior : BaseEnemy
{
    public Animator animator;
    
    public float timeBetweenAttacks = 2f;
    public GameObject projectile;
    public Transform spawnLocation;
    
    
    private float timer = 0f;
    private Health health;

    private bool dead = false;

    //retreat vars
    private float retreatTime;
    private float retreatCD;
    private bool canRetreat;


    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnimation);
        canRetreat = true;
        retreatCD = 3.0f;
    }


    private void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();

        if (timer > timeBetweenAttacks && !dead)
        {
            Attack();
            timer = 0f;
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= 3 && canRetreat)
        {
            animator.SetTrigger("Retreat");
            transform.Translate(-transform.right * moveSpeed);
            retreatTime = Time.time;
            canRetreat = false;
        }
        else if (retreatTime + retreatCD <= Time.time)
            canRetreat = true;

        CheckGround();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.T))
            TeleportToPlayer();
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

    public void SpawnProjectile()
    {
        Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death"); // Animation calls function to destroy the gameobject
    }
}
