using UnityEngine;

public class SlimeBehavior : BaseEnemy
{
    public BoxCollider2D hitbox;
    public float damage;
    //public Animator animator;

    private Health health;

    public float playerCheckRange;
    public LayerMask playerMask;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(Die);
        animator.SetBool("Patrol", true);
        hitbox.enabled = false;
        base.Start();
    }

    public override void Attack()
    {
        hitbox.enabled = true;
    }

    public override void Attack2()
    {
    }

    public override void Attack3()
    {

    }

    public void DisableAttack()
    {
        hitbox.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (facingRight)
        {
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(playerCheckRange, 0), -playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), Color.black);
        }
        else
        {
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), -playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), Color.red);
        }
        if (playerCheck)
        {
            playerFound = true;
            Debug.Log("Player Spotted!");
            animator.SetBool("Chasing", true);
            animator.SetBool("Patrol", false);
        }
        if (playerFound)
        {
            FacePlayer();
        }
        CheckGround();
        ApplyGravity();

        // temp launch test
       
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    //private void PlayDeathAnim()
    //{
    //    Die();
    //}

    public void LookAtPlayer()
    {
        FacePlayer();
    }
}


