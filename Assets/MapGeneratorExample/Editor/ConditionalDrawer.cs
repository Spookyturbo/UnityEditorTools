using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConditionalAttribute))]
public class ConditionalDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalAttribute attr = (ConditionalAttribute)attribute;

        string validation = attr.validation;

        var p = property.serializedObject.FindProperty(validation);

        if(p.boolValue)
        {
            EditorGUI.PropertyField(position, property, label);
        }

    }
}
