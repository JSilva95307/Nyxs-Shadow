using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float gravityStrength;
    public Transform groundCheckPos;
    public LayerMask groundLayerMask;
    protected Vector2 movement;
    protected float verticalSpeed;
    protected bool flip;


    public abstract void Attack();
    public abstract void Attack2();
    public abstract void Attack3();

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPos.position, -Vector2.up, 0.2f, groundLayerMask);
        if (hit.collider)
        {
            verticalSpeed = 0;
        }
        else
            verticalSpeed = Mathf.Lerp(verticalSpeed, -gravityStrength, gravityStrength * Time.deltaTime);
    }
}
