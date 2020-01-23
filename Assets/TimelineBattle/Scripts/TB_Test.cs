using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_Test : MonoBehaviour
{
    public Vector2 pos1, vel1;
    public Vector2 pos2;
    public float spd2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pos1, .2f);

        Gizmos.color = Color.red;
    }
}
