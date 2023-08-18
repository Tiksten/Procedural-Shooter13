using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ComplexLevelGen : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private Texture2D texture;

    private List<Transform> points = new List<Transform>();

    [SerializeField]
    private int roomCount;

    private Rigidbody2D rb;

    private void Start()
    {
        
    }


    //private void CreateLinearPoints()
    //{
    //    var go = new GameObject("Start").transform;

    //    go.parent = transform;
    //    go.position = Vector3.zero;

    //    points.Add(go);


    //    var prevDir = Random.insideUnitCircle.normalized;

    //    var prevPoint = Vector2.zero;






    //    for (int i = roomCount; i >= 0; i--)
    //    {




    //        points.Add(prevPoint + (prevDir + Random.insideUnitCircle).normalized * Random.Range(pointsDist.x, pointsDist.y));
    //        prevPoint = points[points.Count - 1];
    //        prevDir = (prevDir + Random.insideUnitCircle).normalized;
    //    }

    //    return points;
    //}
}
