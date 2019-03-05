using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SteffenTools.Extensions;


namespace TriggerSystem2D {

    public class Trigger2D : MonoBehaviour {
        public static bool showGizmos = true;
        public TriggerOptions2D options;

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
        private void OnTriggerEnter2D(Collider2D other) {
            eventEnter.Invoke();
        }
        private void OnTriggerExit2D(Collider2D other) {
            eventExit.Invoke();
        }
        private void OnTriggerStay2D(Collider2D other) {
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
                            AdditionalGizmos.DrawArc(transform.position, mr.bounds.center, options.connectorGravity.y);
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
            foreach (var col in GetComponents<Collider2D>()) {
                if (!col.isTrigger || !col.enabled)
                    continue;
                if (col.GetType() == typeof(BoxCollider2D)) {
                    BoxCollider2D bc = col as BoxCollider2D;
                    Gizmos.DrawCube(transform.position + (Vector3)bc.offset.Multiply(transform.localScale), Vector3.Scale(transform.localScale, bc.size));
                }
                else if (col.GetType() == typeof(CircleCollider2D)) {
                    CircleCollider2D sc = col as CircleCollider2D;
                    Vector3 s = transform.localScale;
                    float maxScale = Mathf.Max(s.x, s.y, s.z);
                    Gizmos.DrawSphere(transform.position + (Vector3)sc.offset, sc.radius * maxScale);
                }
            }


        }

    }

    
}