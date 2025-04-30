using UnityEngine;

public class BanditBehavior : BaseEnemy
{
    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    public BoxCollider2D meleeCollider;
    public int meleeDamage;
    public int moneySteal;
    private int moneyCollected;
    private Health health;
    private bool facingRight;
    private bool runningAway;


    private RaycastHit2D playerCheck;
    // Start is called once before the first execution of Update after the Mono Behaviour is created
    void Start()
    {
        facingRight = true;
        runningAway = false;
        moneyCollected = 0;
        health = GetComponent<Health>();
        health.AddDeathListener(BanditDeath);
        meleeCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        ApplyGravity();

        if (playerFound)
        {
            animator.SetTrigger("PlayerFound");
            Debug.Log("Chasing Player");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(meleeDamage);
            //call the get and set functions of the player for their money

            //subtract using the moneysteal var
            //add the amount to the moneytotal var
            animator.SetBool("RunAway", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }

    public void FlipBandit()
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
    public void BanditDeath() {  }

    public void LookAwayFromPlayer()
    {

    }

    public void AvoidPlayer()
    {

    }
}
