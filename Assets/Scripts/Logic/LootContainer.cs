using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainer : MonoBehaviour
{
    [SerializeField]
    private Vector2Int dropCount = new Vector2Int(3, 5);

    [SerializeField]
    private LootTable lootTable;


    private void OnDestroy()
    {
        if(GetComponent<Health>()!=null)
        {
            if(GetComponent<Health>().hp<=0)
            {
                for (var i = Random.Range(dropCount.x, dropCount.y+1); i > 0; i--)
                {
                    var dir = Random.insideUnitCircle;

                    var hit = Physics2D.Raycast(transform.position, dir, 2);

                    if (hit.collider != null)
                    {
                        Instantiate(lootTable.GetItem(), hit.point, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(lootTable.GetItem(), (Vector2)transform.position + dir * 2, Quaternion.identity);
                    }
                }
            }
        }
    }
}
