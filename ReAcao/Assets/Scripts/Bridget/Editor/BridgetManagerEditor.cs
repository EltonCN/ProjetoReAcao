using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BridgetManager))]
public class BridgetManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BridgetManager bm = (BridgetManager) target;
        if(GUILayout.Button("Create grid"))
        {
            bm.CreateGrid();
        }
        else if(GUILayout.Button("Clear grid"))
        {
            bm.Clear();
        }

    }
}