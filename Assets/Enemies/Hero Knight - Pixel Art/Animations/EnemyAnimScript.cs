using UnityEngine;

public class EnemyAnimScript : MonoBehaviour
{

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            animator.Play("Attack1");
        }
    }
}
