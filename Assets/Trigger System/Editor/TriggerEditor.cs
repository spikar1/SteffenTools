using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TriggerSystem {

    [CustomEditor(typeof(Trigger))]
    public class TriggerEditor : Editor {

        private void OnSceneGUI()
        {
            if(Event.current.type == EventType.Repaint)
            {
                Handles.color = Color.red;
                Handles.CubeHandleCap(0, Vector3.zero, Quaternion.identity, .1f, EventType.Repaint);

                var t = target as Trigger;

                float size = HandleUtility.GetHandleSize(t.transform.position) * 1f;
                float snap = 0.5f;

                EditorGUI.BeginChangeCheck();

                float scale = Handles.ScaleSlider(t.f, t.transform.position, -t.transform.right, t.transform.rotation, size, snap);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Change Scale Value");
                    t.f = scale;
                    
                }

                /*EditorGUI.BeginChangeCheck();
                t.f = Handles.ScaleSlider(t.f, t.transform.position, -t.transform.forward, t.transform.rotation, size, snap);
                
                EditorGUI.EndChangeCheck();*/
            }
        }

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

            trigger.f = EditorGUILayout.FloatField(trigger.f);

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
