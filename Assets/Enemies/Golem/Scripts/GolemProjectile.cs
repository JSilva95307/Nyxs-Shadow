using UnityEngine;

public class GolemProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 3;
    [SerializeField] private float lifetime = 3;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += transform.right * Time.deltaTime * speed;

        if (timer > lifetime)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            other.GetComponent<Health>().TakeDamage(damage);

        if (other.gameObject.tag != "Enemy")
            Destroy(gameObject);
    }
}
