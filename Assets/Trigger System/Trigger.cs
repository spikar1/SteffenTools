using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SteffenTools.Extensions;


namespace TriggerSystem {

    public class Trigger : MonoBehaviour {
        public static bool showGizmos = true;

        public UnityEvent eventEnter;
        public UnityEvent eventExit;
        public UnityEvent eventStay;

        public Color gizmosColor = new Color(0, 1, 0, .3f);

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
                            Color orgCol = Gizmos.color;
                            if (!active)
                                Gizmos.color = Gizmos.color * .5f;
                            Gizmos.DrawWireCube(t.position, Vector3.one);
                            //Gizmos.DrawLine(transform.position, t.position);
                            AdditionalGizmos.DrawArc(transform.position, t.position);
                            Gizmos.color = orgCol;
                        }

                    }
                }
            }
            foreach (var col in GetComponents<Collider>()) {
                if (!col.isTrigger || !col.enabled)
                    continue;
                if (col.GetType() == typeof(BoxCollider)) {

                    var m = Gizmos.matrix;
                    Gizmos.matrix = transform.localToWorldMatrix;

                    BoxCollider bc = col as BoxCollider;
                    Gizmos.DrawCube(bc.center, bc.size);

                    Gizmos.matrix = m;
                }
                else if (col.GetType() == typeof(SphereCollider)) {
                    SphereCollider sc = col as SphereCollider;
                    Vector3 s = transform.localScale;
                    float maxScale = Mathf.Max(s.x, s.y, s.z);
                    Gizmos.DrawSphere(transform.TransformPoint(sc.center), sc.radius * maxScale);
                }
            }
        }
    }
}