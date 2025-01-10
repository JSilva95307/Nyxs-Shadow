using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    #region functionality Vars
    //bool canGoUpOrDown = false;
    public Rigidbody2D rb;
    public PlayerInputs controls;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public Animator animator;

    public Collider2D gCheck;
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
    public bool isGrounded;
    private bool canDash;

    public float coyoteTime;
    public float coyoteTimeCounter;

    public float jumpBufferTime;
    public float failedJumpTime;

    bool bufferJumpToProcess = false;
    [Space(20)]
    #endregion

    #region Gameplay Stats
    [Header("Gameplay Vars")]
    public float attackBufferTime;
    public Health playerHealth;
    Cooldowns testCooldown;
    bool cooldownActive = false;
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

    Vector2 playerVel = Vector2.zero;
    // Awake executes only once you start to load the game
    private void Awake()
    {
        canDash = true;
        controls = new PlayerInputs();
        testCooldown = gameObject.AddComponent<Cooldowns>();
        testCooldown.SetCooldown(5f);
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
        if (cooldownActive)
            Debug.Log(testCooldown.GetProgress().ToString());
        if (primary.IsPressed())
        {
            animator.SetTrigger(currentWeapon);
        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            if( failedJumpTime - Time.time < jumpBufferTime  && bufferJumpToProcess)
            {
                DoJump();
                bufferJumpToProcess = false;
            }
        }
        else
            coyoteTimeCounter -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = playerVel.x * movementSpeed;
        if (canDash && dash.IsPressed())
        {
            testCooldown.StartCooldown(cdEnded);
            cooldownActive = true;
            canDash = false;
        }
    }

    #region Attack Functions

    private void cdEnded()
    {
        cooldownActive = false;
        canDash = true;
    }

    #endregion

    #region Movement Functions

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
            return;
            
        isGrounded = false;
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

    #endregion

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
        else if(helmet.armorSet == chestplate.armorSet && helmet.armorSet == greaves.armorSet)
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
