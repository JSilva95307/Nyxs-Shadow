using UnityEngine;

public class TestHelmet : MonoBehaviour
{
    private GameObject player;

    public float bonusHealth = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public override void OnEquipped()
    //{
    //    player.GetComponent<Health>().SetMaxHealth(player.GetComponent<Health>().GetMaxHealth() + bonusHealth);
    //}

    //public override void OnUnEquipped()
    //{
    //    player.GetComponent<Health>().SetMaxHealth(player.GetComponent<Health>().GetMaxHealth() - bonusHealth);
    //}
}
