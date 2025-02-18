using UnityEngine;

public class WerewolfBehavior : BaseEnemy
{
    public float timeBetweenAttacks = 2f;

    private float timer = 0f;
    private Health health;

    private bool dead = false;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
        health.AddDeathListener(PlayDeathAnim);
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

    private void PlayDeathAnim()
    {
        Debug.Log("Died");
        animator.SetTrigger("Die");
    }
}
