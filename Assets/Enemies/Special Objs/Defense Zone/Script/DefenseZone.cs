using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefenseZone : MonoBehaviour
{
    [SerializeField] List<GameObject> defendingEnemies;
    [UnityEngine.Range(1, 6)] public int defenderCount;
    public BaseEnemy enemyType;

    BaseEnemy curEnemy;
    UnityEvent<bool> playerUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (defendingEnemies != null)
        {
            GenerateSpawnPoints();
            createDefenders();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void createDefenders()
    {
        
    }

    private void updateDefenders(bool update)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (true)
            updateDefenders(true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(true) 
            updateDefenders(false);
    }

    private void GenerateSpawnPoints()
    {

    }
}
