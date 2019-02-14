using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(JSONStuff))]
public class JSONEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        if(GUILayout.Button("WRITE JSON!!")){
            (target as JSONStuff).Write();
        }
        if (GUILayout.Button("READ JSON!!")){
            (target as JSONStuff).Read();
        }
    }
}
