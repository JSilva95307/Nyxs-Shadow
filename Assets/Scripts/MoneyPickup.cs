using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    public GameObject pickup;

    public float speed;
    public float value;
    public float waitTime;

    public float collectDist;

    private bool move = false;
    private bool spawned = false;


    public float minRandDist;
    public float maxRandDist;

    private Vector2 randDir = Vector2.zero;
    private float randDist;
    private Vector3 randLoc = Vector3.zero;


    private void Start()
    {
        pickup.SetActive(false);

        randDir.x = Random.Range(-360, 360);
        randDir.y = Random.Range(-360, 360);
        randDir.Normalize();

        randDist = Random.Range(minRandDist, maxRandDist);

        randLoc = randDist * randDir;
    }


    private void Update()
    {
        if (spawned)
        {
            transform.parent = null;
            pickup.SetActive(true);

            transform.position = Vector2.Lerp(transform.position, randLoc, speed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, Vector3.Scale(transform.position, randLoc), speed * Time.deltaTime);
        }
        
        
        if (move)
        {
            spawned = false;
            transform.position = Vector2.Lerp(transform.position, PlayerManager.Instance._playerController.playerCenter.position, speed * Time.deltaTime);
        }
        
        if (move && Vector2.Distance(transform.position, PlayerManager.Instance._playerController.playerCenter.position) <= collectDist)
        {
            Collect();
        }
    }

    public IEnumerator MoveToPlayer()
    {
        spawned = true;
        randLoc = transform.position + randLoc;

        yield return new WaitForSeconds(waitTime);
        move = true;
    }

    private void Collect()
    {
        PlayerManager.Instance._playerController.money += value;
        Debug.Log("Collected");
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + randLoc);
    }
}
