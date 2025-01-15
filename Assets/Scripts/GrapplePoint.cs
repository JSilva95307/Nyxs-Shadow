using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    public SpriteRenderer anchorPoint;

    public float SPEED;

    public GameObject player;

    public Vector2 endPosition;

    //when player enters
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make an arrow that points at them until they leave the trigger area.
        player = collision.gameObject;
        anchorPoint.enabled = true;
    }

    //when player exits
    private void OnTriggerExit2D(Collider2D collision)
    {
        //fade out the arrow once the player is out of range and remove the player reference
        player = null;
        anchorPoint.enabled = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anchorPoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float angle = (Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg) - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, SPEED * Time.deltaTime);
            
        }
    }
}
