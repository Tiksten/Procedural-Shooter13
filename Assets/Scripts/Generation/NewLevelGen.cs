using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class NewLevelGen : MonoBehaviour
{
    [SerializeField]
    private List<Tile> wallTiles;

    [SerializeField]
    private List<Tile> floorTiles;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private int textureRes = 128;


    [SerializeField]
    private float decorScale = 0.2f;

    [SerializeField]
    private float roomScale = 0.05f;

    [SerializeField]
    private float spagettiScale = 0.1f;

    [SerializeField]
    private float spagettiValue = 0.4f;


    [SerializeField]
    private Vector2 tileSens = new Vector2(0, 0.4f);


    [SerializeField]
    private UnityEvent OnLevelReady;



    private Vector2 centre;

    private float radius;

    delegate float GenFunc(Vector2 pos);

    delegate float MixFunc(float a, float b);

    private void OnEnable()
    {
        centre = Vector2.one * textureRes / 2;

        radius = textureRes / 2;





        var rooms = Iterate(CreateTexture(), DrawPerlinRooms);

        var decor = Iterate(CreateTexture(), DrawPerlinDecorative);

        var sphere = Iterate(CreateTexture(), DrawSphere);

        var spagetti = Iterate(CreateTexture(), DrawSpagetti);

        var caves = Normalize(Mix(Normalize(Mix(rooms, decor, Multiply)), spagetti, Sum));



        ApplyTexture(Normalize(Mix(caves, sphere, Multiply)));

        if (OnLevelReady != null)
            OnLevelReady.Invoke();
    }

    private Texture2D CreateTexture()
    {
        return new Texture2D(textureRes, textureRes, TextureFormat.RFloat, false);
    }

    private void ApplyTexture(Texture2D texture)
    {
        texture.Apply();

        for (int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                var value = texture.GetPixel(x, y).r;

                if (value > tileSens.x && value < tileSens.y)
                    tilemap.SetTile(new Vector3Int((int)x - textureRes / 2, (int)y - textureRes / 2), wallTiles[Random.Range(0, wallTiles.Count)]);
                else
                    tilemap.SetTile(new Vector3Int((int)x - textureRes / 2, (int)y - textureRes / 2), floorTiles[Random.Range(0, floorTiles.Count)]);
            }
        }
    }

    private float DrawSphere(Vector2 pos)
    {
        var dist = (pos - centre).magnitude;
        
        return Mathf.Sqrt(Mathf.Clamp01((radius - dist) / radius));
    }

    private float DrawSpagetti(Vector2 pos)
    {
        var value = Mathf.PerlinNoise(pos.x * spagettiScale, pos.y * spagettiScale);

        if (value < 0.6f && value > 0.4f)
        {
            return spagettiValue;
        }

        return 0;
    }

    private float DrawPerlinRooms(Vector2 pos)
    {
        return Mathf.PerlinNoise(pos.x * roomScale, pos.y * roomScale);
    }

    private float DrawPerlinDecorative(Vector2 pos)
    {
        return Mathf.PerlinNoise(pos.x * decorScale, pos.y * decorScale);
    }

    private Texture2D Iterate(Texture2D texture, GenFunc genFunc)
    {
        for(int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                texture.SetPixel(x, y, Color.white * genFunc(new Vector2(x, y)));
            }
        }

        return texture;
    }

    private Texture2D Mix(Texture2D a, Texture2D b, MixFunc mixFunc)
    {
        var result = CreateTexture();

        for (int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                result.SetPixel(x, y, Color.white * mixFunc(a.GetPixel(x, y).r, b.GetPixel(x, y).r));
            }
        }

        return result;
    }

    private Texture2D Normalize(Texture2D texture)
    {
        float mx = -10000;

        float mn = 10000;

        for (int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                var value = texture.GetPixel(x, y).r;

                if (value > mx)
                    mx = value;

                if (value < mn)
                    mn = value;
            }
        }

        for (int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                texture.SetPixel(x, y, ((texture.GetPixel(x, y).r - mn)/(mx-mn)) * Color.white);
            }
        }

        return texture;
    }

    private float Multiply(float a, float b)
    {
        return a * b;
    }

    private float Sum(float a, float b)
    {
        return a + b;
    }
}
