using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DefenseZone : MonoBehaviour
{
    public List<GameObject> defendingEnemies;
    [UnityEngine.Range(1, 6)] public int defenderCount;
    public BaseEnemy enemyType;
    BoxCollider2D defenseZone;
    List<Vector2> spawnPoints;

    BaseEnemy curEnemy;
    UnityEvent<bool> playerUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defenseZone = GetComponent<BoxCollider2D>();
        if (defendingEnemies != null)
        {
            GenerateSpawnPoints();
            SetDefenderSpawns();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetDefenderSpawns()
    {
        for(int i = 0; i < defenderCount; ++i)
        {
            
        }
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
        float steps = defenseZone.size.x / defendingEnemies.Count;
        Vector2 curPos = new Vector2( transform.position.x - defenseZone.size.x, transform.position.y);
        for(int i = 0; i < defenderCount; ++i)
        {
            spawnPoints[i] = curPos;
            curPos.x += steps;
        }
    }
}
