using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float damage = 10;
    public float speed = 3;
    public float lifetime = 3;
    private float timer = 0;

    private void Start()
    {
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.position += (-transform.right) * Time.deltaTime * speed;

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
