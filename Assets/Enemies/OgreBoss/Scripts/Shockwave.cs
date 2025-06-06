using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private float damage = 10;
    [SerializeField] private float speed = 3;
    [SerializeField] private float lifetime = 3;
    private float timer = 0;

    public bool facePlayer;

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

    protected void FacePlayer()
    {
        Vector3 target = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (target.x < transform.position.x)
        {
            transform.Rotate(transform.up, 180f);
        }
    }
}
