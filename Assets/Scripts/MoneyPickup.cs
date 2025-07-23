using System.Collections;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public float speed;
    public float value;
    public float waitTime;

    public float collectDist;

    private bool move = false;

    private void Start()
    {
        StartCoroutine(MoveToPlayer());
    }


    private void Update()
    {
        if (move /*&& Vector2.Distance(transform.position, PlayerManager.Instance._playerController.playerCenter.position) >= collectDist*/)
        {
            transform.position = Vector2.LerpUnclamped(transform.position, PlayerManager.Instance._playerController.playerCenter.position, speed * Time.deltaTime);
        }
        
        if (move && Vector2.Distance(transform.position, PlayerManager.Instance._playerController.playerCenter.position) <= collectDist)
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawLine(transform.position, PlayerManager.Instance._playerController.playerCenter.position);
    //}
}
