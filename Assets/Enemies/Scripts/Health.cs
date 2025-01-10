using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;

    [SerializeField] private float invincibilityTime; //How long the player will be invunerable for after taking damage

    //[SerializeField] private BoxCollider2D hurtbox;

    public bool invulnerable = false;

    public float time;

    private void Start()
    {
        currentHealth = maxHealth;
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
            }
        }
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
}
