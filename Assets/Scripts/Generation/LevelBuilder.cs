using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    private float roadSize = 2;






    [SerializeField]
    private Transform startRoomParent;

    private struct Room
    {
        public string name;
        public float radius;
        public Vector2 pos;
    }

    private struct Path
    {
        public Vector2 a;
        public Vector2 b;
    }

    private List<Room> rooms = new List<Room>();

    private List<Path> paths = new List<Path>();

    private Texture2D texture;

    public void OnPointsReady()
    {
        SaveData(startRoomParent.GetChild(0));

        Invoke("BuildLevel", 0.1f);
    }

    private void SaveData(Transform room)
    {
        foreach(Transform child in room)
        {
            SaveData(child);
        }

        if(room.GetComponent<SpringJoint2D>()!=null)
            paths.Add(new Path() { a = room.position, b = room.parent.position });

        rooms.Add(new Room() { name = room.name, radius = room.GetComponent<CircleCollider2D>().radius, pos = room.position });
    }

    private void BuildLevel()
    {
        float maxOffset = 0;

        foreach (Room room in rooms)
        {
            if(maxOffset<room.pos.magnitude)
                maxOffset = room.pos.magnitude;
        }

        maxOffset = Mathf.CeilToInt(maxOffset * 2);

        texture = new Texture2D((int)maxOffset, (int)maxOffset, TextureFormat.RFloat, false);

        foreach (Room room in rooms)
            BuildRoom(room);

        foreach (Path path in paths)
            BuildPath(path);
    }

    private void BuildRoom(Room room)
    {
        var halfSize = Mathf.CeilToInt(room.radius);

        var roomOffset = Vector2Int.CeilToInt(room.pos);

        for(int x = -halfSize; x <= halfSize; x++)
        {
            for (int y = -halfSize; y <= halfSize; y++)
            {
                if(new Vector2(x, y).magnitude <= room.radius)
                {
                    var weight = Mathf.Pow(new Vector2(x, y).magnitude/room.radius, 0.5f);

                    texture.SetPixel(x + roomOffset.x, y + roomOffset.y, (weight + texture.GetPixel(x + roomOffset.x, y + roomOffset.y).r)*Color.white);
                }
            }
        }
    }

    private void BuildPath(Path path)
    {

    }
}
