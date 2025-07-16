using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking = false;
    public bool didUpAttackGround = false;
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
        }

    }

    public void UpAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isAttacking && !didUpAttackGround)
        { 
            didUpAttackGround = true;
            isAttacking = true;
            Debug.Log("Up Attack Pt. 1");
        }
    }

    public void CallResetHitbox()
    {
        BroadcastMessage("ResetHitbox");
    }
}
