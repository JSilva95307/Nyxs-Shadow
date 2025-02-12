using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InventoryScreen : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public GameObject firstSelected;
    public List<Canvas> tabList;
    private int activeTab = 1;
    private void Start()
    {
        canvas.enabled = false;
        tabList[0].enabled = false;
        tabList[1].enabled = false;
        tabList[2].enabled = false;

    }

    private void Update()
    {
        
    }


    public void ToggleInventory()
    {
        if(canvas.enabled == false)
        {
            canvas.enabled = true;
            tabList[activeTab].enabled = true;
            EventSystem.current.SetSelectedGameObject(firstSelected);

            Time.timeScale = 0;
            PlayerManager.Instance.player.GetComponent<PlayerController>().DisableAllControls();
        }
        else if(canvas.enabled == true)
        {
            canvas.enabled = false;
            EventSystem.current.SetSelectedGameObject(null);

            Time.timeScale = 1.0f;
            PlayerManager.Instance.player.GetComponent<PlayerController>().EnableAllControls();
        }

        //Open animation code here
    }

   public void ChangeTab(int tab)
    {
        if (activeTab == tab)
            return;

        Debug.Log("changing tabs");

        tabList[activeTab].enabled = false;

        activeTab = tab;
        tabList[activeTab].enabled = true;
    }
}
