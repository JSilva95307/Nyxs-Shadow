using UnityEditor.Tilemaps;
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
    protected bool targetSet;
    protected Vector2 targetLocation;

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
        Quaternion rotation = transform.rotation;

        if (target.x > transform.position.x)
        {
            //scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            rotation.y = 0;
        }
        else
        {
            //scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            rotation.y = 180;
        }

        //transform.localScale = scale;
        transform.localRotation = rotation;
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

    public void SetTarget(Vector2 targetLoc)
    {
        targetLocation = targetLoc;
        targetSet = true;
    }

    protected void MoveTo()
    {
        if (transform.position.x > targetLocation.x)
            movement = new Vector2(-1, 0);
        else
            movement = new Vector2(1, 0);
        transform.Translate( movement * moveSpeed * Time.deltaTime );
    }
    
    public void ChasePlayer()
    {
        Vector3 target = player.transform.position;

        movement = Vector2.right;
        //if (transform.position.x > target.x)
        //{
        //    Debug.Log("Moving left");
        //}
        //else
        //{
        //    movement = Vector2.right;
        //    Debug.Log("Moving right");
        //}
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public Transform GetTransform() { return transform; }
}
