using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;



interface IInteractable
{
    public void Interact();
}


public class Interactor : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;
    private List<Collider2D> results = new List<Collider2D>();
    private ContactFilter2D filter = new ContactFilter2D().NoFilter();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BoxCollider2D[] _collider = GetComponents<BoxCollider2D>();

        for(int i = 0;  i < _collider.Length; i++) //Gets the collider that is a trigger on the player
        {
            if (_collider[i].isTrigger == false)
            {
                collider = _collider[i];
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Tried Interacting");
            collider.Overlap(results);

            //Debug.Log(results);

            foreach(Collider2D obj in results)
            {
                Debug.Log(obj);
                if (obj.TryGetComponent(out IInteractable interactObj)){
                    interactObj.Interact();
                }
            }
        }
    }

}
