using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WerewolfBehavior : BaseEnemy
{
    public float timeBetweenAttacks = 2f;

    private Health health;

    //public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    public BoxCollider2D meleeCollider;
    public float wolfDamage;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        health = GetComponent<Health>();
        health.AddDeathListener(Die);
        animator.SetBool("Patrol", true);
        meleeCollider.enabled = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(playerCheckRange, 0), playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(playerCheckRange, 0), Color.black);
        }
        else if (!facingRight)
        {
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), Color.red);
        }
        if (playerCheck)
        {
            playerFound = true;
            Debug.Log("Player Spotted!");
            animator.SetTrigger("FoundPlayer");
            animator.SetBool("Patrol", false);
        }

        CheckGround();
        ApplyGravity();
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
    //private void PlayDeathAnim()
    //{
    //    Debug.Log("Died");
    //    animator.SetTrigger("Die");
    //}

    public void FlipWolf()
    {
        Quaternion rotation = transform.rotation;

        if (!facingRight)
        {
            rotation.y = 0;
            facingRight = true;
        }
        else
        {
            rotation.y = 180;
            facingRight = false;
        }
        transform.localRotation = rotation;
    }
    public void LookAtPlayer()
    {
        FacePlayer();
    }
}
