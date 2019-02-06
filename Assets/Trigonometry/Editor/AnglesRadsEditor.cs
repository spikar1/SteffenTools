using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SteffenTools.Extensions;

[CustomEditor(typeof(AnglesRads))]
public class AnglesRadsEditor : Editor
{
    private void OnSceneGUI()
    {
        var ar = target as AnglesRads;

        Vector3 handleDirection = Vector3.forward;
        Vector3 offsetInput = ar.input + ar.axialOffset;

        EditorGUI.BeginChangeCheck();
        var newTargetPosition = Handles.Slider2D(offsetInput, handleDirection, Vector3.right, Vector3.up, .2f, Handles.CircleHandleCap, 0);
        newTargetPosition = ((Vector2)newTargetPosition - ar.axialOffset).Clamp(-1, 1);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(ar, "Change axialInput");
            ar.input = newTargetPosition;
            
        }
    }
}
