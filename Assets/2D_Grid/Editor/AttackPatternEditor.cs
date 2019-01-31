using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SteffenTools.NodeSystem
{
    [CustomEditor(typeof(AttackPattern))]
    public class AttackPatternEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var ap = target as AttackPattern;

            if (GUILayout.Button("reset"))
            {
                ap.Pattern = new bool[0][];
            }
            if (ap.Pattern.Length != ap.sizeX || ap.Pattern[0].Length != ap.sizeY)
                ap.Pattern = new bool[0][];

            GUILayout.BeginVertical();
            for (int y = 0; y < ap.sizeY; y++)
            {
                GUILayout.BeginHorizontal();
                for (int x = 0; x < ap.sizeX; x++)
                {
                    GUILayout.Label($"{x}, {y} - ");
                    ap.Pattern[x][y] = EditorGUILayout.Toggle(ap.Pattern[x][y]);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

        }
    } 
}
