using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadLevelWizard : ScriptableWizard
{
    //These public variables will be viewable in the wizard
    public LevelObject level;

    [MenuItem("My Tools/Load level from texture")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<LoadLevelWizard>("Load level from texture", "Load");
    }

    //Called when wizard is opened or when user changes something
    private void OnWizardUpdate()
    {
        
    }

    //First button click
    private void OnWizardCreate()
    {
        if (level == null)
            return;

        LevelGenerator.GenerateMap(level.map, level.prefabs, level.levelName);
    }
}
