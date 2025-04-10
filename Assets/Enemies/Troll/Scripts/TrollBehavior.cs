using UnityEngine;

public class TrollBehavior : BaseEnemy
{
    public float slapDamage;
    public float smashDamage;
    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    public BoxCollider2D meleeCollider;
    public GameObject thorns;
    public Transform spawnLocation;
    public int meleeDamage;
    private Health health;
    private bool facingRight = true;

    private RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
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

    public void DisableAttack()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

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
}
