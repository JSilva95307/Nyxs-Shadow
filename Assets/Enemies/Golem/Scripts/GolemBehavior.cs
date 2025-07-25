using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GolemBehavior : BaseEnemy
{
    [Space(10)]
    [Header("Functionality Vars")]
    public Animator animator;
    private float timer = 0f;
    private Health health;
    //private bool dead = false;


    [Space(10)]
    [Header("Attack Vars")]
    public float timeBetweenAttacks = 2f;
    public GameObject projectile;
    public GameObject spawnLocation;
    
    
    [Space(10)]
    [Header("Retreat Vars")]
    public float retreatTime;
    public float retreatCD;
    public float retreatDistance;
    public bool canRetreat;
    public bool retreatQueued;
    public Vector3 retreatDest;




    protected override void Start()
    {
        base.Start();

        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnimation);
        canRetreat = true;
    }


    protected override void Update()
    {
        if(dead) return;

        base.Update();

        timer += Time.deltaTime;
        FacePlayer();
        

        if (timer > timeBetweenAttacks && !dead)
        {
            Attack();
            timer = 0f;
        }

        if (Vector2.Distance(transform.position, player.transform.position) <= 3 && canRetreat && DetectWall() == false && DetectLedge() == false)
        {
            canRetreat = false;
            Retreat();
        }
        else if (retreatTime + retreatCD <= Time.time)
        {
            canRetreat = true;
        }

        CheckGround();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.T))
            TeleportToPlayer();

        if (retreatQueued && DetectWall() == false && DetectLedge() == false)
        {
            transform.position = Vector3.Lerp(transform.position, retreatDest, moveSpeed * Time.deltaTime);
        }
    }

    
    public void Retreat()
    {
        retreatDest = transform.position;
        
        if (facingRight)
            retreatDest.x = transform.position.x - retreatDistance;
        else
            retreatDest.x = transform.position.x + retreatDistance;

        animator.SetTrigger("Retreat"); // Retreat animation sets retreatQueued variable
        retreatTime = Time.time;
        canRetreat = false;
    }


    public void QueueRetreat()
    {
        retreatQueued = true;
    }

    public void UnQueueRetreat()
    {
        retreatQueued = false;
        retreatDest = Vector3.zero;
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
        GameObject bullet = Instantiate(projectile, spawnLocation.transform.position, transform.rotation);
        bullet.GetComponent<GolemProjectile>().FacePlayer();
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death"); // Animation calls function to destroy the gameobject
        mainCollision.enabled = false;

        dead = true;

        for (int i = 0; i < drops.Count; i++)
        {
            drops[i].StartCoroutine(drops[i].MoveToPlayer());
        }

    }
}
