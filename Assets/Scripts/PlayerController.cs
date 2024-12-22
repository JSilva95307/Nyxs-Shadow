using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region functionality Vars
    bool canGoUpOrDown = false;
    public Rigidbody2D rb;
    public PlayerInputs controls;
    #endregion

    #region Stats
    public float moveSpeed;
    #endregion

    #region Inputs
    InputAction move;
    InputAction jump;
    InputAction primary;
    InputAction secondary;
    InputAction ability1;
    InputAction ability2;
    #endregion

    Vector2 playerVel = Vector2.zero;
    // Awake executes only once you start to load the game
    private void Awake()
    {
        controls = new PlayerInputs();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       playerVel = move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = playerVel.x * moveSpeed;
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

        move.Enable();
        jump.Enable();
        primary.Enable();
        secondary.Enable();
        ability1.Enable();
        ability2.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        primary.Disable();
        secondary.Disable();
        ability1.Disable();
        ability2.Disable();
    }
    #endregion

}
