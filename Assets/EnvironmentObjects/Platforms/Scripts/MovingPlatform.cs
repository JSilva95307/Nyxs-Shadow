using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    public int i;
    
    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;

            if(i == points.Length)
                i = 0;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        Debug.Log("Moving");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
