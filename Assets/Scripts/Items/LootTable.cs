using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLootTable", menuName = "CreateLootTable")]
public class LootTable : ScriptableObject
{
    [System.Serializable]
    private struct LootTableLine
    {
        public GameObject item;
        public float spawnChance;
    }

    [SerializeField]
    private LootTableLine[] lootTable;

    public GameObject GetItem()
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
                return l.item;
            }
            else
            {
                prev += l.spawnChance;
            }
        }

        return null;
    }
}
