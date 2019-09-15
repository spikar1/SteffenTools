using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLecture : MonoBehaviour
{
    private void Update() {
        DrawLine(0, 0, 100, 100, Color.red);
    }

    void DrawLine(float x1, float y1, float x2, float y2, Color color) {
        Debug.DrawLine(new Vector3(x1, y1), new Vector3(x2, y2), color);
    }
}
