using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region functionality Vars
    public Rigidbody2D rb;
    public PlayerInputs controls;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public LayerMask grappleLayer;
    public LayerMask wallLayer;
    public Animator animator;

    public Collider2D gCheck;
    private Vector2 grapplePos;
    private Vector2 grappleTime;
    private bool grappling;
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
    public float jumpForce;
    public float dashStr;
    public bool isGrounded;
    private bool canDash;
    public bool facingRight;

    public float coyoteTime;
    public float coyoteTimeCounter;

    public float jumpBufferTime;
    public float failedJumpTime;

    bool bufferJumpToProcess = false;
    public float dashCD;
    public float grappleCD;
    public bool grappleIsFar;
    public float grappleLaunchPower;
    public bool canWallJump;

    public float wallJumpBuffer;
    public float wallDistance;
    bool isWallSliding;
    RaycastHit2D wallCheck;
    float jumpTime;

    [Space(20)]
    #endregion

    #region Gameplay Stats
    [Header("Gameplay Vars")]
    public float attackBufferTime;
    public Health playerHealth;
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

    // Awake executes only once you start to load the game
    private void Awake()
    {
        grappleIsFar = true;
        canDash = true;
        controls = new PlayerInputs();
        dashCooldown = gameObject.AddComponent<Cooldowns>();
        dashCooldown.SetCooldown(dashCD);
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

        if (Input.GetKeyDown(KeyCode.Q))
            EquipArmor(armorList[0]);

        playerVel = move.ReadValue<Vector2>();
        if (playerVel.x > 0f)
            facingRight = true;
        else if (playerVel.x < 0f)
            facingRight = false;

        if (primary.IsPressed())
        {
            animator.SetTrigger(currentWeapon);
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            if (failedJumpTime - Time.time < jumpBufferTime && bufferJumpToProcess)
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
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = playerVel.x * movementSpeed;
        if (!canDash)
        {
            rb.linearVelocityY = 0;
            if (facingRight)
            {
                rb.linearVelocityX = 12f;
            }
            else
            {
                rb.linearVelocityX = -12f;
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
            wallCheck = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), -wallDistance, wallLayer);
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
    }

    #region Attack Functions

    private void DashCooldown()
    {
        if (!isGrounded)
            TouchedGround = false;
        canDash = true;
        rb.gravityScale = 2;
        controls.Enable();
    }

    private void GrappleCooldown()
    {
        grappling = false;
    }

    #endregion

    #region Movement Functions

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
            TouchedGround = true;
        }
        if (((1 << other.gameObject.layer) & grappleLayer) != 0)
        {
            grapplePoints.Add(other.gameObject);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
            return;
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
            isGrounded = false;
        if (((1 << collision.gameObject.layer) & grappleLayer) != 0)
            grapplePoints.Remove(collision.gameObject);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (coyoteTimeCounter > 0f)
        {
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
            controls.Disable();
            rb.gravityScale = 0;
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
            rb.linearVelocity = new Vector2(10, jumpForce);
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
                    //helmet.attack;
                    //helmet.defense;
                    movementSpeed -= helmet.speed;

                }

                helmet = armor;

                //Add new stats to the player
                Debug.Log("Stats Added");
                playerHealth.SetCurrentHealth(playerHealth.GetCurrentHealth() + helmet.health);
                //Set Attack when stat is added
                //Are we adding a defense stat?
                movementSpeed += helmet.speed;

                break;
            case ArmorType.Chestplate:
                chestplate = armor;
                break;
            case ArmorType.Greaves:
                greaves = armor;
                break;
        }


        //Check if each piece of armor is the same type and apply a set bonus
        if (helmet == null || chestplate == null || greaves == null)
            return;
        else if (helmet.armorSet == chestplate.armorSet && helmet.armorSet == greaves.armorSet)
        {
            //Use a swich case to determine what set bonus to apply
        }
    }

    public float FindPercentage(float input, float percentage)
    {
        float p = percentage / 100;

        return input * p;
    }
    #endregion
}
