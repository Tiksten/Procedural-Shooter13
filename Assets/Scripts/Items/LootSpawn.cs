using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    [SerializeField]
    private LootTable lootTable;

    [SerializeField]
    private Transform[] points; 

    private void Start()
    {
        if (points == null)
            SpawnAt(transform);
        else
        {
            foreach (Transform t in points)
                SpawnAt(t);
        }
    }

    private void SpawnAt(Transform point)
    {
        Instantiate(lootTable.GetItem(), point.position, point.rotation, transform);
    }
}
