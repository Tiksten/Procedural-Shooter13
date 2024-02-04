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
    private AnimationCurve tickTime;

    private Transform player;

    public float maxDist = 30;

    [SerializeField]
    private AnimationCurve maxEnemies;

    private int totalEnemies = 0;

    private float currTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("Tick", tickTime.Evaluate(currTime));
    }

    private void Tick()
    {
        if (totalEnemies < maxEnemies.Evaluate(currTime))
            TrySpawnEnemy();

        currTime += tickTime.Evaluate(currTime);

        Invoke("Tick", tickTime.Evaluate(currTime));
    }

    private void TrySpawnEnemy()
    {
        if(player == null) 
            return;

        var pos = player.position + (Vector3)Random.insideUnitCircle.normalized * Random.Range(spawnDist.x, spawnDist.y);

        if (!Physics2D.OverlapCircle(pos, 0.2f))
        {
            totalEnemies++;
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], pos, Quaternion.identity, transform);
            enemy.AddComponent<EnemyAnchor>().enemySpawner = this;
        }
    }

    public void TrySpawnEnemy(Transform pos)
    {
        if (!Physics2D.OverlapCircle(pos.position, 0.2f))
        {
            totalEnemies++;
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Count)], pos.position, Quaternion.identity, transform);
            enemy.AddComponent<EnemyAnchor>().enemySpawner = this;
        }
    }

    public void OnEnemyDeath(EnemyAnchor enemyAnchor)
    {
        totalEnemies--;
    }
}
