using Unity.VisualScripting;
using UnityEngine;

public class WerewolfBehavior : BaseEnemy
{
    public float timeBetweenAttacks = 2f;

    private float timer = 0f;
    private Health health;

    public Animator animator;
    public float playerCheckRange;
    public LayerMask playerMask;
    private bool facingRight = true;

    RaycastHit2D playerCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            playerCheck = Physics2D.Raycast(transform.position, new Vector2(playerCheckRange, 0), playerCheckRange, playerMask);
            Debug.DrawRay(transform.position, new Vector2(playerCheckRange, 0), Color.black);
        }
        else
        {
            playerCheck = Physics2D.Raycast(transform.position, new Vector2(-playerCheckRange, 0), playerCheckRange, playerMask);
            Debug.DrawRay(transform.position, new Vector2(-playerCheckRange, 0), Color.red);
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

    private void PlayDeathAnim()
    {
        Debug.Log("Died");
        animator.SetTrigger("Die");
    }

    public void FlipWolf()
    {
        Vector3 scale = transform.localScale;

        if (facingRight)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            facingRight = false;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            facingRight = true;
        }

        transform.localScale = scale;
    }
}
