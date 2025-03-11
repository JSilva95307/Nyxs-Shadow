using UnityEngine;

public class SlimeBehavior : BaseEnemy
{
    public BoxCollider2D hitbox;
    public float damage;
    public Animator animator;

    private Health health;

    public float playerCheckRange;
    public LayerMask playerMask;
    private bool facingRight = true;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
        animator.SetBool("Patrol", true);
        hitbox.enabled = false;
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
    void Update()
    {
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
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void PlayDeathAnim()
    {
        Debug.Log("Died");
        animator.SetTrigger("Die");
    }

    public void FlipSlime()
    {
        Vector3 scale = transform.localScale;

        if (facingRight)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            facingRight = false;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            facingRight = true;
        }

        transform.localScale = scale;
    }
}
