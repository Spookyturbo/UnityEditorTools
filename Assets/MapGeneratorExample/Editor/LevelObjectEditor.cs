using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelObject))]
public class LevelObjectEditor : Editor
{

    LevelObject Level
    {
        get { return (LevelObject)target; }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Load Level"))
        {
            Level.LoadLevel();
        }

        if(GUILayout.Button("Delete Level"))
        {
            Level.DeleteLevel();
        }

        if(GUILayout.Button("Update Level"))
        {
            Level.UpdateLevel();
        }

        GUILayout.EndHorizontal();
    }
}
