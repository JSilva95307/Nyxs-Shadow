using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public bool active;
    public float damage;
    public float stagger;
    private List<GameObject> hitEnemies = new List<GameObject>();
    
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider2D>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        active = gameObject.activeSelf;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            if (hitEnemies.Contains(other.gameObject))
                return;

            hitEnemies.Add(other.gameObject);
        }


        //Deal Damage
        if (other.gameObject.TryGetComponent(out Health enemyHealth))
        {
            enemyHealth.TakeDamage(PlayerManager.Instance.damage);
            enemyHealth.TakeStagger(PlayerManager.Instance.stagger);
        }
        
    }

    public void ResetHitbox()
    {
        hitEnemies.Clear();
        PlayerManager.Instance.lungeDist.Set(0, 0);
    }

}
