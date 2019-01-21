using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TriggerSystem {

    [CustomEditor(typeof(Trigger))]
    public class TriggerEditor : Editor {


        public override void OnInspectorGUI() {
            //DrawDefaultInspector();

            if (GUILayout.Button("Toggle Gizmos")) {
                Trigger.showGizmos = !Trigger.showGizmos;
                SceneView.RepaintAll();
            }
            SerializedObject SO = new SerializedObject(target);
            SerializedProperty propEnter = SO.FindProperty("eventEnter");
            SerializedProperty propExit = SO.FindProperty("eventExit");
            SerializedProperty propStay = SO.FindProperty("eventStay");

            Trigger trigger = target as Trigger;

            bool onTriggerEnter = trigger.onTriggerEnter;
            bool onTriggerExit = trigger.onTriggerExit;
            bool onTriggerStay = trigger.onTriggerStay;

            EditorGUI.BeginChangeCheck();
            if (trigger.onTriggerEnter = EditorGUILayout.Foldout(trigger.onTriggerEnter, "On Trigger Enter")) {
                EditorGUILayout.PropertyField(propEnter);
            }
            if (trigger.onTriggerExit = EditorGUILayout.Foldout(trigger.onTriggerExit, "On Trigger Exit")) {
                EditorGUILayout.PropertyField(propExit);
            }
            if (trigger.onTriggerStay = EditorGUILayout.Foldout(trigger.onTriggerStay, "On Trigger Stay")) {
                EditorGUILayout.PropertyField(propStay);
            }
            EditorGUI.EndChangeCheck();
            SO.ApplyModifiedProperties();

            trigger.gizmosColor = EditorGUILayout.ColorField("Gizmos Color", trigger.gizmosColor);
            SceneView.RepaintAll();

        }
    } 
}
