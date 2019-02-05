using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;
using UnityEditor;

using G = UnityEngine.Gizmos;
using C = UnityEngine.Color;
using V3 = UnityEngine.Vector3;
using M = UnityEngine.Mathf;

public class AnglesRads: MonoBehaviour
{
    [Header("Visualize Triangle")]
    public bool fromDegrees = true;
    public float degrees = 0;
    public float radians = 0;
    [Header("Parameters")]
    public float sine;
    public float cosine, tangent;
    public bool showTan = false;
    [Header("Animate")]
    public bool showAnims = false;
    public bool useTime = false;


    private void OnDrawGizmos()
    {
        
        if (fromDegrees)
            radians = (degrees * M.PI) / 180;
        else
            degrees = (radians * 180) / M.PI;

        //If allowed, use time as input.
        float t = (float)EditorApplication.timeSinceStartup;
        if (useTime)
        {
            radians = t;
            degrees = degrees = (radians * 180) / M.PI;
        }

        //Clamp the values to a readable range
        radians = M.Repeat(radians, 2 * M.PI);
        degrees = M.Repeat(degrees, 360);

        //local variables for simplicity
        sine = M.Sin(radians);
        cosine = M.Cos(radians);
        tangent = M.Tan(radians);

        //Draw Radius
        DebugDrawers.DrawCircle(V3.zero, 1, Axis.Z, C.white, 0, 64);
        //Construct a triangle
        G.color = C.green;
        G.DrawLine(V3.zero, new V3(cosine, sine));
        G.color = C.blue;
        G.DrawLine(V3.zero, V3.right * cosine);
        G.color = C.red;
        G.DrawRay(V3.zero + V3.right * cosine, V3.up * sine);

        
        //Draw Graph of the curves
        G.color = C.white;
        V3 offset = V3.down * 2 + V3.right * -1;
        G.DrawRay(offset + V3.up * .5f, V3.down * 1);
        G.DrawRay(offset + V3.down * .5f, V3.right * 2);

        G.color = C.gray;
        G.DrawRay(offset + V3.up * .5f, V3.right * 2);
        G.DrawRay(offset, V3.right * 2);

        G.DrawRay(offset + V3.up * .5f + V3.right * M.Repeat(radians / M.PI, 2), V3.down * 1);

        //Draw Sine
        G.color = C.red;
        offset = V3.down * 2 + V3.right * -1;
        V3 last = new V3(0, 0, 0) + offset;
        for (int i = 1; i <= 360; i++)
        {
            V3 point = new V3((float)i / 180, M.Sin(M.Deg2Rad * i) * .5f, 0) + offset;

            G.DrawLine(last, point);
            last = point;
        }
        //Draw Cosine
        G.color = C.blue;
        offset = V3.down * 2 + V3.right * -1;
        last = new V3(0, .5f, 0) + offset;
        for (int i = 1; i <= 360; i++)
        {
            V3 point = new V3((float)i / 180, M.Cos(M.Deg2Rad * i) * .5f, 0) + offset;

            G.DrawLine(last, point);
            last = point;
        }
        //Draw Tangent
        if (showTan)
        {
            G.color = C.yellow;
            offset = V3.down * 2 + V3.right * -1;
            last = new V3(0, .5f, 0) + offset;
            for (int i = 1; i <= 360; i++)
            {
                V3 point = new V3((float)i / 180, M.Tan(M.Deg2Rad * i) * .5f, 0) + offset;

                G.DrawLine(last, point);
                last = point;
            }
        }


        if (!showAnims)
            return;
        G.color = C.white;
        G.DrawWireCube(V3.right * 2 + V3.up * M.Sin(t) + V3.up * -.25f, new V3(1, .5f, 0));
        G.DrawWireCube(V3.right * 3 + V3.up * M.Sin(t * 2) + V3.up * -.25f, new V3(1, .5f, 0));
        G.DrawWireCube(V3.right * 4 + V3.up * M.Sin(t * 2) * .5f + V3.up * -.25f, new V3(1, .5f, 0));
    }
}
