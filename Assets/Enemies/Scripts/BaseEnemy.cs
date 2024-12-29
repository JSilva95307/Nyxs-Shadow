using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float moveSpeed;

    public abstract void Attack();
    public abstract void Attack2();
    public abstract void Attack3();
}
