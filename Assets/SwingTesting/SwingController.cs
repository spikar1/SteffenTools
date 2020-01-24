using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public Vector3 swingObjectPosition;

    IEnumerator Start()
    {

        while (!Input.GetKeyDown(KeyCode.Space))
        {

            yield return null;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(swingObjectPosition, .3f);
    }
}
