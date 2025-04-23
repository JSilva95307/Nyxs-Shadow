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
