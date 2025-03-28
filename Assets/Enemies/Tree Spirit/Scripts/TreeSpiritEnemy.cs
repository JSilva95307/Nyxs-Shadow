using UnityEngine;

public class TreeSpiritEnemy : BaseEnemy
{
    private Health health;

    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    private bool facingRight = true;
    public BoxCollider2D meleeCollider;
    public BaseProjectile thorns;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
        meleeCollider.enabled = false;
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
    }

    public override void Attack()
    {
    }

    public override void Attack2()
    {
    }

    public override void Attack3()
    {
    }

    public void PlayDeathAnim()
    {
        animator.SetTrigger("Die");
    }

    public void LookAtPlayer() { FacePlayer(); }
    public void FlipTreeSpirit()
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
}
