using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class MapTextureImporter : AssetPostprocessor
{
    //Is the current run a new import or not?
    bool NewImport
    {
        get
        {
            Texture2D asset = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            return !asset;
        }
    }

    //Is the current run updating an existing import?
    bool Update { get { return !NewImport; } }

    //The LevelPath of the current run, only access this if actually working with a level
    string LevelPath
    {
        get
        {
            string name = Path.GetFileNameWithoutExtension(assetPath);
            return "Assets/MapGeneratorExample/Levels/" + name + ".asset";
        }
    }

    void OnPreprocessTexture()
    {
        //Only preprocess if hasn't been imported yet
        if (!NewImport)
        {
            Debug.Log("Already imported, not preprocessing");
            return;
        }

        string lowercasePath = assetPath.ToLower();

        //Import as a map if place in this directory
        if (lowercasePath.Contains("/maps/"))
        {
            TextureImporter importer = (TextureImporter)assetImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.filterMode = FilterMode.Point;
            importer.isReadable = true;
            importer.textureCompression = TextureImporterCompression.Uncompressed;
        }
    }

    void OnPostprocessTexture(Texture2D texture)
    {
        string lowercasePath = assetPath.ToLower();

        //Import as a map if place in this directory
        if (lowercasePath.Contains("/maps/"))
        {
            if(NewImport)
            {
                CreateScriptableObjectFromTexture(texture);
            }
            else
            {
                UpdateScriptableObjectFromTexture(texture);
            }
        }
    }

    //Check for additional colors, call the update on the level automatically
    void UpdateScriptableObjectFromTexture(Texture2D texture)
    {
        EditorApplication.delayCall += () =>
        {
            LevelObject level = AssetDatabase.LoadAssetAtPath<LevelObject>(LevelPath);
            texture = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);

            List<ColorToPrefab> prefabs = new List<ColorToPrefab>(level.prefabs);

            bool newColor = false;

            //Check for new colors
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color color = texture.GetPixel(x, y);
                    if (color.a < 1)
                        continue;

                    //Check to see if the current color is not in the list, if not add it
                    if (!prefabs.Any(cp => cp.color == color))
                    {
                        newColor = true;
                        prefabs.Add(new ColorToPrefab() { color = color });
                    }
                }
            }
            //Assign new colors
            if (newColor)
                level.prefabs = prefabs.ToArray();
            //update the level
            level.UpdateLevel();
        };
    }



    void CreateScriptableObjectFromTexture(Texture2D texture)
    {
        //Get and set values for the new object
        string name = Path.GetFileNameWithoutExtension(assetPath);
        LevelObject newLevel = ScriptableObject.CreateInstance<LevelObject>();
        newLevel.levelName = name;

        //Actually create the new object
        AssetDatabase.CreateAsset(newLevel, LevelPath);
        AssetDatabase.SaveAssets();

        //For some reason the texture actually isnt done being imported yet so use delay call
        UnityEditor.EditorApplication.delayCall += () =>
        {
            //Set the map
            Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>(assetPath);
            newLevel.map = t;

            //Set the values for the color array
            HashSet<Color> colors = new HashSet<Color>();
            //List<Color> colors = new List<Color>();

            //Get all the colors
            for (int x = 0; x < t.width; x++)
            {
                for (int y = 0; y < t.height; y++)
                {
                    Color c = t.GetPixel(x, y);
                    //Ignore transparent colors
                    if (c.a < 1)
                        continue;
                    colors.Add(c);
                }
            }

            //Set the array to have a value for each color
            ColorToPrefab[] matchings = new ColorToPrefab[colors.Count];
            int i = 0;
            foreach (Color color in colors)
            {
                matchings[i] = new ColorToPrefab() { color = color };
                i++;
            }

            //Finish assigning the colors
            newLevel.prefabs = matchings;
        };
    }

}
