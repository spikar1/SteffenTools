﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Vector2 gravity = new Vector3(0, -.314f, 0);
    public float jumpForce = .3f;

    public Unit[] units;

    public float minY = -4;
    public float maxY = 4;

    void Start()
    {
        Debug.Log("Hello World!");
    }

    void Update()
    {

        foreach (var unit in units)
        {
            unit.velocity += gravity * Time.deltaTime;

            if (unit.position.y < minY)
            {
                unit.velocity = Vector2.zero;
                unit.position.y = minY;
            }
            if (unit.position.y > maxY - unit.height)
            {
                unit.velocity = Vector2.zero;
                unit.position.y = maxY - unit.height;
            }
            foreach (var other in units)
            {
                if (other == unit)
                    continue;
                if (unit.Overlap(other))
                    unit.velocity = Vector2.zero;
            }

            unit.Update();
            DrawSquare(unit.position, unit.width, unit.height, unit.color);
        }

        units[0].Overlap(units[1]);

        Debug.DrawLine(new Vector2(-10, minY), new Vector2(10, minY), Color.white);
        Debug.DrawLine(new Vector2(-10, maxY), new Vector2(10, maxY), Color.white);
    }



    void DrawSquare(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color color)
    {
        Debug.DrawLine(a, b, color);
        Debug.DrawLine(b, c, color);
        Debug.DrawLine(c, d, color);
        Debug.DrawLine(d, a, color);
    }
    void DrawSquare(Vector3 pos, float width, float height, Color color)
    {
        DrawSquare(
            pos + new Vector3(-width / 2, 0),
            pos + new Vector3(-width / 2, height),
            pos + new Vector3(width / 2, height),
            pos + new Vector3(width / 2, 0),
            color);
    }

    void DrawTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        DrawTriangle(a, b, c, Color.white);
    }
    void DrawTriangle(Vector3 a, Vector3 b, Vector3 c, Color color)
    {
        Debug.DrawLine(a, b, color);
        Debug.DrawLine(b, c, color);
        Debug.DrawLine(c, a, color);
    }
    void DrawTriangle(Vector3 pos, float width, float height)
    {
        DrawTriangle(pos, width, height, Color.white);
    }
    void DrawTriangle(Vector3 pos, float width, float height, Color color)
    {
        DrawTriangle(
            pos + new Vector3(-width / 2, 0),           //lower left point
            pos + new Vector3(0, height),               //upper middle
            pos + new Vector3(width / 2, 0), color);    //lower right
    }

    private void OnDrawGizmos()
    {
        foreach (var item in units)
        {
            //Gizmos.DrawWireSphere(item.position, .2f);
        }
    }
}

[System.Serializable]
public class Unit
{
    public string name = "Unit";
    public Vector2 position;
    public Vector2 velocity;
    public float width = 1, height = 1;
    public Color color = Color.white;

    public Unit()
    {
        name = "New Unit";
        position = new Vector2();
        velocity = new Vector2();
        width = 1;
        height = 1;
        color = Color.white;
    }

    public Corners corners {
        get {
            Corners c = new Corners();
            c.upperLeft = position + new Vector2(-width * .5f, height * .5f);
            c.upperRight = position + new Vector2(width * .5f, height * .5f);
            c.lowerLeft = position + new Vector2(-width * .5f, -height * .5f);
            c.lowerRight = position + new Vector2(width * .5f, -height * .5f);
            return c;
        }
    }

    public struct Corners
    {
        public Vector2 upperLeft, upperRight, lowerLeft, lowerRight;
    }

    public void Update()
    {
        position += velocity;
    }

    public bool Overlap(Unit other)
    {
        return Overlap(other, Vector2.zero);
    }

    public bool Overlap(Unit other, Vector2 offset)
    {
        Vector2 pos = position + offset;
        if (pos.x + width * .5f > other.position.x - other.width * .5f &&
            pos.x - width * .5f < other.position.x + other.width * .5f &&
            pos.y + height > other.position.y &&
            pos.y < other.position.y + other.height)
        {
            Debug.Log("Collision between " + name + ", " + other.name);
            return true;
        }
        return false;
    }
}