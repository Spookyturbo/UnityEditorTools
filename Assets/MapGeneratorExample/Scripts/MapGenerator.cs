using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Based of Brackeys Level Generator
//https://www.youtube.com/watch?v=B_Xp9pt8nRY
public class MapGenerator : MonoBehaviour
{

    public Texture2D map;
    public ColorToPrefab[] prefabs;

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color color = map.GetPixel(x, y);

        //Nothing in the spot, nothing to generate
        if (color.a == 0)
            return;

        //Find what should be made and make it
        foreach(ColorToPrefab property in prefabs)
        {
            if(property.color.Equals(color))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(property.prefab, position, Quaternion.identity, transform);
                return;
            }
        }

    }

    [System.Serializable]
    public class ColorToPrefab
    {
        public Color color;
        public GameObject prefab;
    }

}
