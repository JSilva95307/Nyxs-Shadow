using UnityEngine;

public class TrollBehavior : BaseEnemy
{
    public float slapDamage;
    public float smashDamage;
    Health health;
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
