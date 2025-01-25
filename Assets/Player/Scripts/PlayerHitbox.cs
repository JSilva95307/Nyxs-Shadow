using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public bool active;
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        active = gameObject.activeSelf;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Deal Damage
        if (other.gameObject.TryGetComponent(out Health enemyHealth))
        {
            enemyHealth.TakeDamage(10f);
        }
    }
}
