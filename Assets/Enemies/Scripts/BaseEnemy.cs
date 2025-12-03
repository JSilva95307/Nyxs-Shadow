using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEditor.PlayerSettings;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Functionality")]
    //animation controller
    public Animator animator;

    [Space(10)]
    [Header("Movement")]
    //variables controlling the enemy's movement
    public float moveSpeed;
    public float gravityStrength;
    public Transform groundCheckPos;
    protected Vector2 movement;
    protected float verticalSpeed;

    [Space(10)]
    [Header("Detection")]
    //variables to detect platform ledges
    public Vector3 ledgeDetectOffset = new Vector3(0f, 0f, 0f);
    public float ledgeDetectSpacing = 1.2f;
    //variables to detect walls
    public Vector3 wallDetectOffset = new Vector3(0f, 0f, 0f);
    public float wallDetectSpacing = 1.4f;

    public Collider2D mainCollision;
    protected Rigidbody2D rb;

    [Space(5)]
    //changes whether or not to show the detection areas for the variables above
    public bool showDetection;

    [Space(10)]
    [Header("Layer Masks")]
    public LayerMask groundLayerMask;
    public LayerMask collisionLayerMask;

    [Space(10)]
    [Header("Money Vars")]
    public bool dropMoney;
    public float dropAmount;
    //public GameObject moneyPickup;
    protected MoneyPickup pickupRef;
    protected List<MoneyPickup> drops;


    [Space(10)]
    [Header("Launch Vars")]
    LaunchScript launcher;
    public float k = 5; //k = excitation constant (lower k (~1-2) for sluggish movement, higher k (~10) for move snappish behavior)

    protected GameObject player;
    protected bool playerFound;
    
    protected bool facingRight;
    protected bool grounded;
    
    protected bool targetSet;
    protected Vector2 targetLocation;

    protected bool dead;

    public abstract void Attack();
    public abstract void Attack2();
    public abstract void Attack3();

    protected virtual void Start()
    {
        drops = new List<MoneyPickup>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        launcher = GetComponent<LaunchScript>();

        if (dropMoney)
        {
            for (int i = 0; i < dropAmount; i++)
            {
                //Debug.Log("spawned pickup");
                GameObject money = Instantiate(GameManager.Instance.moneyPickup);
                pickupRef = money.GetComponent<MoneyPickup>();
                money.transform.position = transform.position;
                money.transform.parent = transform;
                drops.Add(pickupRef);
            }
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (launcher.launchForce > 0)
        {
            launcher.ApplyLaunch();
        }

        if (targetSet == true && Vector2.Distance(targetLocation, transform.position) > 0.25f)
        {
            MoveTo();
        }
        else if (targetSet == true && Vector2.Distance(targetLocation, transform.position) <= 0.25f)
        {
            targetSet = false;
            targetLocation = Vector2.zero;
        }
    }

    protected virtual void FixedUpdate()  {    }

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
        animator.SetTrigger("Die");
        mainCollision.enabled = false;

        if (rb != null)
            rb.gravityScale = 0;

        dead = true;

        for (int i = 0; i < drops.Count; i++)
        {
            drops[i].StartCoroutine(drops[i].MoveToPlayer());
        }
    }

    public void DestroyEnemy()
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
            rotation.y = 0;
            facingRight = true;
        }
        else
        {
            rotation.y = 180;
            facingRight = false;
        }

        transform.localRotation = rotation;
    }

    protected void ApplyGravity()
    {
        movement = new Vector2(0, verticalSpeed);
        transform.Translate(movement * 2 * Time.deltaTime);
    }

    //Returns true if a ledge is detected
    protected bool DetectLedge() 
    {
        Vector3 pos = groundCheckPos.position + ledgeDetectOffset;

        if (transform.rotation.y == 0)//behind
            pos.x -= ledgeDetectSpacing;
        else
            pos.x += ledgeDetectSpacing;

        RaycastHit2D hit = Physics2D.Raycast(pos, -Vector2.up, 0.2f, groundLayerMask);

        if (hit.collider)
            return false;
        else
            return true;
    }

    //Returns true if a wall is detected
    protected bool DetectWall()
    {
        Vector3 pos = groundCheckPos.position + wallDetectOffset;

        if (transform.rotation.y == 0)//behind
            pos.x -= wallDetectSpacing;
        else
            pos.x += wallDetectSpacing;

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.right, 0.2f, groundLayerMask);

        if (hit.collider)
            return true;
        else
            return false;
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
        transform.Translate( Vector2.right * moveSpeed * Time.deltaTime );
    }
    
    public void ChasePlayer()
    {
        Vector3 target = player.transform.position;
        movement = Vector2.right;
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    public void Teleport(Vector2 location)
    {
        //Add some visual effect here

        transform.position = location;
    }

    public void TeleportToPlayer()
    {
        Vector2 target = player.transform.position;
        Vector2 pos = target;
        float offset = 2f;
        int attempts = 0;

        pos.x = target.x + offset;
        pos.y += 2f;


        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.right, 2f, collisionLayerMask);
        RaycastHit2D groundHit = Physics2D.Raycast(pos, -Vector2.up, 5f, groundLayerMask);

        if (hit.collider == null && groundHit.collider != null)
            transform.position = pos;
    }

    public Transform GetTransform() { return this.transform; }

    public void FlipEnemy()
    {
        Vector3 scale = transform.localScale;
        Quaternion rotation = transform.rotation;

        if (facingRight)
        {
            rotation.y = 180;
            facingRight = false;
        }
        else
        {
            rotation.y = 0;
            facingRight = true;
        }
        Debug.Log("Called Flip");
        transform.rotation = rotation;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float _moveSpeed)
    {
        moveSpeed = _moveSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (showDetection)
        {
            //Draw Ledge Detection
            Vector3 v = groundCheckPos.position + ledgeDetectOffset;
            Vector3 v2 = groundCheckPos.position + ledgeDetectOffset;

            v.x += ledgeDetectSpacing;
            v2.x += ledgeDetectSpacing;
            v.y -= 0.2f;
            Gizmos.DrawLine(v, v2);

            v.x -= ledgeDetectSpacing * 2;
            v2.x -= ledgeDetectSpacing * 2;
            Gizmos.DrawLine(v, v2);
            //

            //Draw Wall Detection
            v = groundCheckPos.position + wallDetectOffset;
            v2 = groundCheckPos.position + wallDetectOffset;

            v.x += wallDetectSpacing;
            v2.x += wallDetectSpacing;
            v2.x += 0.2f;
            Gizmos.DrawLine(v, v2);

            v.x -= wallDetectSpacing * 2;
            v2.x -= wallDetectSpacing * 2;
            Gizmos.DrawLine(v, v2);
        }
    }
}
