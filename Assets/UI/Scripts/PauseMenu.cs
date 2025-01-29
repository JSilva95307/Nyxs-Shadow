using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class PauseMenu : MonoBehaviour 
{
    [SerializeField] private Canvas canvas;

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("paused");
            canvas.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = 1.0f;
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
