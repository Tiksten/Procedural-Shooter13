using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAnchor : MonoBehaviour
{
    [HideInInspector]
    public EnemySpawner enemySpawner;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Invoke("CheckDist", 3);
    }

    private void CheckDist()
    {
        if (player == null)
            return;

        if ((player.position - transform.position).magnitude > enemySpawner.maxDist)
            Destroy(gameObject);
        else
            Invoke("CheckDist", 3);
    }

    private void OnDestroy()
    {
        enemySpawner.OnEnemyDeath(this);
    }
}
