using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackController : MonoBehaviour
{
    public Animator animator;
    public bool isAttacking = false;
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

    public void CallResetHitbox()
    {
        BroadcastMessage("ResetHitbox");
    }
}
