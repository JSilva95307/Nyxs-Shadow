using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public float moveSpeed;
    public float gravityStrength;
    public Transform groundCheckPos;
    public LayerMask groundLayerMask;
    public bool grounded;
    protected Vector2 movement;
    protected float verticalSpeed;
    protected bool flip;
    protected GameObject player;
    protected bool playerFound;

    public abstract void Attack();
    public abstract void Attack2();
    public abstract void Attack3();

    private void Start()
    {
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPos.position, -Vector2.up, 0.2f, groundLayerMask);
        if (hit.collider)
        {
            verticalSpeed = 0;
            grounded = true;
        }
        else
        {
            verticalSpeed = Mathf.Lerp(verticalSpeed, -gravityStrength, gravityStrength * Time.deltaTime);
            grounded = false;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected void FacePlayer()
    {
        Vector3 scale = transform.localScale;
        Vector3 target = player.transform.position;

        if (target.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
        }

        transform.localScale = scale;
    }

    protected void ApplyGravity()
    {
        movement = new Vector2(0, verticalSpeed);
        transform.Translate(movement * 2 * Time.deltaTime);
    }

    public void PlayerListener(bool playerUpdate)
    {
        playerFound = playerUpdate;
    }
}
