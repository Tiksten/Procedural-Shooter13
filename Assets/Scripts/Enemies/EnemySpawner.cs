using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Vector2 spawnDist = new Vector2(10, 15);



    [SerializeField]
    private List<GameObject> enemies;

    [SerializeField]
    private float tickTime = 1;

    private Transform player;

    public float maxDist = 30;

    [SerializeField]
    private int maxEnemies = 12;

    private int totalEnemies = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("Tick", tickTime);
    }

    private void Tick()
    {
        if (totalEnemies < maxEnemies)
            TrySpawnEnemy();

        Invoke("Tick", tickTime);
    }

    private void TrySpawnEnemy()
    {
        var pos = player.position + (Vector3)Random.insideUnitCircle.normalized * Random.Range(spawnDist.x, spawnDist.y);

        if (!Physics2D.OverlapCircle(pos, 0.2f))
        {
            totalEnemies++;
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], pos, Quaternion.identity, transform);
            enemy.AddComponent<EnemyAnchor>().enemySpawner = this;
        }
    }

    public void OnEnemyDeath(EnemyAnchor enemyAnchor)
    {
        totalEnemies--;
    }
}
