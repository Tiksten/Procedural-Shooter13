using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Level : MonoBehaviour
{
    [SerializeField]
    private GameObject roomObject;

    private Vector3Int pos = Vector3Int.zero;

    [SerializeField]
    private int rooms = 7;

    public Tile floor;

    public Tile wall;

    public Tile door;

    public GameObject exit;

    public GameObject enemy;

    private Tilemap tilemap;

    [SerializeField]
    private int halfRoadWidth = 2;

    [SerializeField]
    private int roadLength = 20;

    private List<Vector3Int> roomPoints = new List<Vector3Int>();

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();

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
            RoadCycle();
        else
            OnRoadsDone();
    }

    private void OnRoadsDone()
    {
        var rom = BuildRoom(Vector3Int.zero, Random.Range(4, 4 + roadLength / 2), Random.Range(4, 4 + roadLength / 2), true);

        foreach (Vector3Int roomPoint in roomPoints)
        {
            rom = BuildRoom(roomPoint, Random.Range(4, 4 + roadLength / 2), Random.Range(4, 4 + roadLength / 2));
        }

        rom.isExit = true;
    }

    private Room BuildRoom(Vector3Int coord, int halfHeight, int halfWidth, bool isStartRoom = false)
    {
        for(int x = -halfWidth; x <= halfWidth; x++)
        {
            for(int y = -halfHeight; y <= halfHeight; y++)
            {
                if (Mathf.Abs(x) == halfWidth || Mathf.Abs(y) == halfHeight)
                {
                    if (Mathf.Abs(x) <= 1 || Mathf.Abs(y) <= 1)
                    {
                        if(tilemap.GetTile(coord + new Vector3Int(x, y, 0))!=null)
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

        if (!isStartRoom)
        {
            var rom = Instantiate(roomObject, (Vector3)coord + new Vector3(0.5f, 0.5f), transform.rotation, transform);

            rom.GetComponent<Room>().lvl = this;

            rom.GetComponent<Room>().coord = coord;
            rom.GetComponent<Room>().halfHeight = halfHeight;
            rom.GetComponent<Room>().halfWidth = halfWidth;
            rom.GetComponent<Room>().tilemap = tilemap;

            return rom.GetComponent<Room>();
        }

        return null;
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
