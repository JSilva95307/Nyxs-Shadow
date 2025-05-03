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
        float moneyTotal;
        if (collision.CompareTag("Player"))
        {
            //call the get and set functions of the player for their money
            collision.GetComponent<Health>().TakeDamage(meleeDamage);
            moneyTotal = collision.GetComponent<Currency>().GetMoney();
            //subtract using the moneySteal var
            collision.GetComponent <Currency>().SetMoney(moneyTotal - moneySteal);
            //add the amount to the moneyCollected var
            moneyCollected += moneySteal;

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
        Vector3 scale = transform.localScale;
        Vector3 target = player.transform.position;
        Quaternion rotation = transform.rotation;

        if (target.x > transform.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        //transform.localScale = scale;
        transform.localRotation = rotation;
    }

    public void AvoidPlayer()
    {
        Vector3 target = player.transform.position;

        movement = Vector2.right;

        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public void PersuePlayer()
    {
        playerFound = true;
    }
}
