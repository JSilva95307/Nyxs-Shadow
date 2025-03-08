using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefenseZone : MonoBehaviour
{
    List<GameObject> defendingEnemies;
    public GameObject defenderType;
    public GameObject defenderCount;

    UnityEvent<bool> playerUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void createDefenders()
    {

    }

    private void updateDefenders()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }

}
