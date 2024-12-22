using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region functionality Vars
    bool canGoUpOrDown = false;
    public Rigidbody2D rb;
    public PlayerInputs controls;
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
        
    }


    #region Input Boilerplate
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
    #endregion

}
