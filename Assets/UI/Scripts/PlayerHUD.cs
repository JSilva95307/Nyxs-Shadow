using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider healthBar;



    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
}
