using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript2D : MonoBehaviour
{
    #region Parameters
    public float playerSpeed = 3;
    #endregion


    private Rigidbody2D rb;
    Rigidbody2D Rb{
        get {
            if (!rb)
                rb = GetComponent<Rigidbody2D>();
            return rb;
        }
    }

    private void FixedUpdate() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input *= playerSpeed;

        Vector2 v = Rb.velocity;                //Get velocity
        v = new Vector2(input.x, input.y);      //Change velocity
        rb.velocity = v;                        //Apply new velocity
        
    }
}
