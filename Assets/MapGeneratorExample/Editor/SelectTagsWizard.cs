using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectTagsWizard : ScriptableWizard
{
    public string tag;

    [MenuItem("My Tools/Select Tags")]
    static void OpenWizard()
    {
        ScriptableWizard.DisplayWizard<SelectTagsWizard>("Select Tags", "Select All");
    }

    private void OnWizardCreate()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        Selection.objects = objects;
    }

}
