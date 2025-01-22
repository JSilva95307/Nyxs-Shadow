using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



interface IInteractable
{
    public void Interact();
}


public class Interactor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCollider;
    private List<Collider2D> results = new List<Collider2D>();

    void Start()
    {
        BoxCollider2D[] _collider = GetComponents<BoxCollider2D>();

        for(int i = 0;  i < _collider.Length; i++) //Gets the collider that is a trigger on the player
        {
            if (_collider[i].isTrigger == false)
            {
                playerCollider = _collider[i];
                break;
            }
        }
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Tried Interacting");
            playerCollider.Overlap(results);

            foreach (Collider2D obj in results)
            {
                Debug.Log(obj);
                if (obj.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }

}
