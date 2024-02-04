using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawn : MonoBehaviour
{
    [System.Serializable]
    private struct LootTableLine
    {
        public GameObject item;
        public float spawnChance;
    }

    [SerializeField]
    private LootTableLine[] lootTable;

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
        float weight = 0;

        foreach (var l in lootTable)
        {
            weight += l.spawnChance;
        }

        var choose = Random.value;

        choose *= weight;

        float prev = 0;

        foreach (var l in lootTable)
        {
            if (choose <= l.spawnChance + prev)
            {
                Instantiate(l.item, point.position, point.rotation, transform);
                Destroy(this);
                break;
            }
            else
            {
                prev += l.spawnChance;
            }
        }
    }
}
