using UnityEngine;

public class GoblinBehavior : BaseEnemy
{
    public BoxCollider2D hitbox;
    public float damage;
    public Animator animator;
    private Health health;

    public LayerMask playerMask;

    private float test;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
        hitbox.enabled = false;
        test = 0;
    }

    // Update is called once per frame
    void Update()
    {
        test += Time.deltaTime;
        if (test >= 3)
            animator.SetBool("Chase", true);
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

   
}
