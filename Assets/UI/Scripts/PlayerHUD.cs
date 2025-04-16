using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    [SerializeField] private GameObject _mostRecentEnemyHealthBar;
    [SerializeField] private GameObject _mostRecentEnemyStaggerBar;

    private Slider mostRecentEnemyHealthBar;
    private Slider mostRecentEnemyStaggerBar;
    
    [SerializeField] private float EnemyBarTimeLimit;

    public float timer = 0f;

    private void Start()
    {
        mostRecentEnemyHealthBar = _mostRecentEnemyHealthBar.GetComponent<Slider>();
        mostRecentEnemyStaggerBar = _mostRecentEnemyStaggerBar.GetComponent<Slider>();


        timer = EnemyBarTimeLimit;

        SetEnemyMaxHealth(100);
        SetEnemyHealth(0);
        SetEnemyMaxStagger(100);
        SetEnemyStagger(0);

        _mostRecentEnemyHealthBar.SetActive(false);
        _mostRecentEnemyStaggerBar.SetActive(false);
    }

    private void Update()
    {
        if(mostRecentEnemyHealthBar.enabled == true)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0, EnemyBarTimeLimit);


            if(timer >= EnemyBarTimeLimit)
            {
                EnableEnemyBars(false);
            }
        }
    }

    public void SetMaxHealth(float health)
    {
        healthBar.maxValue = health;
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    public void SetEnemyHealth(float health)
    {
        mostRecentEnemyHealthBar.value = health;
    }

    public void SetEnemyStagger(float stagger)
    {
        mostRecentEnemyStaggerBar.value = stagger;
    }

    public void SetEnemyMaxHealth(float health)
    {
        mostRecentEnemyHealthBar.maxValue = health;
    }

    public void SetEnemyMaxStagger(float stagger)
    {
        mostRecentEnemyStaggerBar.maxValue = stagger;
    }

    public void EnableEnemyBars(bool enabled)
    {
        if(enabled)
        {
            timer = 0;
            _mostRecentEnemyHealthBar.SetActive(true);
            _mostRecentEnemyStaggerBar.SetActive(true);

        }   
        else
        {
            _mostRecentEnemyHealthBar.SetActive(false);
            _mostRecentEnemyStaggerBar.SetActive(false);
        }   
    }
}
