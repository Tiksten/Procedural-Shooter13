using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
                {
                    var go = Instantiate(item, (Vector2Int)pos + Vector2.one * 0.5f, Quaternion.identity, transform);
                    go.name = go.name.Remove(go.name.Length-7);
                }
            }
        }

        Destroy(tilemap.gameObject);
        Destroy(this);
    }
}
