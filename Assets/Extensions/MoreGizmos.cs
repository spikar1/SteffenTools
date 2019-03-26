using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoreGizmos {

    public static void DrawCircle(Vector3 origin, float radius, Color color) {
        DrawCircle(origin, radius, 12, color);
    }
    public static void DrawCircle(Vector3 origin, float radius, int sides, Color color) {
        Color orgCol = Gizmos.color;
        Gizmos.color = color;
        DrawCircle(origin, radius, sides);
        Gizmos.color = orgCol;
    }
    public static void DrawCircle(Vector3 origin, float radius, int sides) {
        Vector3 lastPos = origin + Vector3.right * radius;
        float x = origin.x;
        float y = origin.y;
        float z = origin.z;
        if (!Mathf.Approximately(z, 0))
            DrawCircle(new Vector3(x, y, 0), radius, sides, Color.red * .5f);
        if (sides < 1)
            throw new System.Exception("Sides can not be zero or negative numbers");
        for (int i = 0; i <= sides; i++) {
            float angle = 360 / sides;
            float rad = Mathf.Deg2Rad * angle * i;
            Vector3 newPos = new Vector3(x + radius * Mathf.Cos(rad), y + radius * Mathf.Sin(rad), z);
            Gizmos.DrawLine(lastPos, newPos);
            lastPos = newPos;
        }
        
    }
}
