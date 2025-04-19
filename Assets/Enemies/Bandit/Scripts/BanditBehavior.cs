using UnityEngine;

public class BanditBehavior : BaseEnemy
{
    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    public BoxCollider2D meleeCollider;
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
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(meleeDamage);
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

    public void KnifeSpawn()
    {
       
    }
}
