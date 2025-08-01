using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region functionality Vars
    public Rigidbody2D rb;
    public PlayerInputs controls;
    public PlayerHUD playerHUD;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public LayerMask grappleLayer;
    public LayerMask wallLayer;
    public Animator animator;
    public Transform playerCenter;

    //public Collider2D gCheck;
    private Vector2 grapplePos;
    private Vector2 grappleTime;
    private bool grappling;
    public bool attacking;
    #endregion

    #region Inputs
    [Header("Player's Inputs")]
    InputAction move;
    InputAction jump;
    InputAction primary;
    InputAction secondary;
    InputAction ability1;
    InputAction ability2;
    InputAction dash;
    InputAction grapple;
    InputAction interact;
    InputAction openIventory;
    InputAction groundPound;
    [Space(20)]
    #endregion

    #region Movement Numbdies
    [Header("Player Movement Vars")]
    public float movementSpeed;
    public float fallingspeedCap;
    public float apexSpeedBoost;
    public bool isGrounded;
    public bool facingRight;
    public bool isDashing;
    public bool canDash;

    [Space(10)]

    public float coyoteTime;
    public float coyoteTimeCounter;

    [Space(10)]

    public float jumpForce;
    public float jumpBufferTime;
    public float failedJumpTime;
    bool bufferJumpToProcess = false;

    [Space(10)]

    public float dashStr;
    public float dashLength;

    [Space(10)]

    public float grappleCD;
    public bool grappleIsFar;
    public float grappleLaunchPower;

    [Space(10)]

    public bool canWallJump;
    public float wallJumpBuffer;
    public float wallDistance;
    public float wallJumpDuration;
    public bool wallJumping;
    bool isWallSliding;

    [Space(10)]

    RaycastHit2D wallCheck;
    float jumpTime;
    public bool jumpingRight;
    [Space(20)]
    #endregion

    #region Gameplay Stats
    [Header("Gameplay Vars")]
    public float attackBufferTime;
    public Health playerHealth;
    public Currency playerMoney;
    public float money;
    Cooldowns dashCooldown;
    Cooldowns grappleCooldown;
    bool TouchedGround;
    [Space(20)]
    #endregion

    #region Camera Controls
    [Header("Camera")]
    public Camera cam;
    public float maxDistance;
    public float minDistance;
    public float followSpeed;
    [Space(20)]
    #endregion

    #region Weapons
    [Header("Weapons")]
    public WeaponStats sword;
    public WeaponStats gun;
    public WeaponStats tonfa;
    public WeaponStats spear;
    string currentWeapon;
    #endregion

    #region Armor
    [Header("Armor")]
    public Armor helmet;
    public Armor chestplate;
    public Armor greaves;
    public List<Armor> armorList;

    [Space(20)]
    #endregion

    #region miscellaneous
    public List<GameObject> grapplePoints;
    Vector2 playerVel = Vector2.zero;
    #endregion


    public PlayerStates state;
    public GameObject groundCheck;
    public Vector2 boxSize;
    public float castDistance;

    public bool isOnPlatform;
    private BoxCollider2D playerCollider;

    // Awake executes only once you start to load the game
    private void Awake()
    {
        grappleIsFar = true;
        canDash = true;
        controls = new PlayerInputs();
        dashCooldown = gameObject.AddComponent<Cooldowns>();
        dashCooldown.SetCooldown(dashLength);
        grappleCooldown = gameObject.AddComponent<Cooldowns>();
        grappleCooldown.SetCooldown(grappleCD);
        currentWeapon = "SwoPrim";
        Physics.IgnoreLayerCollision(0, 6);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrounded = false;
        playerHealth.SetCurrentHealth(playerHealth.GetMaxHealth());
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            currentWeapon = "SwoPrim";
        if (Input.GetKeyDown(KeyCode.I))
            currentWeapon = "GunPrim";
        if (Input.GetKeyDown(KeyCode.O))
            currentWeapon = "SpePrim";
        if (Input.GetKeyDown(KeyCode.P))
            currentWeapon = "TonPrim";
        
        if (Input.GetKeyDown(KeyCode.M))
            GetComponent<AfterimageGenerator>().Play();
        if (Input.GetKeyDown(KeyCode.N))
            GetComponent<AfterimageGenerator>().Stop();


        GroundCheck();

        playerVel = move.ReadValue<Vector2>();
        
        Vector3 temp = transform.localScale;
        if (playerVel.x > 0f)
        {
            facingRight = true;
            temp.x = 1;
            transform.localScale = temp;
        }
        else if (playerVel.x < 0f)
        {
            facingRight = false;
            temp.x = -1;
            transform.localScale = temp; // Flips the player
        }

        if (primary.IsPressed())
        {
            //animator.SetTrigger(currentWeapon);
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            if (Time.time - failedJumpTime  < jumpBufferTime && bufferJumpToProcess)
            {
                DoJump();
                bufferJumpToProcess = false;
            }
        }
        else
            coyoteTimeCounter -= Time.deltaTime;
        
        //Debug.Log((Distance2D(transform.position, grapplePos)));
        if ((Distance2D(transform.position, grapplePos)) <= 1f && grappling)
        {
            grappleIsFar = true;
            grappling = false;
            if (rb.linearVelocityY > 8)
                rb.linearVelocityY = 8;
            Debug.Log(rb.linearVelocityY);
        }

        if(playerVel.x != 0)
            animator.SetBool("Running", true);
        else
            animator.SetBool("Running", false);

        if (Input.GetKey(KeyCode.S))
        {
            state = PlayerStates.Crouching;
        }
        else
        {
            state = PlayerStates.Standing;
        }

    }


    private void FixedUpdate()
    {
        rb.linearVelocityX = (playerVel.x + (PlayerManager.Instance.lungeDist.x * transform.localScale.x)) * movementSpeed ;
        if (PlayerManager.Instance.lungeDist.y >= 1)
            rb.linearVelocityY = (playerVel.y + (PlayerManager.Instance.lungeDist.y * transform.localScale.y));
        else
        {
            PlayerManager.Instance.lungeDist.y = 0;
        }
            
        //rb.MovePosition(transform.position + );

        if (!canDash)
        {
            rb.linearVelocityY = 0;
            if (facingRight)
            {
                rb.linearVelocityX = dashStr;
            }
            else
            {
                rb.linearVelocityX = -dashStr;
            }
        }

        if (grappleIsFar && grappling)
        {
            //transform.position = grapplePos;
            Debug.Log((grapplePos - (Vector2)(transform.position)).normalized * grappleLaunchPower);
            Vector2 powah = (grapplePos - (Vector2)(transform.position)).normalized * grappleLaunchPower;
            rb.linearVelocity = powah * grappleLaunchPower;
            //grappling = false;
            grappleCooldown.StartCooldown(GrappleCooldown);
        }

        //wall jump
        if (facingRight)
        {
            wallCheck = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayer);
            Debug.DrawRay(transform.position, new Vector2(wallDistance, 0), Color.black);
        }
        else
        {
            wallCheck = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            Debug.DrawRay(transform.position, new Vector2(-wallDistance, 0), Color.red);
        }

        if (wallCheck && !isGrounded)
        {
            isWallSliding = true;
            jumpTime = Time.time + wallJumpBuffer;
            canWallJump = true;
        }
        else if (jumpTime < Time.time)
        {
            isWallSliding = false;
            canWallJump = false;
        }

        if (wallJumping && wallJumpDuration >= 0)
        {
            if (facingRight)
                rb.linearVelocity = new Vector2(-6, 6);
            else
                rb.linearVelocity = new Vector2(6, 6);
            wallJumpDuration -= Time.deltaTime;
        }

        if (wallJumpDuration < 0)
        {
            wallJumping = false;
            move.Enable();
        }

    }

    #region Attack Functions

    public void SetAttacking()
    {
        if(attacking == false)
            attacking = true;
        else
            attacking = false;
    }


    private void DashCooldown()
    {
        if (!isGrounded)
            TouchedGround = false;
        canDash = true;
        rb.gravityScale = 2;

        if (!attacking)
        {
            controls.Enable();
        }

        Physics2D.IgnoreLayerCollision(8, 6,  false);
        GetComponent<AfterimageGenerator>().Stop();
        playerHealth.invulnerable = false;
        isDashing = false;
    }

    private void GrappleCooldown()
    {
        grappling = false;
    }

    #endregion

    #region Movement Functions

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (((1 << other.gameObject.layer) & groundLayer) != 0)
    //    {
    //        isGrounded = true;
    //        TouchedGround = true;
    //    }
    //    if (((1 << other.gameObject.layer) & grappleLayer) != 0)
    //    {
    //        grapplePoints.Add(other.gameObject);
    //    }

    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
    //        return;
    //    if (((1 << collision.gameObject.layer) & groundLayer) != 0)
    //        isGrounded = false;
    //    if (((1 << collision.gameObject.layer) & grappleLayer) != 0)
    //        grapplePoints.Remove(collision.gameObject);
    //}

    private void GroundCheck()
    {
        if (Physics2D.BoxCast(groundCheck.transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            isGrounded = true;

            if(TouchedGround == false)
                TouchedGround = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (state == PlayerStates.Crouching)
        {
            Drop();
            return;
        }
        
        if (coyoteTimeCounter > 0f && ctx.performed)
        {
            if(jump.enabled)
                DoJump();
        }
        else if (ctx.performed)
        {
            failedJumpTime = Time.time;
            bufferJumpToProcess = true;
        }
        if (ctx.canceled && rb.linearVelocityY > 0)
        {
            rb.linearVelocityY = 0;
            coyoteTimeCounter = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = true;
        }
    }

    private void Drop()
    {
        if (isGrounded && isOnPlatform && playerCollider.enabled)
        {
            StartCoroutine(DisablePlayerCollider(.5f));
        }

    }

    private IEnumerator DisablePlayerCollider(float disableTime)
    {
        //playerCollider.enabled = false;
        //playerCollider.excludeLayers.
        Physics2D.IgnoreLayerCollision(8, 12, true);
        yield return new WaitForSeconds(disableTime);
        //playerCollider.enabled = true;
        Physics2D.IgnoreLayerCollision(8, 12, false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
        }
    }

    private void DoJump()
    {
        rb.linearVelocityY = (jumpForce);
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canDash && TouchedGround)
        {
            dashCooldown.StartCooldown(DashCooldown);
            if (!isGrounded)
                TouchedGround = false;
            canDash = false;
            isDashing = true;
            GetComponent<AfterimageGenerator>().Play();
            controls.Disable();
            rb.gravityScale = 0;
            Physics2D.IgnoreLayerCollision(8, 6, true); // Ignores collision between the player and enemy layer when dashing
            playerHealth.invulnerable = true; //Makes the player invincible while dashing
        }
    }

    public void Grapple(InputAction.CallbackContext ctx)
    {
        int index = 0;
        if (ctx.performed && grapplePoints.Count > 0)
        {
            double curDistance = Distance2D(grapplePoints[0].transform.position, transform.position);
            if (grapplePoints.Count > 1)
            {
                double lastDistance = 0;
                for (int i = 0; i < grapplePoints.Count; ++i)
                {
                    curDistance = Distance2D(grapplePoints[i].transform.position, transform.position);
                    if (i == 0)
                    {
                        lastDistance = curDistance;
                    }
                    else if (lastDistance > curDistance)
                    {
                        index = i;
                    }
                }
            }
            grappleLaunchPower = (float)(curDistance / grappleCD);
            grappling = true;
            grapplePos = grapplePoints[index].transform.position;
        }
    }

    private double Distance2D(Vector2 start, Vector2 end)
    {
        return Math.Sqrt(Math.Pow(end.x - start.x, 2) + Math.Pow(end.y - start.y, 2));
    }
    #endregion

    public void DoWallJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canWallJump)
        {
            wallJumpDuration = 0.2f;
            wallJumping = true;
            move.Disable();
        }
    }

    public void GroundPound(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && !isGrounded)
        {
            rb.linearVelocityY = -20;
        }
    }

    public void DisableMovement()
    {
        move.Disable();
        jump.Disable();
    }

    public void EnableMovement()
    {
        move.Enable();
        jump.Enable();
    }

    #region Input Boilerplate
    private void OnEnable()
    {
        move = controls.Player.Move;
        jump = controls.Player.Jump;
        primary = controls.Player.Primary;
        secondary = controls.Player.Secondary;
        ability1 = controls.Player.Ability1;
        ability2 = controls.Player.Ability2;
        dash = controls.Player.Dash;
        grapple = controls.Player.Grapple;
        interact = controls.Player.Interact;
        openIventory = controls.Player.OpenInventory;
        groundPound = controls.Player.GroundPound;

        move.Enable();
        jump.Enable();
        primary.Enable();
        secondary.Enable();
        ability1.Enable();
        ability2.Enable();
        dash.Enable();
        grapple.Enable();
        interact.Enable();
        openIventory.Enable();
        groundPound.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        primary.Disable();
        secondary.Disable();
        ability1.Disable();
        ability2.Disable();
        dash.Disable();
        grapple.Disable();
        interact.Disable();
        openIventory.Disable();
        groundPound.Disable();
    }

    public void DisableAllControls()
    {
        move.Disable();
        jump.Disable();
        primary.Disable();
        secondary.Disable();
        ability1.Disable();
        ability2.Disable();
        dash.Disable();
        grapple.Disable();
        interact.Disable();
        openIventory.Disable();
        groundPound.Disable();
        //controls.Disable();
    }

    public void EnableAllControls()
    {
        move.Enable();
        jump.Enable();
        primary.Enable();
        secondary.Enable();
        ability1.Enable();
        ability2.Enable();
        dash.Enable();
        grapple.Enable();
        interact.Enable();
        openIventory.Enable();
        groundPound.Enable();
        //controls.Enable();
    }
    #endregion

    #region Armor Functions
    public void EquipArmor(Armor armor)
    {
        //Remove stat increases of current armor and add new armor to the slot
        ArmorType type = armor.armorType;
        switch (type)
        {
            case ArmorType.Helmet:
                if (helmet != null)
                {
                    Debug.Log("Stats Removed");
                    //Remove stat boosts from current armor

                    playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() - helmet.health);
                    movementSpeed -= helmet.speed;

                }

                if(helmet == armor)
                {
                    helmet = null;
                    Debug.Log("Helmet Unequipped");
                    return; //Unequips the armor if you try to equip the same one twice
                }

                helmet = armor;

                //Add new stats to the player
                Debug.Log("Stats Added");
                playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() + helmet.health);
                movementSpeed += helmet.speed;

                break;
            case ArmorType.Chestplate:
                
                if (chestplate != null)
                {
                    Debug.Log("Stats Removed");
                    //Remove stat boosts from current armor

                    playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() - chestplate.health);
                    movementSpeed -= chestplate.speed;

                }

                if (chestplate == armor)
                {
                    chestplate = null;
                    Debug.Log("Chestplate Unequipped");
                    return; //Unequips the armor if you try to equip the same one twice
                }

                chestplate = armor;

                //Add new stats to the player
                Debug.Log("Stats Added");
                playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() + chestplate.health);
                movementSpeed += chestplate.speed;

                break;
            case ArmorType.Greaves:
                
                if (greaves != null)
                {
                    Debug.Log("Stats Removed");
                    //Remove stat boosts from current armor

                    playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() - greaves.health);
                    movementSpeed -= greaves.speed;

                }

                if (greaves == armor)
                {
                    greaves = null;
                    Debug.Log("Greaves Unequipped");
                    return; //Unequips the armor if you try to equip the same one twice
                }

                greaves = armor;

                //Add new stats to the player
                Debug.Log("Stats Added");
                playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() + greaves.health);
                movementSpeed += greaves.speed;

                break;
        }


        //Check if each piece of armor is the same type and apply a set bonus
        if (helmet == null || chestplate == null || greaves == null)
            return;
        else if (helmet.armorSet == chestplate.armorSet && helmet.armorSet == greaves.armorSet)
        {
            //Use a swich case to determine what set bonus to apply
            ArmorSet set = helmet.armorSet;

            switch (set)
            {
                case ArmorSet.TestSet:
                    Debug.Log("This is the test armor set bonus");
                    break;
            }
        }
    }

    public float FindPercentage(float input, float percentage)
    {
        float p = percentage / 100;

        return input * p;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.transform.position - transform.up * castDistance, boxSize);
    }
}
