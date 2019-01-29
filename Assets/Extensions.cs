using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteffenTools.Extensions
{
    public static class AdditionalGizmos
    {
        /// <summary>
        /// Draws an arc from pos1 to pos2
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <param name="heightScale">how tall the apex of the arc should be</param>
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
                    Vector3 pos = Vector3Extensions.GetPointAlongArc(pos1, pos2, Mathf.Repeat((float)i / divisions, divisions), heightScale);

                    Gizmos.DrawLine(prevPos, pos);
                    //Gizmos.color = new Color(c.r, c.g, c.b, c.a * (Mathf.Sin(((float)i / divisions) * Mathf.PI)) + .1f);
                    prevPos = pos;
                }
                heightScale += .003f;
            }
            Gizmos.color = c;
        }
    }
    public enum AxisPlane { X, Y, Z}
    public static class DebugDrawers
    {
        
        public static void DrawCircle(Vector3 center, float radius, AxisPlane axisPlane)
        {
            DrawCircle(center, radius, axisPlane, Color.white, 0);
        }

        public static void DrawCircle(Vector3 center, float radius, AxisPlane axisPlane, Color color, float duration, float resolution = 16)
        {
                switch (axisPlane)
                {
                    case AxisPlane.X:
                        DrawCircle(center, radius, Vector3.right, color, duration, resolution);
                        break;
                    case AxisPlane.Y:
                        DrawCircle(center, radius, Vector3.up, color, duration, resolution);
                        break;
                    case AxisPlane.Z:
                        DrawCircle(center, radius, Vector3.forward, color, duration, resolution);
                        break;
                    default:
                        break;
                }

        }

        public static void DrawCircle(Vector3 center, float radius, Vector3 dir, Color color, float duration, float resolution = 16)
        {
            Vector3 lastPos = Vector3.zero;
            Vector3 pos = lastPos;
            for (int i = 0; i <= resolution; i++)
            {
                float f = (float)i / resolution * Mathf.PI * 2;

                pos = new Vector3(Mathf.Sin(f), Mathf.Cos(f), 0) * radius;

                //I need to understand this sometime....
                Matrix4x4 m = Matrix4x4.Rotate(Quaternion.LookRotation(dir));
                //End of not understanding

                pos = m.MultiplyPoint3x4(pos);
                

                if (i != 0)
                    Debug.DrawLine(lastPos + center, pos + center, color, duration);
                lastPos = pos;
            }
        }
    }
    public static class Vector3Extensions
    {
        /// <summary>
        /// Gets a Point along an arc from a to b.
        /// </summary>
        /// <param name="a">start pos</param>
        /// <param name="b">start pos</param>
        /// <param name="t">0 = start, 1 = end</param>
        /// <param name="maxHeight">the apex of the arc</param>
        /// <returns></returns>
        public static Vector3 GetPointAlongArc(Vector3 a, Vector3 b, float t, float maxHeight = 1)
        {
            //Vector3 dir = (a - b).normalized;
            Vector3 pos = Vector3.Lerp(a, b, t);
            float height = Mathf.Sin((t * Mathf.PI)) * maxHeight;
            pos = new Vector3(pos.x, pos.y + height, pos.z);
            
            return pos;
        }

        /// <summary>
        /// Multiply each axis, Convolution
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Multiply (this Vector3 a, Vector3 b) {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Returns the average point from a number of points
        /// </summary>
        /// <param name="vectors"></param>
        /// <returns></returns>
        public static Vector3 Average (params Vector3[] vectors)
        {
            Vector3 sum = Vector3.zero;
            foreach (var v in vectors)
            {
                sum += v;
            }
            return sum / vectors.Length;
        }

        /// <summary>
        /// Return a float of the largest axis (positive only)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float MaxAxis (Vector3 v)
        {
            return Mathf.Max(v.x, v.y, v.z);
        }

        public static Vector3 RotateAround (this Vector3 vector, Vector3 euler)
        {
            return Quaternion.Euler(euler) * vector;
        }
        public static Vector3 RotateAround(this Vector3 vector, float x, float y, float z)
        {
            return RotateAround(vector, new Vector3(x, y, z));
        }
    }
}