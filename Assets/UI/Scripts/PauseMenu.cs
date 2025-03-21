using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseMenu : MonoBehaviour 
{
    [SerializeField] private Canvas canvas;
    private bool paused = false;
    public GameObject firstSelected;

    public void Start()
    {
        //Debug.Log("Started");
        canvas.enabled = false;
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && !paused)
        {
            canvas.enabled = true;
            EventSystem.current.SetSelectedGameObject(firstSelected);
            Time.timeScale = 0;
            PlayerManager.Instance.player.GetComponent<PlayerController>().DisableAllControls();
        }
        else if(ctx.performed && paused) 
        {
            Resume();
        }
    }

    public void Resume()
    {
        canvas.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1.0f;
        PlayerManager.Instance.player.GetComponent<PlayerController>().EnableAllControls();
    }

    public void Settings()
    {
        //Open the settings menu
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
