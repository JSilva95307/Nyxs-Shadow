using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

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
        //apply launch
        if(PlayerManager.Instance.launchSpeed > 0 && other.gameObject.TryGetComponent(out LaunchScript enemyLaunchee))
        {
            enemyLaunchee.launchDir = PlayerManager.Instance.launcherDir;
            enemyLaunchee.launchForce = PlayerManager.Instance.launchSpeed;
        }
    }

    public void ResetHitbox()
    {
        hitEnemies.Clear();
        PlayerManager.Instance.lungeDist.Set(0, 0);
    }

}
