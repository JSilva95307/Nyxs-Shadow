using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    private Collider2D collider;
    private bool playerOnPlatform;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)
        {
            //disable collision between platform and player here
        }
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player  = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            playerOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }
}
