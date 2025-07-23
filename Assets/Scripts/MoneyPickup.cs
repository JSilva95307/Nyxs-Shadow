using System.Collections;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public float speed;
    public float value;
    public float waitTime;

    private bool move = false;

    private void Start()
    {
        StartCoroutine(MoveToPlayer());
    }


    private void Update()
    {
        if (move && Vector2.Distance(transform.position, PlayerManager.Instance.player.transform.position) >= 0.2f)
        {
            transform.position = Vector2.Lerp(transform.position, PlayerManager.Instance.player.transform.position, speed * Time.deltaTime);
        }
        else if(move && Vector2.Distance(transform.position, PlayerManager.Instance.player.transform.position) <= 0.2f)
        {
            Collect();
        }

    }

    public IEnumerator MoveToPlayer()
    {
        yield return new WaitForSeconds(waitTime);
        move = true;
    }

    private void Collect()
    {
        PlayerManager.Instance._playerController.money += value;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLIDED");

        if (other.gameObject.tag == "Player")
        {
            PlayerManager.Instance._playerController.money += value;
            Debug.Log("Arrived");
            Destroy(gameObject);
        }
    }
}
