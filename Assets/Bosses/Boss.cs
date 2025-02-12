using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;

    bool isFlipped = false;
    public BoxCollider2D primary;
    public float primaryDamage;
    public Transform spawnLocation;
    //do projectile attack functionality
    public BaseProjectile projectile;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    
    public void PrimaryAttackActive()
    {
        primary.enabled = true;
    }

    public void PrimaryDisabled()
    {
        primary.enabled = false;
    }

    public void FireProjectile()
    {
        projectile.SetDirection(transform.right);
        Instantiate(projectile, spawnLocation.position, spawnLocation.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(primaryDamage);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        primary.enabled = false;
        //secondary.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
