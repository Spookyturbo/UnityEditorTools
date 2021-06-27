using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator
{
    public Texture2D map;
    public ColorToPrefab[] prefabs;

    public static void GenerateMap(Texture2D map, ColorToPrefab[] prefabs, string mapName = "Map")
    {
        GameObject parentObject = new GameObject();
        parentObject.name = mapName;

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y, map, prefabs, parentObject.transform);
            }
        }
    }

    static void GenerateTile(int x, int y, Texture2D map, ColorToPrefab[] prefabs, Transform parent)
    {
        Color color = map.GetPixel(x, y);

        //Nothing in the spot, nothing to generate
        if (color.a == 0)
            return;

        //Find what should be made and make it
        foreach (ColorToPrefab property in prefabs)
        {
            if (property.color.Equals(color))
            {
                Vector2 position = new Vector2(x, y);
                GameObject.Instantiate(property.prefab, position, Quaternion.identity, parent);
                return;
            }
        }

    }
}

[System.Serializable]
public class ColorToPrefab
{
    public Color color;
    public GameObject prefab;
}