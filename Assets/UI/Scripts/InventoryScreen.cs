using UnityEngine;

public class InventoryScreen : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    public void OpenInventory()
    {
        canvas.enabled = true;

        //Open animation code here
    }

    public void CloseInventory()
    {
        canvas.enabled = false;
        
        //Close animation code here
    }
}
