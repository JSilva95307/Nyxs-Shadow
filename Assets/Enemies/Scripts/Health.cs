using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float health;


    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float _health)
    {
        health = _health;
    }
}
