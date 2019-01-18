using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    #region Parameters
    public float playerSpeed = 3;
    #endregion


    private Rigidbody rb;
    Rigidbody Rb{
        get {
            if (!rb)
                rb = GetComponent<Rigidbody>();
            return rb;
        }
    }

    private void FixedUpdate() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input *= playerSpeed;

        Vector3 v = Rb.velocity;                //Get velocity
        v = new Vector3(input.x, v.y, input.y); //Change velocity
        rb.velocity = v;                        //Apply new velocity
        
    }
}
