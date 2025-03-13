using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [SerializeField] private float invincibilityTime; //How long the player will be invunerable for after taking damage
    private UnityEvent died;

    public bool invulnerable = false;

    //private float time;

    private void Awake()
    {
        currentHealth = maxHealth;
        died = new UnityEvent();
        if (gameObject.tag == "Player")
        {
            gameObject.BroadcastMessage("SetMaxHealth", maxHealth); // Sets the max value for the health bar
            gameObject.BroadcastMessage("SetHealth", maxHealth); // Sets the current value for the health bar
        }
        
    }

    public void TakeDamage(float damage)
    {
        if(invulnerable == false || damage < 0)
        {
            currentHealth -= damage;

            if(gameObject.tag == "Player" && damage > 0)
            {
                StartCoroutine(TriggerInvincibility());
                gameObject.BroadcastMessage("Shake", 0.5f);
                gameObject.BroadcastMessage("SetHealth", currentHealth); // Update the health bar when taking damage

                //if (gameObject.TryGetComponent(out DamageFlash colorStrobe))
                //    StartCoroutine(colorStrobe.StrobeColor());
                //else
                //    Debug.Log("ColorStrobeNotFound");
            }
        }
        if( currentHealth <= 0 )
            died.Invoke();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }

    private IEnumerator TriggerInvincibility()
    {
        invulnerable = true;
        //Activate damage flash here

        yield return new WaitForSeconds(invincibilityTime);

        invulnerable = false;
        //Deactivate damage flash here

        yield return null;
    }

    public void AddDeathListener(UnityAction _newListener)
    {
        died.AddListener(_newListener);
    }
}
