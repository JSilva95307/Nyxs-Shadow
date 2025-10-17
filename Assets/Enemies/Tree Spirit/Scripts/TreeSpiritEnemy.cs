using Unity.VisualScripting;
using UnityEngine;

public class TreeSpiritEnemy : BaseEnemy
{
    private Health health;

    //public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    public BoxCollider2D meleeCollider;
    public GameObject thorns;
    public Transform spawnLocation;
    public int meleeDamage;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        health = GetComponent<Health>();
        health.AddDeathListener(Die);
        meleeCollider.enabled = false;
        playerFound = false;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        CheckGround();
        ApplyGravity();

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
        else if (!facingRight)
        {
            playerCheck = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), -playerCheckRange, playerMask);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1), new Vector2(-playerCheckRange, 0), Color.red);
        }

        if(playerCheck)
        {
            Debug.Log("Player Spotted!");
            animator.SetBool("Chase", true);
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

    public void DisableAttack()
    {
        meleeCollider.enabled = false;
    }

    //public void PlayDeathAnim()
    //{
    //    animator.SetTrigger("Die");
    //}

    public void FireProjectile()
    {
        Instantiate(thorns, spawnLocation.position, spawnLocation.rotation);
    }

    public void LookAtPlayer() { FacePlayer(); }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(meleeDamage);
        }
    }
}
