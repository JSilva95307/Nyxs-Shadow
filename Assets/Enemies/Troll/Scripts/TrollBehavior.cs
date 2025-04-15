using NUnit.Framework;
using UnityEngine;

public class TrollBehavior : BaseEnemy
{
    public float slapDamage;
    public float smashDamage;
    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    public BoxCollider2D meleeCollider;
    public BoxCollider2D slamCollider;
    public GameObject shockwave;
    public Transform spawnLocation;
    public int meleeDamage;
    private Health health;
    private bool facingRight = true;

    private RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        meleeCollider.enabled = false;
        health.AddDeathListener(TrollDeath);
    }

    // Update is called once per frame
    void Update()
    {
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

        if (playerCheck)
        {
            Debug.Log("Player Spotted!");
            animator.SetTrigger("Chase");
        }
    }

    public override void Attack()
    {
        meleeCollider.enabled = true;
    }

    public override void Attack2()
    {
        slamCollider.enabled = true;
        ShockwaveSpawn();
    }

    public override void Attack3()
    {
    }

    public void DisableAttack()
    {
        meleeCollider.enabled = false;
        slamCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(meleeDamage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    public void FlipTroll()
    {
        Quaternion rotation = transform.rotation;
        if (facingRight)
        {
            rotation.y = 0;
        }
        else
        {
            rotation.y = 180;
        }
        transform.localRotation = rotation;
        facingRight = !facingRight;
    }
    public void LookAtPlayer() { FacePlayer(); }
    public void TrollDeath() { animator.SetTrigger("Die"); }

    public void ShockwaveSpawn()
    {
        Instantiate(shockwave, spawnLocation.position, spawnLocation.rotation);
    }
}
