using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ColorToPrefab))]
public class ColorToPrefabDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty color = property.FindPropertyRelative("color");
        SerializedProperty prefab = property.FindPropertyRelative("prefab");

        Rect colorRect = new Rect(position.x, position.y, 150, position.height);
        Rect prefabRect = new Rect(position.x + 180, position.y, 200, position.height);

        EditorGUIUtility.labelWidth = 60;

        EditorGUI.PropertyField(colorRect, color);
        EditorGUI.PropertyField(prefabRect, prefab);
    }
}
