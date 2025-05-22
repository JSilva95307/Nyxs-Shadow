using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using RangeAttribute = UnityEngine.RangeAttribute;

public class DefenseZone : MonoBehaviour
{
    [SerializeField] List<GameObject> defendingEnemies;
    public GoblinBehavior enemyType;
    public GameObject enemy;
    BoxCollider2D defenseZone;
    public LayerMask playerLayer;
    [SerializeField] List<Vector2> spawnPoints;
    [Range (1, 6)] public int enemyCount;

    public bool playertest = false;

    GoblinBehavior curEnemy;
    UnityEvent<bool> playerUpdate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defenseZone = GetComponent<BoxCollider2D>();
        playerUpdate = new UnityEvent<bool>();
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
            defendingEnemies.Add(Instantiate(enemy, spawnPoints[i], transform.rotation));
            curEnemy = defendingEnemies[i].GetComponent<GoblinBehavior>();
            curEnemy.SetSpawn(spawnPoints[i]);
            playerUpdate.AddListener(curEnemy.PlayerListener);
        }
    }

    private void updateDefenders(bool update)
    {
        playerUpdate.Invoke(update);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            updateDefenders(true);
            Debug.Log("player entered!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            updateDefenders(false);
            for(int i = 0;i < enemyCount; ++i)
            {
                defendingEnemies[i].GetComponent<GoblinBehavior>().animator.SetTrigger("Retreat");
            }
            Debug.Log("player Left!");
        }
    }

    private void GenerateSpawnPoints()
    {
        float steps = defenseZone.size.x / enemyCount;
        Vector2 curPos = new Vector2( transform.position.x - (defenseZone.size.x * 0.5f), transform.position.y);
        for(int i = 0; i < enemyCount; ++i)
        {
            spawnPoints.Add(curPos);
            curPos.x += steps;
        }
    }
}
