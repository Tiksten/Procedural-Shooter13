using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AreaItemSpawner : MonoBehaviour
{
    [SerializeField]
    private float radius = 5;


    [SerializeField]
    private LootTable anyPointObj;

    [SerializeField]
    private int aTryCnt = 5;


    [SerializeField]
    private bool insideGridCell = true;

    [SerializeField]
    private LootTable exactPointObj;

    [SerializeField]
    private int eTryCnt = 5;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {

        if (anyPointObj != null)
        {
            for (int i = 0; i < aTryCnt; i++)
            {
                SetAny();
            }
        }

        if (exactPointObj != null)
        {
            for (int i = 0; i < eTryCnt; i++)
            {
                SetExact();
            }
        }
    }

    private void SetAny()
    {
        var pos = (Vector2)transform.position + Random.insideUnitCircle * radius;

        if (!Physics2D.OverlapCircle(pos, 0.5f))
        {
            Instantiate(anyPointObj.GetItem(), pos, Quaternion.identity, transform);
        }
    }

    private void SetExact()
    {
        Vector2 pos;

        if(insideGridCell)
            pos = Vector2.one * 0.5f + Vector2Int.RoundToInt((Vector2)transform.position + Random.insideUnitCircle * radius);
        else
            pos = Vector2Int.RoundToInt((Vector2)transform.position + Random.insideUnitCircle * radius);

        if (!Physics2D.OverlapCircle(pos, 0.5f))
        {
            Instantiate(exactPointObj.GetItem(), pos, Quaternion.identity, transform);
        }
    }
}
