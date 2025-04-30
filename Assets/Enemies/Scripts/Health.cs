using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Vars")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    public bool invulnerable = false;

    [Space(10)]

    [Header("Stagger Vars")]
    [SerializeField] private float currentStagger = 0;
    [SerializeField] private float maxStagger = 100f;
    [SerializeField] private float staggerMultiplier = 1.3f; // Damage multiplier for when an enemy is staggered
    [SerializeField] private float unstaggeredDecreaseRate = 5f; // The rate the stagger meter will decrease at when the enemy is not staggered
    [SerializeField] private float staggeredDecreaseRate = 15f; // The rate the stagger meter will decrease at when the enemy is staggered
    public bool staggered = false;

    [Space(10)]

    [Header("Player Only")]
    [SerializeField] private float invincibilityTime; //How long the player will be invunerable for after taking damage
    
    
    private UnityEvent died;

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

    private void Update()
    {
        if (staggered)
        {
            if (currentStagger > 0)
            {
                currentStagger -= Time.deltaTime * staggeredDecreaseRate;
                currentStagger = Mathf.Clamp(currentStagger, 0, maxStagger);
                PlayerManager.Instance.SetEnemyStagger(currentStagger);
            }
        }
        else if (!staggered)
        {
            if (currentStagger > 0)
            {
                currentStagger -= Time.deltaTime * unstaggeredDecreaseRate;
                currentStagger = Mathf.Clamp(currentStagger, 0, maxStagger);
                PlayerManager.Instance.SetEnemyStagger(currentStagger);
            }
        }

        if(staggered && currentStagger <= 0)
        {
            staggered = false;
        }
        
    }

    public void TakeDamage(float damage)
    {
        if(invulnerable == false || damage < 0)
        {
            if(staggered)
                currentHealth -= damage * staggerMultiplier;
            else
                currentHealth -= damage;

            if(gameObject.tag == "Player" && damage > 0)
            {
                StartCoroutine(TriggerInvincibility());
                gameObject.BroadcastMessage("Shake", 0.5f);
                PlayerManager.Instance.playerHUD.SetPlayerHealth(currentHealth);
            }
            else if (gameObject.tag != "Player")
            {
                //Displays the health and stagger of the enemy that is hit most recently
                PlayerManager.Instance.playerHUD.EnableEnemyBars(true);

                PlayerManager.Instance.SetEnemyMaxHealth(maxHealth);
                PlayerManager.Instance.SetEnemyHealth(currentHealth);
                PlayerManager.Instance.SetEnemyMaxStagger(maxStagger);
                PlayerManager.Instance.SetEnemyStagger(currentStagger);
            }
        }
        
        if( currentHealth <= 0 )
            died.Invoke();
    }

    public void TakeStagger(float stagger)
    {
        if(staggered) { return; }

        //PlayerManager.Instance.playerHUD.EnableEnemyBars(true);

        if (invulnerable == false || stagger > 0)
        {
            currentStagger += stagger;
            currentStagger = Mathf.Clamp(currentStagger, 0, maxStagger);
        }

        if(currentStagger >= maxStagger)
        {
            staggered = true;
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


    public float GetMaxStagger()
    {
        return maxStagger;
    }

    public void SetMaxStagger(float _maxStagger)
    {
        maxStagger = _maxStagger;
    }

    public float GetCurrentStagger()
    {
        return currentStagger;
    }

    public void SetCurrentStagger(float _currentStagger)
    {
        currentStagger = _currentStagger;
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
