using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Tilemaps;

public class LevelGen : MonoBehaviour
{
    [SerializeField]
    private float upperValue = 1;

    [SerializeField]
    private float addValue = 1;


    [SerializeField]
    private MeshRenderer rend;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Tilemap tilemap;

    [SerializeField]
    private int roomCount = 7;

    [Tooltip("Distance on which will be spawned new rooms")]
    [SerializeField]
    private Vector2 pointsDist = new Vector2(15, 25);

    [Tooltip("How many px will be added around farther point coord")]
    [SerializeField]
    private int additionalSpace = 32;


    [SerializeField]
    private float perlinNoiseScale = 0.001f;

    [SerializeField]
    private float perlinNoiseWeight = 0.1f;


    [SerializeField]
    private Vector2 setTilesInBetween = new Vector2(0.5f, 0.6f);

    private void OnEnable()
    {
        tilemap.ClearAllTiles();
        BuildLevel(PerlinDeform(MetaballDeform(GeneratePoints())));
    }

    private List<Vector2> GeneratePointsSimple()
    {
        var prevPoint = Vector2.zero;

        List<Vector2> points = new List<Vector2>(){ Vector2.zero };

        for(int i = roomCount; i >= 0; i--)
        {
            points.Add(prevPoint + Random.insideUnitCircle.normalized * Random.Range(pointsDist.x, pointsDist.y));
            prevPoint = points[points.Count-1];
        }

        return points;
    }

    private List<Vector2> GeneratePoints()
    {
        var prevPoint = Vector2.zero;

        var prevDir = Random.insideUnitCircle.normalized;

        List<Vector2> points = new List<Vector2>() { Vector2.zero };

        for (int i = roomCount; i >= 0; i--)
        {
            points.Add(prevPoint + (prevDir + Random.insideUnitCircle).normalized * Random.Range(pointsDist.x, pointsDist.y));
            prevPoint = points[points.Count - 1];
            prevDir = (prevDir + Random.insideUnitCircle).normalized;
        }

        return points;
    }

    private Texture2D MetaballDeformSimple(List<Vector2> points)
    {
        float maxMagnitude = 0;

        foreach(Vector2 point in points)
        {
            if (point.magnitude > maxMagnitude)
                maxMagnitude = point.magnitude;
        }

        var textureRes = Mathf.CeilToInt(maxMagnitude) + additionalSpace;

        var result = new Texture2D(textureRes, textureRes, TextureFormat.RFloat, false);

        for(int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                var coord = new Vector2Int(x, y) - Vector2Int.one * Mathf.FloorToInt(textureRes / 2f);

                var weight = 0f;

                foreach(Vector2 point in points)
                {
                    var value = 1 / (((Vector2)coord - point).magnitude + 1);

                    if (value > weight)
                        weight = value;
                }

                result.SetPixel(x, y, Color.white * weight);
            }
        }

        result.Apply();

        return result;
    }

    private Texture2D MetaballDeform(List<Vector2> points)
    {
        float maxMagnitude = 0;

        foreach (Vector2 point in points)
        {
            if (point.magnitude > maxMagnitude)
                maxMagnitude = point.magnitude;
        }

        var textureRes = 2*Mathf.CeilToInt(maxMagnitude) + additionalSpace;

        var result = new Texture2D(textureRes, textureRes, TextureFormat.RFloat, false);

        for (int x = 0; x < textureRes; x++)
        {
            for (int y = 0; y < textureRes; y++)
            {
                var coord = new Vector2Int(x, y) - Vector2Int.one * Mathf.FloorToInt(textureRes / 2f);

                var weight = 0f;

                foreach (Vector2 point in points)
                {
                    weight += upperValue / (((Vector2)coord - point).magnitude + addValue);
                }

                result.SetPixel(x, y, Color.white * weight);
            }
        }

        result.Apply();

        return result;
    }

    private Texture2D PerlinDeform(Texture2D input)
    {
        for (int x = 0; x < input.width; x++)
        {
            for (int y = 0; y < input.height; y++)
            {
                input.SetPixel(x, y, input.GetPixel(x, y) + perlinNoiseWeight * Color.white * Mathf.PerlinNoise(x*perlinNoiseScale, y*perlinNoiseScale));
            }
        }

        input.Apply();

        return input;
    }

    private void BuildLevel(Texture2D input)
    {
        rend.material.SetTexture("_BaseMap", input);

        for (int x = 0; x < input.width; x++)
        {
            for (int y = 0; y < input.height; y++)
            {
                var coord = new Vector2Int(x, y) - Vector2Int.one * Mathf.FloorToInt(input.width / 2f);

                if (input.GetPixel(x, y).r > setTilesInBetween.x && input.GetPixel(x, y).r < setTilesInBetween.y)
                    tilemap.SetTile((Vector3Int)coord, tile);
            }
        }
    }

    private void Smth()
    {
        
    }
}
