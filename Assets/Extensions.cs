using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteffenTools.Extensions
{
    public static class AdditionalGizmos
    {
        public static void DrawArc(Vector3 pos1, Vector3 pos2, float heightScale = .7f)
        {

            Color c = Gizmos.color;

            int divisions = 15;
            for (int ii = 0; ii < 3; ii++)
            {

                Gizmos.color = new Color(c.r, c.g, c.b, .1f);
                Vector3 prevPos = pos1;

                for (int i = 0; i < divisions; i++)
                {
                    //Vector3 dir = (pos2 - pos1).normalized;
                    //Vector3 pos = Vector3.Lerp(pos1, pos2, (float)i / divisions);
                    //float height = Mathf.Sin(((float)i / divisions) * Mathf.PI) * Vector3.Distance(pos1, pos2) * heightScale;
                    //pos = new Vector3(pos.x, pos.y + height, pos.z);
                    Vector3 pos = Vector3Extensions.getPointAlongArc(pos1, pos2, Mathf.Repeat((float)i / divisions, divisions), heightScale);

                    Gizmos.DrawLine(prevPos, pos);
                    //Gizmos.color = new Color(c.r, c.g, c.b, c.a * (Mathf.Sin(((float)i / divisions) * Mathf.PI)) + .1f);
                    prevPos = pos;
                }
                heightScale += .003f;
            }
            Gizmos.color = c;
        }
    }

    public static class Vector3Extensions
    {
        public static Vector3 getPointAlongArc(Vector3 a, Vector3 b, float t, float maxHeight = 1)
        {
            //Vector3 dir = (a - b).normalized;
            Vector3 pos = Vector3.Lerp(a, b, t);
            float height = Mathf.Sin((t * Mathf.PI)) * maxHeight;
            pos = new Vector3(pos.x, pos.y + height, pos.z);

            return pos;
        }

        public static Vector3 Multiply (this Vector3 a, Vector3 b) {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }
    } 
}