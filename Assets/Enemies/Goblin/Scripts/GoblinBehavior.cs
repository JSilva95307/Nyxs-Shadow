using UnityEngine;

public class GoblinBehavior : BaseEnemy
{
    public BoxCollider2D hitbox;
    public float damage;
    public Animator animator;
    private Health health;
    private bool facingRight;
    [SerializeField]private Vector2 spawnLocation;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        ApplyGravity();
        facingRight = true;
        if (targetSet == true && Vector2.Distance(targetLocation, transform.position) > 0.25f)
        {
            MoveTo();
        }
        else if (targetSet == true && Vector2.Distance(targetLocation, transform.position) <= 0.25f)
        {
            targetSet = false;
            targetLocation = Vector2.zero;
            animator.SetTrigger("Idle");
        }
        if (playerFound == true)
        {
            animator.SetBool("Chase", true);
            Debug.Log("Chasing Player");
        }
        else if (playerFound == false)
        {
            animator.SetTrigger("Idle");
            animator.SetBool("Chase", false);
            Debug.Log("Stopped Chasing Player");
        }

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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    public void PlayDeathAnim()
    {
        animator.SetTrigger("Die");
    }

    public void LookAtPlayer()
    {
        FacePlayer();
    }

    public void FlipGoblin()
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

    public Vector2 GetSpawn() { return spawnLocation; }
    public void SetSpawn(Vector2 newSpawn) { spawnLocation = newSpawn; }
}
