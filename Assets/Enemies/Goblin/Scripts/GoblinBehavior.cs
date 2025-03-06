using UnityEngine;

public class GoblinBehavior : BaseEnemy
{
    public BoxCollider2D hitbox;
    public float damage;
    public Animator animator;

    private Health health;

    public float playerCheckRange;
    public LayerMask playerMask;
    private bool facingRight = true;
    public bool playerFound;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        //health.AddDeathListener(PlayDeathAnim);
        animator.SetBool("Patrol", true);
        hitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
