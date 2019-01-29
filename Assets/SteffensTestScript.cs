    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;

public class SteffensTestScript : MonoBehaviour
{
    public Vector3 dir = Vector3.right;

    public List<Vector3> points;

    public AxisPlane axisPlane = AxisPlane.X;

    public Vector3 tDir;
    public Vector3 transformed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dir = Quaternion.Euler(0, 90, 0) * dir;
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = points[i].RotateAround(0, 90, 0);
            }
        }

        DebugDrawers.DrawCircle(transform.position, 2, transform.forward, Color.red, 0, 20);

        DebugDrawers.DrawCircle(transform.position, 4, axisPlane);

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
