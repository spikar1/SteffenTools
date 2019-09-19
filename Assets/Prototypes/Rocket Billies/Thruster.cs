using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    public KeyCode thrustKey = KeyCode.Space;
    public Rigidbody2D shipRb;
    private MeshRenderer mf;
    public float thrusterPower = 2;

    bool isActive = false;
    private Color orgCol;
    Vector3 orgScale;

    private void Start()
    {
        if (!shipRb)
        {
            shipRb = transform.GetComponentInParent<Rigidbody2D>();
        }
        if (!mf)
        {
            mf = GetComponent<MeshRenderer>();
            orgCol = mf.material.color;
            orgScale = transform.localScale;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(thrustKey))
        {
            shipRb.AddForceAtPosition(transform.up * thrusterPower, transform.position);
            if (!isActive)
                PlayEffect();
            isActive = true;
        }
        else
        {
            StopAllCoroutines();
            isActive = false;
            mf.material.color = orgCol;
            transform.localScale = orgScale;
            mf.material.color = orgCol;
        }


    }

    private void PlayEffect()
    {
        StartCoroutine(Effect());
    }

    IEnumerator Effect()
    {
        var t = 0f;

        mf.material.color = Color.white;
        var scale = transform.localScale = orgScale * 1.4f;

        while (t <= 1)
        {
            mf.material.color = Color.Lerp(Color.white, orgCol * 1.2f, t);
            transform.localScale = Vector3.Lerp(scale, orgScale * 1.2f, t);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = orgScale * 1.2f;
        mf.material.color = orgCol * 1.2f;
    }
}
