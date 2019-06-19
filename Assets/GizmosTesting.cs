using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;

public class GizmosTesting : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        AdditionalGizmos.DrawArrow(transform.position, transform.forward);
    }
}
