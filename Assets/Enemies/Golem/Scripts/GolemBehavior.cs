using UnityEngine;

public class GolemBehavior : BaseEnemy
{
    public Animator animator;
    
    public float timeBetweenAttacks = 2f;
    public GameObject projectile;
    public Transform spawnLocation;

    
    private GameObject player;
    private float timer = 0f;
    private Health health;

    private bool dead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = GetComponent<Health>();
    }


    private void Update()
    {
        timer += Time.deltaTime;
        FacePlayer();

        if (timer > timeBetweenAttacks)
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

        if (target.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }

        transform.localScale = scale;
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
