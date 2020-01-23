using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_Test : MonoBehaviour
{
    // === variables you need ===
    //how fast our shots move
    public float shotSpeed;
    //objects
    GameObject shooter;
    GameObject target;


    
    //now use whatever method to launch the projectile at the intercept point

    public Vector2 pos1, vel1;
    public float spd1 => vel1.magnitude;
    public Vector2 pos2;
    public float spd2;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pos1, .2f);
        Gizmos.DrawRay(pos1, vel1);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pos2, .2f);

        /* 
         Gizmos.DrawWireSphere(pos1, spd1);

         
         //Gizmos.DrawRay(pos2, vel2);
         Gizmos.DrawWireSphere(pos2, spd2);

         Gizmos.color = Color.gray;
         Gizmos.DrawWireSphere(pos1, Mathf.Sqrt((spd1 * spd1) + (spd2 * spd2)));
         Gizmos.DrawWireSphere(pos2, Mathf.Sqrt((spd2 * spd2) + (spd1 * spd1)));*/

        //calculate intercept
        Vector3 interceptPoint = FirstOrderIntercept
        (
            pos2,
            Vector3.zero,
            spd2,
            pos1,
            vel1
        );

        Gizmos.DrawLine(pos2, interceptPoint);
    }

    //first-order intercept using absolute target position
    public static Vector3 FirstOrderIntercept
    (
        Vector3 shooterPosition,
        Vector3 shooterVelocity,
        float shotSpeed,
        Vector3 targetPosition,
        Vector3 targetVelocity
    ) {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    ) {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f) {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f) { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f) {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }
}