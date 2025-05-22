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

    void Start()
    {
        health = GetComponent<Health>();
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

        CheckGround();
        movement = new Vector2(0, verticalSpeed);
        transform.Translate(movement * 2 * Time.deltaTime);
        
        if(health.GetCurrentHealth() <= 0 && dead == false)
        {
            PlayDeathAnimation();
            dead = true;
        }

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
