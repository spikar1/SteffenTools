using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;
using UnityEditor;
using System;

using G = UnityEngine.Gizmos;
using C = UnityEngine.Color;
using V3 = UnityEngine.Vector3;
using M = UnityEngine.Mathf;

public class AnglesRads: MonoBehaviour
{
    float t;
    V3 offset;
    #region Params
    [Header("Visualize Triangle")]
    public bool fromDegrees = true;
    [Range (0, 359.9f)]
    public float degrees = 0;
    [Range (0, M.PI*2)]
    public float radians = 0;
    [Header("Parameters")]
    public float sine;
    public float cosine, tangent;
    public bool showTan = false;
    [Header("Animate")]
    public bool showAnims = false;
    public bool useTime = false;
    [Header("Pendulum")]
    public float maxDeg = 60;
    public float timeScale = 2;
    [Header("Axial Input")]
    public Vector2 axialOffset = Vector2.up;
    public Vector2 input;
    public float angle;
    #endregion

    private void OnDrawGizmos()
    {
        GenerateBaseValues();
        DrawBaseCircle();
        DrawGraph();
        DrawPlatforms();
        DrawPendulum();
        AxialInput();
    }


    private void GenerateBaseValues()
    {
        if (fromDegrees)
            radians = (degrees * M.PI) / 180;
        else
            degrees = (radians * 180) / M.PI;

        //If allowed, use time as input.
        t = (float)EditorApplication.timeSinceStartup;
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
    }
    private void DrawBaseCircle()
    {
        //Draw Radius
        DebugDrawers.DrawCircle(V3.zero, 1, Axis.Z, C.white, 0, 64);
        //Construct a triangle
        G.color = C.blue;
        G.DrawLine(V3.zero, new V3(cosine, sine));
        G.color = C.red;
        G.DrawLine(V3.zero, V3.right * cosine);
        G.color = C.green;
        G.DrawRay(V3.zero + V3.right * cosine, V3.up * sine);
    }
    private void DrawGraph()
    {
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
    }
    private void DrawPlatforms()
    {
        if (!showAnims)
            return;
        G.color = C.white;
        G.DrawWireCube(V3.right * 2 + V3.up * M.Sin(radians * M.PI) + V3.up * -.25f, new V3(1, .5f, 0));
        G.DrawWireCube(V3.right * 3 + V3.up * M.Sin(radians * M.PI / 2) + V3.up * -.25f, new V3(1, .5f, 0));
        G.DrawWireCube(V3.right * 4 + V3.up * M.Sin(radians * M.PI / 2) * .5f + V3.up * -.25f, new V3(1, .5f, 0));
    }
    private void DrawPendulum()
    {
        //Draw Pendulum
        float deg = sine * maxDeg;
        float rad = M.Deg2Rad * deg;
        V3 pundulumPos = new V3(M.Sin(rad * timeScale), -M.Cos(rad * timeScale), 0);
        G.DrawWireSphere(pundulumPos + V3.left * 3, .2f);
        G.DrawLine(V3.zero + V3.left * 3, pundulumPos + V3.left * 3);
    }
    private void AxialInput()
    {
        offset = axialOffset;

        if(M.Abs(Input.GetAxis("Horizontal")) > 0 || M.Abs(Input.GetAxis("Vertical")) > 0)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        G.DrawWireCube(offset, new V3(2, 2, 0));
        G.color = C.magenta;
        G.DrawLine(offset, (Vector3)input + offset);

        var x = input.x;
        var y = input.y;
        G.color = C.red;
        G.DrawLine(offset, offset + V3.right * x);
        G.color = C.green;
        G.DrawLine(offset, offset + V3.up * y);
        G.color = C.black * .5f;
        G.DrawRay(offset, new V3(x, y).normalized);
        

    }
}
