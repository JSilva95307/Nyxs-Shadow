using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking = false;
    public bool didUpAttackGround = false;
    public bool downAttackLv1 = false;
    public bool downAttackLv2 = false;
    public bool downAttackLv3 = false;
    public bool downAttacking = false;
    public bool dirInputting = false;
    public bool airUpAttack = false;
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
        Debug.Log("Charging for primary bool: " + dirInputting);
        if (ctx.performed && !isAttacking && !didUpAttackGround && !downAttacking && !dirInputting)
        {
            isAttacking = true;
        }

    }

    public void UpAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !isAttacking && !didUpAttackGround && !downAttacking)
        {
            if (controller.isGrounded)
                didUpAttackGround = true;
            else if (!controller.isGrounded)
                airUpAttack = true;
            Debug.Log("Up Attack Pt. 1");
        }
    }
    
    public void DownAttackCharge(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            animator.SetTrigger("Charge");
            Debug.Log("Started Charging");
        }
        if(ctx.canceled && !isAttacking && !didUpAttackGround && !downAttacking)
        {
            Debug.Log("Down Attack Charged time: " + ctx.duration);
            if (ctx.duration <= 1.0f)
                downAttackLv1 = true;
            else if (ctx.duration > 1.0f && ctx.duration <= 2.0f)
                downAttackLv2 = true;
            else if (ctx.duration > 2.0f)
                downAttackLv3 = true;
            downAttacking = true;
        }
    }

    public void CallResetHitbox()
    {
        BroadcastMessage("ResetHitbox");
    }
}
