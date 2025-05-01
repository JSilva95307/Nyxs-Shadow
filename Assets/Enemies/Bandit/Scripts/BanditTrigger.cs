using UnityEngine;

public class BanditTrigger : MonoBehaviour
{
    public BanditBehavior bandit;
    public LayerMask playerLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            bandit.PersuePlayer();
            Destroy(gameObject);
        }
    }
}
