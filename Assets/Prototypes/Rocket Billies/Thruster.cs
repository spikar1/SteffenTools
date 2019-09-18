using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public KeyCode thrustKey = KeyCode.Space;
    public Rigidbody2D shipRb;
    public float thrusterPower = 2;

    private void Start()
    {
        if (!shipRb)
            shipRb = transform.GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(thrustKey))
        {
            shipRb.AddForceAtPosition(transform.up * thrusterPower, transform.position);
        }
    }
}
