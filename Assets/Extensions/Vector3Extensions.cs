using UnityEngine;
using System.Collections;

static public class Vector3Extensions {

    static public Vector2 toVector2(this Vector3 _v3)
    {
        return new Vector2(_v3.x, _v3.y);
    }
    static public Vector3 Multiply(this Vector3 _v3a, Vector3 _v3b)
    {
        return new Vector3(_v3a.x * _v3b.x, _v3a.y * _v3b.y, _v3a.z * _v3b.z);
    }
    public static Vector3 RoundUp(this Vector3 vector3) {
        vector3 = new Vector3(
            (int)(vector3.x),
            (int)(vector3.y),
            (int)(vector3.z));

        return vector3;

    }
    public static Vector3 Round(this Vector3 vector3) {
        vector3 = new Vector3(
            Mathf.RoundToInt(vector3.x),
            Mathf.RoundToInt(vector3.y),
            Mathf.RoundToInt(vector3.z));

        return vector3;

    }

    public static Vector3 Abs(this Vector3 vector3) {
        vector3 = new Vector3(
            Mathf.Abs(vector3.x),
            Mathf.Abs(vector3.y),
            Mathf.Abs(vector3.z));

        return vector3;
    }
}
