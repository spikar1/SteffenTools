using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SteffenTools.Shortcuts.General {
    public class Shortcuts {
        // % == ctrl
        // # == Shift
        // & == alt
        // _ == nothing



        [MenuItem("SteffenTools/Shortcuts/Reset Position    #W")]
        public static void ResetPosition() {
            var selectedTransforms = Selection.transforms;

            //iterate through all selected objects and reset their position
            foreach (var selectedTransform in selectedTransforms) {
                Undo.RecordObject(selectedTransform, "ResetPosition of " + selectedTransform.name);

                selectedTransform.position = Vector3.zero;

                EditorUtility.SetDirty(selectedTransform);
            }
        }
        [MenuItem("SteffenTools/Shortcuts/Reset Rotation    #E")]
        public static void ResetRotation() {
            var selectedTransforms = Selection.transforms;

            foreach (var selectedTransform in selectedTransforms) {
                Undo.RecordObject(selectedTransform, "Reset Rotation of " + selectedTransform.name);

                selectedTransform.rotation = Quaternion.identity;

                EditorUtility.SetDirty(selectedTransform);
            }
        }
        [MenuItem("SteffenTools/Shortcuts/Reset Scale       #R")]
        public static void ResetScale() {
            var selectedTransforms = Selection.transforms;

            foreach (var selectedTransform in selectedTransforms) {
                Undo.RecordObject(selectedTransform, "Reset Scale of " + selectedTransform.name);

                selectedTransform.localScale = Vector3.one;

                EditorUtility.SetDirty(selectedTransform);
            }
        }
        [MenuItem("SteffenTools/Shortcuts/Reset Transform   #Q")]
        public static void ResetTransform() {
            ResetPosition();
            ResetRotation();
            ResetScale();
        }

        [MenuItem("SteffenTools/Shortcuts/SnapToGrid        %W")]
        public static void SnapToGrid() {
            var selectedTransforms = Selection.transforms;

            //iterate through all selected objects and reset their position
            foreach (var selectedTransform in selectedTransforms) {
                Undo.RecordObject(selectedTransform, "ResetPosition of " + selectedTransform.name);
                var originalPosition = selectedTransform.position;
                selectedTransform.position = new Vector3(Mathf.Round(originalPosition.x), Mathf.Round(originalPosition.y), Mathf.Round(originalPosition.z));

                EditorUtility.SetDirty(selectedTransform);
            }
        }
    }
}