using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float gravityStrength;
    public Transform groundCheckPos;
    protected Vector2 movement;
    protected float verticalSpeed;

    [Space(10)]
    [Header("Detection")]
    public Vector3 ledgeDetectOffset = new Vector3(0f, 0f, 0f);
    public float ledgeDetectSpacing = 1.2f;

    public Vector3 wallDetectOffset = new Vector3(0f, 0f, 0f);
    public float wallDetectSpacing = 1.4f;
    public Collider2D mainCollision;

    [Space(5)]
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
    public Vector2 launchDir;
    public float k = 3; //k = excitation constant (lower k (~1-2) for sluggish movement, higher k (~10) for move snappish behavior)

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
        
        if (dropMoney)
        {
            for (int i = 0; i < dropAmount; i++)
            {
                Debug.Log("spawned pickup");
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
        if (Input.GetKeyDown(KeyCode.L))
        {
            launchDir.y += 10f;
            Debug.Log("Launched");
        }


        launchDir.x = Mathf.Lerp(launchDir.x, 0f, (float)(1 - Mathf.Exp(-k * Time.deltaTime)));
        launchDir.y = Mathf.Lerp(launchDir.y, 0f, (float)(1 - Mathf.Exp(-k * Time.deltaTime)));

        if(launchDir.x > 0.5f)
        {
            launchDir.x = 0;
        }
        
        if (launchDir.y > 0.5f)
        {
            launchDir.y = 0;
        }
    }

    protected virtual void FixedUpdate()
    {
        ApplyLaunch();


    }

    private void ApplyLaunch()
    {
        if(launchDir != Vector2.zero)
        {
            Vector3 temp = Vector3.zero;
            float tempX = (movement.x + (launchDir.x + transform.localScale.x)) * moveSpeed;
            float tempY = (movement.y + (launchDir.y + transform.localScale.y)) * moveSpeed;

            temp.x = tempX;
            temp.y = tempY;

            transform.position = temp;
        }
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
            facingRight = true;
        }
        else
        {
            //scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            rotation.y = 180;
            facingRight = false;
        }

        //transform.localScale = scale;
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
        //if (transform.position.x > targetLocation.x)
        //    movement = new Vector2(-1, 0);
        //else
        //    movement = new Vector2(1, 0);
        transform.Translate( Vector2.right * moveSpeed * Time.deltaTime );
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

        //while (true)
        //{
        //    attempts++;
        //    pos.x = target.x + offset;
        //    pos.y += 2f;
        //    Collider2D hit = Physics2D.OverlapCircle(target, 1, collisionLayerMask);

        //    if (hit == null)
        //    {
        //        Teleport(pos);
        //        Debug.Log(offset);
        //        break;
        //    }

        //    pos.x = target.x - offset;
        //    hit = Physics2D.OverlapCircle(target, 1, collisionLayerMask);
            
        //    if (hit == null)
        //    {
        //        Teleport(pos);
        //        Debug.Log(offset);
        //        break;
        //    }

        //    offset += 2;
        //    Debug.Log(offset);

        //    if (attempts > 5)
        //    {
        //        Debug.Log("failed teleport");
        //        break;
        //    }
        //}
        
    }

    public Transform GetTransform() { return transform; }

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
            //
        }
    }
}
