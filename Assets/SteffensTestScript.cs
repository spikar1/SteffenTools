    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;

public class SteffensTestScript : MonoBehaviour
{
    public Vector3 dir = Vector3.forward;

    public List<Vector3> points = new List<Vector3>
    {
        new Vector3(0,0,1),
        new Vector3(0,0, 2)
    };

    public Axis axisPlane = Axis.X;

    public Vector3 tDir;
    public Vector3 transformed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            points = points.RotateAround(Axis.Y, 90);
            dir = Quaternion.Euler(0, 90, 0) * dir;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            points = points.RotateAround(Axis.Y, -90);
            dir = Quaternion.Euler(0, -90, 0) * dir;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            points = points.RotateAround(Axis.X, 90);
            dir = Quaternion.Euler(90, 0, 0) * dir;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            points = points.RotateAround(Axis.X, -90);
            dir = Quaternion.Euler(-90, 0, 0) * dir;
        }

        DebugDrawers.DrawCircle(transform.position, 2, transform.forward, Color.red, 0, 20);

        DebugDrawers.DrawCircle(transform.position, 4, axisPlane, Color.blue, 0, 32);

        Debug.DrawRay(Vector3.zero, dir);

        Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, Quaternion.LookRotation(tDir), Vector3.one);
        transformed = m.inverse.MultiplyPoint3x4(Vector3.forward);

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawCube(points[i] + transform.position, Vector3.one * .7f);
        }
    }
}
