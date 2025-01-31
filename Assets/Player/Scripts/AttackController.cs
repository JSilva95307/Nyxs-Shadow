using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking = false;
    //public Vector2 lungeDist;
    private PlayerController controller;

    public static AttackController instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }


    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isAttacking)
        {
            isAttacking = true;
            //controller.transform.position += controller.transform.up * Time.deltaTime * PlayerManager.Instance.lungeDist.x;
            //controller.rb.AddForce(PlayerManager.Instance.lungeDist * Time.deltaTime);
            //controller.rb.linearVelocity = PlayerManager.Instance.lungeDist;
            //StartCoroutine(Lunge());
            //Debug.Log("Attacked");
            //Debug.Log("PlayerManager.Instance.lungeDist");
        }

    }

    private void Update()
    {
        if (isAttacking)
        {
            
        }
    }


    IEnumerator Lunge()
    {
        float elapsedTime = 0;
        float waitTime = 1f;
        Vector2 pos = transform.position;
        
        while (elapsedTime < waitTime)
        {
            transform.position = Vector2.Lerp(pos, pos + PlayerManager.Instance.lungeDist, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.position = pos + PlayerManager.Instance.lungeDist;
        yield return null;

    }

    public void CallResetHitbox()
    {
        BroadcastMessage("ResetHitbox");
    }
}
