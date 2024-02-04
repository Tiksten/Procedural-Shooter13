using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class LootFromTilemap : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private LootTable lootTable;

    private void Start()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if(tilemap.HasTile(pos))
            {
                var item = lootTable.GetItem();
                if (item != null)
                    Instantiate(item, (Vector2Int)pos + Vector2.one * 0.5f, Quaternion.identity, transform);
            }
        }

        Destroy(tilemap.gameObject);
        Destroy(this);
    }
}
