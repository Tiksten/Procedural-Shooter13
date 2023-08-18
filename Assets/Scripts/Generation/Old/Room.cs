using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public Level lvl;

    private GameObject exit;

    private Tile floor;

    private Tile door;

    private GameObject enemy;

    public Tilemap tilemap;

    public Vector3Int coord;

    public int halfHeight;

    public int halfWidth;

    public Vector2Int waves = new Vector2Int(1, 5);

    public Vector2Int enemies = new Vector2Int(3, 8);

    private int remainingEnemies;

    private int remainingWaves;

    public bool isExit;

    private void Start()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(halfWidth*2-2, halfHeight*2-2);
        enemy = lvl.enemy;
        floor = lvl.floor;
        door = lvl.door;
        exit = lvl.exit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        remainingWaves = Random.Range(waves.x, waves.y);

        GetComponent<BoxCollider2D>().enabled = false;

        UseDoors();

        Wave();
    }

    private void Wave()
    {
        remainingWaves--;

        for(var i = Random.Range(enemies.x, enemies.y); i>0; i--)
        {
            remainingEnemies++;

            var enem = Instantiate(enemy, transform.position + (Vector3)Random.insideUnitCircle * 4, transform.rotation);
            enem.AddComponent<AliveAnchor>().room = this;
        }
    }

    private void UseDoors(bool close = true)
    {
        for(int x = -1; x<=1;x++)
        {
            InvertTile(new Vector3Int(coord.x+halfWidth, coord.y+x), close);
            InvertTile(new Vector3Int(coord.x-halfWidth, coord.y+x), close);
            InvertTile(new Vector3Int(coord.x+x, coord.y+halfHeight), close);
            InvertTile(new Vector3Int(coord.x+x, coord.y-halfHeight), close);
        }
    }

    private void InvertTile(Vector3Int pos, bool close)
    {
        if (close)
        {
            if (tilemap.GetTile(pos).name.StartsWith("Floor"))
                tilemap.SetTile(pos, door);
        }
        else
        {
            if (tilemap.GetTile(pos).name.StartsWith("Door"))
                tilemap.SetTile(pos, floor);
        }
    }

    public void OnUnitDeath()
    {
        remainingEnemies--;

        if (remainingEnemies == 0)
        {
            if (remainingWaves > 0)
                Wave();
            else
            {
                UseDoors(false);

                if (isExit)
                    Instantiate(exit, transform.position, transform.rotation);

                Destroy(gameObject);
            }
        }
            
    }
}
