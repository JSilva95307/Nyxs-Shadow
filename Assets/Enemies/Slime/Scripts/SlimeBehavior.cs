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
        if (targetSet == true && Vector2.Distance(targetLocation, transform.position) > 0.25f)
        {
            MoveTo();
        }
        else if (targetSet == true && Vector2.Distance(targetLocation, transform.position) <= 0.25f)
        {
            targetSet = false;
            targetLocation = Vector2.zero;
        }

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
        Quaternion rotation = transform.rotation;

        if (facingRight)
        {
            rotation.y = 180;
            //scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            facingRight = false;
        }
        else
        {
            rotation.y = 0;
            //scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            facingRight = true;
        }
        Debug.Log("Called Flip");
        //transform.localScale = scale;
        //transform.localRotation = rotation;
    }
    public void LookAtPlayer()
    {
        FacePlayer();
    }
}


