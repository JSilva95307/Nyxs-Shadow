using UnityEngine;

public class WerewolfBehavior : BaseEnemy
{
    public float timeBetweenAttacks = 2f;

    private float timer = 0f;
    private Health health;

    private bool dead = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = GetComponent<Health>();
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
