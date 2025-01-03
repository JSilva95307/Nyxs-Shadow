using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region functionality Vars
    //bool canGoUpOrDown = false;
    public Rigidbody2D rb;
    public PlayerInputs controls;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;
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
    public float coyoteTime;
    public float jumpForce;
    public float jumpBufferTime;
    public bool isGrounded;
    private bool canDash;
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

    Vector2 playerVel = Vector2.zero;
    // Awake executes only once you start to load the game
    private void Awake()
    {
        canDash = true;
        controls = new PlayerInputs();
        testCooldown = gameObject.AddComponent<Cooldowns>();
        testCooldown.SetCooldown(5f);
        currentWeapon = "ESword";
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerVel = move.ReadValue<Vector2>();
        if (cooldownActive)
            Debug.Log(testCooldown.GetProgress().ToString());
        if (Input.GetKeyDown(KeyCode.U))
        {
            animator.SetBool(currentWeapon, false);
            animator.SetBool("ESword", true);
            currentWeapon = "ESword";
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            animator.SetBool(currentWeapon, false);
            animator.SetBool("EGun", true);
            currentWeapon = "EGun";
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool(currentWeapon, false);
            animator.SetBool("ESpear", true);
            currentWeapon = "ESpear";
        }
        if (Input.GetKeyDown(KeyCode.P))

        {
            animator.SetBool(currentWeapon, false);
            animator.SetBool("ETonfa", true);
            currentWeapon = "ETonfa";
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        rb.linearVelocityX = playerVel.x * movementSpeed;
        if (isGrounded && jump.IsPressed())
        {
            Debug.Log("Jumped!");
            rb.linearVelocityY = (jumpForce);
        }
        if (canDash && dash.IsPressed())
        {
            testCooldown.StartCooldown(cdEnded);
            cooldownActive = true;
            canDash = false;
        }
    }

    private void cdEnded()
    {
        cooldownActive = false;
        canDash = true;
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

}
