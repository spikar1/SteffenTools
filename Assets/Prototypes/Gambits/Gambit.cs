using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gambit : MonoBehaviour
{
    int a;
    int b;
    private void Update() {
        a++;
        if(a > 100) {
            print("a wins");
        }
    }
    private void FixedUpdate() {
        b++;
        if (b > 100) {
            print("b wins");
        }
    }
}

class JunkYard
{
    /*int health = 5;
    public float maxSpeed = 10;
    Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float input = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(input * maxSpeed * Time.deltaTime, rb.velocity.y);

        if (health <= 0) {
            bool shouldDie = true;
        }

        if (shouldDie == true) {
            Destroy(gameObject);
        }
    }*/
}