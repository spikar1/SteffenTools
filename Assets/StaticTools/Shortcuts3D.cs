using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SteffenTools.Shortcuts.For3D {
    public class Shortcuts {

        [MenuItem("SteffenTools/Shortcuts/3D/PlaceOnGround #S")]
        public static void PlaceOnGround() {
            foreach (var trans in Selection.transforms) {
                DoPlaceOnGround(trans);
            }
        }

        static void DoPlaceOnGround(Transform trans) {
            Undo.RecordObject(trans, "Placed " + trans.name + " on the ground");
            var renderer = trans.GetComponent<Renderer>();

            var offset = 0f;
            if (renderer.GetComponent<SpriteRenderer>())
                //TODO: Find a way to get the bottom pixel colour 
                {
                offset = 0f;
            }
            else if (renderer)
                offset = renderer.bounds.extents.y;

            //TODO: Should have solution not revolving around colliders, but alas
            RaycastHit hit;
            RaycastHit2D hit2D;

            if (Physics.Raycast(trans.position, Vector3.down, out hit, 100)) {
                trans.position = hit.point + Vector3.up * offset;
            }
            else if (hit2D = Physics2D.Raycast(trans.position, Vector2.down, 100)) {
                trans.position = (Vector3)hit2D.point + Vector3.up * offset + Vector3.forward * trans.position.z;
            }
            EditorUtility.SetDirty(trans);
        }

        //TODO: Make PlaceOnGroundWithAlign. This needs a seperate function.
    }
}
