using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace TriggerSystem {

    public class Trigger : MonoBehaviour {
        public static bool showGizmos = true;

        public UnityEvent eventEnter;
        public UnityEvent eventExit;
        public UnityEvent eventStay;

        public Color gizmosColor = new Color(0, 1, 0, .3f);

        public float triggerRadius => GetComponent<SphereCollider>().radius;
        public Vector3 triggerSize => GetComponent<BoxCollider>().size;

        [HideInInspector]
        public bool onTriggerEnter = false;
        [HideInInspector]
        public bool onTriggerExit = false;
        [HideInInspector]
        public bool onTriggerStay = false;
        private void OnTriggerEnter(Collider other) {
            eventEnter.Invoke();
        }
        private void OnTriggerExit(Collider other) {
            eventExit.Invoke();
        }
        private void OnTriggerStay(Collider other) {
            eventStay.Invoke();
        }

        private void OnDrawGizmos() {
            if (!showGizmos)
                return;
            Gizmos.color = gizmosColor;

            DrawEvents(eventEnter, onTriggerEnter);
            DrawEvents(eventExit, onTriggerExit);
            DrawEvents(eventStay, onTriggerStay);

            void DrawEvents(UnityEvent ev, bool active) {
                for (int i = 0; i < ev.GetPersistentEventCount(); i++) {
                    var component = ev.GetPersistentTarget(i) as Component;
                    if (component != null) {
                        Transform t = component.transform;

                        if (t.GetComponent<MeshRenderer>()) {
                            var mr = t.GetComponent<MeshRenderer>();
                            Color orgCol = Gizmos.color;
                            if (!active)
                                Gizmos.color = Gizmos.color * .5f;
                            Gizmos.DrawWireCube(mr.bounds.center, mr.bounds.size + Vector3.one * .1f);
                            AdditionalGizmos.DrawArc(transform.position, mr.bounds.center);
                            Gizmos.color = orgCol;
                        }
                        else {
                            Gizmos.DrawWireCube(t.position, Vector3.one);
                            //Gizmos.DrawLine(transform.position, t.position);
                            AdditionalGizmos.DrawArc(transform.position, t.position);
                        }

                    }
                }
            }
            foreach (var col in GetComponents<Collider>()) {
                if (!col.isTrigger || !col.enabled)
                    continue;
                if (col.GetType() == typeof(BoxCollider)) {
                    BoxCollider bc = col as BoxCollider;
                    Gizmos.DrawCube(transform.position + bc.center, Vector3.Scale(transform.localScale, bc.size));
                }
                else if (col.GetType() == typeof(SphereCollider)) {
                    SphereCollider sc = col as SphereCollider;
                    Vector3 s = transform.localScale;
                    float maxScale = Mathf.Max(s.x, s.y, s.z);
                    Gizmos.DrawSphere(transform.position + sc.center, sc.radius * maxScale);
                }
            }


        }

    }

    public static class AdditionalGizmos {
        public static void DrawArc(Vector3 pos1, Vector3 pos2, float heightScale = .2f) {

            Color c = Gizmos.color;

            int divisions = 15;
            for (int ii = 0; ii < 3; ii++) {

                Gizmos.color = new Color(c.r, c.g, c.b, .1f);
                Vector3 prevPos = pos1;

                for (int i = 1; i <= divisions; i++) {
                    Vector3 dir = (pos2 - pos1).normalized;
                    Vector3 pos = Vector3.Lerp(pos1, pos2, (float)i / divisions);
                    float height = Mathf.Sin(((float)i / divisions) * Mathf.PI) * Vector3.Distance(pos1, pos2) * heightScale;
                    pos = new Vector3(pos.x, pos.y + height, pos.z);

                    Gizmos.DrawLine(prevPos, pos);
                    Gizmos.color = new Color(c.r, c.g, c.b, c.a * (Mathf.Sin(((float)i / divisions) * Mathf.PI)) + .1f);
                    prevPos = pos;
                }
                heightScale += .002f;
            }
            Gizmos.color = c;
        }
    }

    public static class Vector3Extensions {
        public static Vector3 getPointAlongArc(Vector3 a, Vector3 b, float t, float maxHeight = 1) {
            Vector3 dir = (a - b).normalized;
            Vector3 pos = Vector3.Lerp(a, b, t);
            float height = Mathf.Sin((t * Mathf.PI)) * Vector3.Distance(a, b) * maxHeight;
            pos = new Vector3(pos.x, pos.y + height, pos.z);

            return pos;
        }
    } 
}