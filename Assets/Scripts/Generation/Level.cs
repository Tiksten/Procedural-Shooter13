using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Level : MonoBehaviour
{
    private Vector3Int pos = Vector3Int.zero;

    [SerializeField]
    private int rooms = 7;

    [SerializeField]
    private Tile floor;

    [SerializeField]
    private Tile wall;

    private Tilemap tilemap;

    [SerializeField]
    private int halfRoadWidth = 2;

    [SerializeField]
    private int roadLength = 20;

    private List<Vector3Int> roomPoints = new List<Vector3Int>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

        //BuildRoom(Vector3Int.zero, 3, 5);

        //BuildRoad(Vector3Int.zero, Vector2Int.right);
        //BuildRoad(Vector3Int.zero, Vector2Int.down);

        RoadCycle();
    }

    private void RoadCycle()
    {
        var dir = Random.insideUnitCircle.normalized;

        Vector2Int dirInt;       

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            dirInt = new Vector2Int((int)Mathf.Sign(dir.x), 0);
        else
            dirInt = new Vector2Int(0, (int)Mathf.Sign(dir.y));

        if (tilemap.GetTile(pos + (Vector3Int)dirInt * (roadLength + 8)) == null)
        {
            roomPoints.Add(pos + (Vector3Int)dirInt * (roadLength + 8));
            rooms--;
        }

        BuildRoad(pos, dirInt);

        pos = pos + (Vector3Int)dirInt * (roadLength + 8);

        if (rooms > 0)
            Invoke("RoadCycle", 2);
        else
            OnRoadsDone();
    }

    private void OnRoadsDone()
    {
        BuildRoom(Vector3Int.zero, Random.Range(4, 4 + roadLength / 2), Random.Range(4, 4 + roadLength / 2));

        foreach (Vector3Int roomPoint in roomPoints)
        {
            BuildRoom(roomPoint, Random.Range(4, 4 + roadLength / 2), Random.Range(4, 4 + roadLength / 2));
        }
    }

    private void BuildRoom(Vector3Int coord, int halfHeight, int halfWidth)
    {
        for(int x = -halfWidth; x <= halfWidth; x++)
        {
            for(int y = -halfHeight; y <= halfHeight; y++)
            {
                if (Mathf.Abs(x) == halfWidth || Mathf.Abs(y) == halfHeight)
                {
                    if (Mathf.Abs(x) <= 1 || Mathf.Abs(y) <= 1)
                    {
                        if(tilemap.GetTile(new Vector3Int(x, y, 0))!=null)
                            tilemap.SetTile(coord + new Vector3Int(x, y, 0), floor);
                        else
                            tilemap.SetTile(coord + new Vector3Int(x, y, 0), wall);
                    }
                    else
                    {
                        tilemap.SetTile(coord + new Vector3Int(x, y, 0), wall);
                    }
                }
                else
                {
                    tilemap.SetTile(coord + new Vector3Int(x, y, 0), floor);
                }
            }
        }

        
    }

    private void BuildRoad(Vector3Int from, Vector2Int dir)
    {
        tilemap.SetTile(from, floor);

        tilemap.SetTile(from + (Vector3Int)dir * (roadLength + 8), floor);

        for (int x = -halfRoadWidth; x <= halfRoadWidth; x++)
        {
            for (int y = 4; y <= roadLength + 4; y++)
            {
                var tile = floor;

                if (Mathf.Abs(x) == halfRoadWidth)
                    tile = wall;

                tilemap.SetTile(new Vector3Int(from.x + x * dir.y + y * dir.x, from.y + x * dir.x + y * dir.y), tile);
            }
        }
    }
}
