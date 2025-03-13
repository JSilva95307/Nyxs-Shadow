using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RangeAttribute = UnityEngine.RangeAttribute;

public class DefenseZone : MonoBehaviour
{
    [SerializeField] List<GameObject> defendingEnemies;
    public BaseEnemy enemyType;
    public GameObject enemy;
    BoxCollider2D defenseZone;
    List<Vector2> spawnPoints;
    [Range (1, 6)] public int enemyCount;

    BaseEnemy curEnemy;
    UnityEvent<bool> playerUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defenseZone = GetComponent<BoxCollider2D>();
        spawnPoints = new List<Vector2>();
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
        for(int i = 0; i < enemyCount; ++i)
        {
            defendingEnemies.Add(Instantiate<GameObject>(enemy, transform.position, new Quaternion()));
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
        for(int i = 0; i < enemyCount; ++i)
        {
            spawnPoints.Add(curPos);
            curPos.x += steps;
        }
    }
}
