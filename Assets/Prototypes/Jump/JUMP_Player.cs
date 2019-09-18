﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JUMP_Player : MonoBehaviour
{
    Transform visTrans;
    Rigidbody2D rb;

    public LayerMask playerMask;

    public bool grounded = false;

    public AnimationCurve landingCurve;

    // Start is called before the first frame update
    void Start()
    {
        if (!rb) {
            rb = GetComponent<Rigidbody2D>();
        }
        if (!visTrans) {
            visTrans = transform.GetChild(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {
            rb.velocity = new Vector2(rb.velocity.x, 6);

        }

        Debug.DrawRay(transform.position, rb.velocity*.3f, Color.green);
        if(Physics2D.Raycast((Vector2)transform.position, rb.velocity * .7f, rb.velocity.magnitude * .7f, playerMask)) 
        {
            visTrans.up = Vector3.Lerp(visTrans.up, Vector3.up, 10 * Time.deltaTime);

        }
        else if(rb.velocity.magnitude > .1f && !grounded)
            visTrans.up = Vector3.Lerp(visTrans.up, rb.velocity.normalized, 10 * Time.deltaTime);

        Debug.DrawLine(transform.position + Vector3.left * .5f + Vector3.down * .52f,
            transform.position + Vector3.right * .5f + Vector3.down * .52f, Color.red);
        if (Physics2D.Linecast(transform.position + Vector3.left * .5f + Vector3.down * .52f, transform.position + Vector3.right * .5f + Vector3.down * .52f)) {
            if (grounded == false)
                StartCoroutine(Land());
            grounded = true;
            visTrans.up = Vector3.up;
        }
        else
            grounded = false;
    }

    IEnumerator Land() {
        var t = 0f;
        while (t < 1) {
            transform.localScale = new Vector3(1, landingCurve.Evaluate(t), 1);
            t += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    private void FixedUpdate() {
        if(Mathf.Abs(rb.velocity.x) < 6)
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 7,0));
    }

    /*private void OnCollisionEnter2D(Collision2D collision) {
        visTrans.up = Vector3.up;//collision.contacts[0].normal;
    }*/
}
