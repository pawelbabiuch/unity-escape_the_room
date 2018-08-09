using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Key))]
public class KeyEditor : Editor
{
    Key key;

    private void OnEnable()
    {
        key = (Key)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = false;
        EditorGUILayout.Space();
        EditorGUILayout.IntField("Key ID", key.keyID);
        GUI.enabled = true;
    }
}
