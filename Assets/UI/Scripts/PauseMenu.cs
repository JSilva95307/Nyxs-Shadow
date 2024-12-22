using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public class PauseMenu : MonoBehaviour 
{
    [SerializeField] private Canvas canvas;

    public void Pause()
    {
        canvas.enabled = true;
    }

    public void Resume()
    {
        canvas.enabled = false;
    }

    public void Settings()
    {
        //Open the settings menu
    }

    public void QuitToMainMenu()
    {
        //Go to the main emu scene
    }
}
