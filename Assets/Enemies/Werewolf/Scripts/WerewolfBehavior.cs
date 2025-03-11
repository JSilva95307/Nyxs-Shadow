using Unity.VisualScripting;
using UnityEngine;

public class WerewolfBehavior : BaseEnemy
{
    public float timeBetweenAttacks = 2f;

    private Health health;

    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    private bool facingRight = true;
    public BoxCollider2D meleeCollider;
    public float wolfDamage;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
        animator.SetBool("Patrol", true);
        meleeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(playerCheckRange, 0), -playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), Color.black);
        }
        else if (!facingRight)
        {
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), -playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), Color.red);
        }
        if (playerCheck)
        {
            playerFound = true;
            Debug.Log("Player Spotted!");
            animator.SetTrigger("FoundPlayer");
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
            collision.GetComponent<Health>().TakeDamage(wolfDamage);
        }
    }


    public override void Attack()
    {
        meleeCollider.enabled = true;
    }

    public override void Attack2()
    {
    }

    public override void Attack3()
    {

    }

    public void DisableAttacks()
    {
        meleeCollider.enabled = false;
    }
    private void PlayDeathAnim()
    {
        Debug.Log("Died");
        animator.SetTrigger("Die");
    }

    public void FlipWolf()
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
