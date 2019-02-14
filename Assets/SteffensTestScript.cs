    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;
using SteffenTools.NodeSystem;

public class SteffensTestScript : MonoBehaviour {
    public Node[] nodes;

    public Transform seeker, target;
    public  LayerMask seekablesLayer;

    float angle;

    public enum State { Nothing, Forward, Right, Back, Left}
    public State state = State.Nothing;

    void LateUpdate() {

        //
        if(Input.GetKey(KeyCode.L))
            loops += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.K))
            resolution ++;
        //
        if (Input.GetKeyDown(KeyCode.A)) {
            foreach (var n in nodes) {
                n.RotateCCW();
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            foreach (var n in nodes) {
                n.RotateCW();
            }
        }

        nodes.Visualize(Color.yellow * .5f, .4f);
        nodes.Visualize(Color.red, .35f);

        

        DebugDrawers.DrawArc(transform.position, Vector3.zero, Color.yellow, 1);

        //rotate = Time.time * 180;

        Debug.DrawRay(seeker.position, seeker.forward);
        Debug.DrawRay(target.position, target.forward);

        DebugDrawers.DrawCircle(seeker.position, 3, Axis.Y);
        Collider[] c = Physics.OverlapSphere(seeker.position, 3,  seekablesLayer);

        if(c.Length > 0)
        {
            print("Found " + target.name);

            Debug.DrawLine(seeker.position, target.position);

            angle = Vector3.Angle(seeker.forward, target.position - seeker.position);

            angle = Vector3.SignedAngle(seeker.forward, target.position - seeker.position, Vector3.up);
            var a = angle;

            if (a > -45 && a < 45)
                state = State.Forward;
            else if (a > 45 && a < 135)
                state = State.Right;
            else if (a > -135 && a < -45)
                state = State.Left;
            else if (a > 135 || a < -135)
                state = State.Back;
            else
                state = State.Nothing;
        }
        else
            state = State.Nothing;  
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "angle " + angle.ToString());
    }

    private void OnDrawGizmos()
    {



        DrawCoil(transform.position);
        //DrawCoil(Vector3.zero);
    }
    #region Coil
    [Header("Coil")]
    public float loops = 2;
    public float Loops {
        get {
            return (float)((int)(loops * resolution))/resolution;
        }
    }
    public int resolution = 16;
    public float height = 1;
    public bool animateHeight = false;
    public float radius = 1;
    public float rotate = 0f;
    public bool fadeColor = true;
    public AnimationCurve curve = AnimationCurve.Constant(0, 1, 1);

    private LineRenderer lr;
    private List<Vector3> lrPos = new List<Vector3>();
    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void DrawCoil(Vector3 offset)
    {
        var tempHeight = height;
        if (animateHeight)
            tempHeight = Mathf.Sin((float)UnityEditor.EditorApplication.timeSinceStartup) * height * .5f + height * .5f;

        Gizmos.color = Color.white;

        Vector3 pos;
        Vector3 lastPos = pos = Vector3.zero;
        //LineRender
        lrPos.Clear();
        if (lr)
            lr.positionCount = (int)(resolution * loops);
        for (int i = 0; i <= resolution * Loops; i++)
        {
            if (loops <= 0)
                break;
            float f = ((float)(i) / resolution) * Mathf.PI * 2;
            float radian = f + rotate * (Mathf.PI * 2) / 360;
            float c = f / (Mathf.PI * 2) / loops;


            float r = radius * curve.Evaluate(c);

            pos = new Vector3(
                Mathf.Cos(radian) * r, 
                f * tempHeight / Loops / (Mathf.PI*2), 
                Mathf.Sin(radian) * r
                ) + offset;
            if (fadeColor)
                Gizmos.color = new Color(c, c, c);

            if (i == 0)
            {
                lastPos = pos;
                continue;
            }
            Gizmos.DrawLine(lastPos, pos);
            lastPos = pos;
            lrPos.Add(pos);
            //LineRender
        }
        lrPos.Add(lastPos);
        if (lr)
            lr.SetPositions(lrPos.ToArray());
    }
    #endregion
}


