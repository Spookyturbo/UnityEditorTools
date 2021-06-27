using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/LevelTexture", order = 1)]
public class LevelObject : ScriptableObject
{
    public string levelName = "Map";
    public Texture2D map;
        
    public ColorToPrefab[] prefabs;

    [ContextMenu("Load Level")]
    public void LoadLevel()
    {
        LevelGenerator.GenerateMap(map, prefabs, levelName);
    }

    [ContextMenu("Delete Level")]
    public void DeleteLevel()
    {
        GameObject level = GameObject.Find(levelName);
        if (level == null)
            return;
        DestroyImmediate(level);
    }

    [ContextMenu("Update Level")]
    public void UpdateLevel()
    {
        GameObject level = GameObject.Find(levelName);

        //Don't update if the level is not already in the window
        if (!level)
            return;

        DeleteLevel();
        LoadLevel();
    }
}
