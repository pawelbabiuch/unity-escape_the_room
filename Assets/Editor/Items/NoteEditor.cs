using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Note))]
public class NoteEditor : Editor
{
    Note note;

    private void OnEnable()
    {
        note = (Note)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUI.enabled = false;
        EditorGUILayout.Space();
        EditorGUILayout.TextField("Password", note.password);
        GUI.enabled = true;
    }
}
