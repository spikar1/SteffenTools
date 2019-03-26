using UnityEngine;

public class Snap : MonoBehaviour
{
    [Header("Doesn't work on childed objects...")]
    public bool showGizmo = false;

    public bool snapPosition = true;
    public bool snapRotation = true;
    public bool snapScale = true;

    public bool snapToGround = false;

    public float gridSize = 1;
    float gridFactor;

    Vector3 inputPos;
    Vector3 newSnapPosition = new Vector3(0, 0, 0);
    Vector3 newSnapScale = new Vector3(0, 0, 0);

    Collider col;
    Event currentEvent;

    void Awake()
    {
        enabled = false;
    }

    public void OnGUI()
    {
        currentEvent = Event.current;
        Debug.Log(Event.GetEventCount());
        if (currentEvent.button == 0 && currentEvent.isMouse)
        {
            Debug.Log("left mouse button clicked");
        }
    }

    void OnDrawGizmosSelected()
    {

        //Debug.Log(Event.current);
        /* if (currentEvent.button == 0 && currentEvent.isMouse)
         {
             Debug.Log("left mouse button clicked");
         }*/

        if (enabled == false)
            return;

        if (gridSize != 0)
            gridFactor = 1 / gridSize;
        else
            gridFactor = 1;

        inputPos = transform.localPosition;
        col = GetComponent<Collider>();

        //make input values easier to work with
        newSnapPosition = Vector3Extensions.Round(10 * transform.localPosition) / (10);
        newSnapScale = transform.localScale.Round();
        if (newSnapScale.x % gridSize != 0)
        {
            newSnapScale.x -= newSnapScale.x % gridSize;
        }
        if (newSnapScale.y % gridSize != 0)
        {
            newSnapScale.y -= newSnapScale.y % gridSize;
        }
        if (newSnapScale.z % gridSize != 0)
        {
            newSnapScale.z -= newSnapScale.z % gridSize;
        }

        //Calculate new positions with snap in mind
        newSnapPosition.x = FindSnapPosition(newSnapPosition.x, newSnapScale.x, inputPos.x);
        newSnapPosition.y = FindSnapPosition(newSnapPosition.y, newSnapScale.y, inputPos.y);
        newSnapPosition.z = FindSnapPosition(newSnapPosition.z, newSnapScale.z, inputPos.z);


        //Make sure no object is flat in any axis
        if (newSnapScale.x == 0)
            newSnapScale.x = gridSize;
        if (newSnapScale.y == 0)
            newSnapScale.y = gridSize;
        if (newSnapScale.z == 0)
            newSnapScale.z = gridSize;



        Vector3 gizmoPos;
        Vector3 gizmoScale;

        Gizmos.color = Color.red; //Color of the wireCube.

        //If snap is toggled on, apply transformation changes. Otherwise use original values.
        if (snapPosition)
        {
            transform.localPosition = newSnapPosition;
            gizmoPos = transform.position;
        }
        else
            gizmoPos = transform.position;

        if (snapRotation)
        {
            //snap rotation code goes here
        }

        if (snapScale)
        {

            transform.localScale = newSnapScale;
            gizmoScale = newSnapScale;
        }
        else
            gizmoScale = col.bounds.size;

        if (showGizmo)
            Gizmos.DrawWireCube(gizmoPos, gizmoScale);


        //float scale = Handles.ScaleValueHandle(5, Vector3.zero, Quaternion.identity, 3, Handles.ArrowCap, .5f);


        /* if (snapRotation)
         {*/
        Vector3 newRot;
        newRot.x = Mathf.Round(transform.localRotation.eulerAngles.x / 90) * 90;
        newRot.y = Mathf.Round(transform.localRotation.eulerAngles.y / 90) * 90;
        newRot.z = Mathf.Round(transform.localRotation.eulerAngles.z / 90) * 90;
        transform.localRotation = Quaternion.Euler(newRot);
        //}
    }

    public float FindSnapPosition(float pos, float scale, float refPos)
    {
        if (scale.IsOdd())
        {
            //print (scale * gridFactor);
            pos -= pos % .5f;
            if ((Mathf.Abs(pos - .5f) % 1 == .5f))
            {
                pos += FloatExtensions.Sign(refPos) * .5f;
            }

        }
        else if ((scale * gridFactor).IsEven())
        {
            //print("its even");
            pos = Mathf.Round(pos * gridFactor) / gridFactor;
        }
        return pos;
    }

}