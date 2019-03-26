using UnityEngine;
using System.Collections;

public static class Vector2Extensions
{
    public static Vector3 ToVector3(this Vector2 _v2, float z = 0)
    {
        return new Vector3(_v2.x, _v2.y, z);
    }

    public static Vector2 RoundedToInt(this Vector2 vector)
    {
        Vector2 result;
        result.x = Mathf.Round(vector.x);
        result.y = Mathf.Round(vector.y);
        return result;
    }
}