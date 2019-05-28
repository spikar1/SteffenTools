using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Westerdals_Del1 : MonoBehaviour
{
    //Lære om spillmotorer
    //Enkle "Draw" funksjoner
    //tegne kvadrat, rektangel, trekant
    //"Gravity!"
    [SerializeField]
    Color mainColor = Color.white;

    public List<Doodads> doodads = new List<Doodads>();

    public float a = 100;

    public Vector[] polyPoints;

    private void Update()
    {
        DrawLine(0, 100, 200, 250, Color.red);
        DrawLine(0, 0, 10, 10, Color.red);

        DrawPolygon(
            new Vector(100, 100),
            new Vector(200, 300),
            new Vector(300, 100),
            new Vector(200, 150)
            );

        polyPoints = RotateVectors(polyPoints, Time.deltaTime);
        DrawPolygon(polyPoints);
        
        /*DrawLine(100, 0, 100, 600, Color.red);
        DrawLine(90, 0, 90, 600, Color.red);
        DrawLine(80, 0, 80, 600, Color.red);
        DrawLine(70, 0, 70, 600, Color.red);

        DrawLine(new Vector(400, 400), new Vector(450, 450), mainColor);

        DrawTrapezoid(10, 10, 50, 10, 20, 20, 10, 20, Color.white);

        DrawRectangle(200, 200, 100, 30, Color.green);*/

       // DrawAstroid(new Vector(300, 300), 3);
    }

    Vector[] RotateVectorsAroundOrigin(Vector[] vectors, Vector origin, float angle)
    {
        

        var s = Mathf.Sin(angle);
        var c = Mathf.Cos(angle);
        var newVectors = new Vector[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            var v = vectors[i];
            v.x -= origin.x;
            v.y -= origin.y;

            float newX = v.x * c - v.y * s;
            float newY = v.x * s + v.y * c;

            v.x = newX + origin.x;
            v.y = newY + origin.y;
            newVectors[i] = v;
        }
        return newVectors;
    }

    Vector[] RotateVectors(Vector[] vectors, float angle)
    {
        float cx = 0;
        float cy = 0;
        for (int i = 0; i < vectors.Length; i++)
        {
            cx += vectors[i].x;
            cy += vectors[i].y;
        }
        cx = cx / vectors.Length;
        cy = cy / vectors.Length;

        return RotateVectorsAroundOrigin(vectors, new Vector(cx, cy), angle);
    }

    #region Draw Functions
    /// <summary>
    /// Draw a line from a to b
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    void DrawLine(float x1, float y1, float x2, float y2, Color color)
    {
        Debug.DrawLine(new Vector3(x1, y1), new Vector3(x2, y2), color);
    }

    void DrawLine(Vector a, Vector b, Color color)
    {
        DrawLine(a.x, a.y, b.x, b.y, color);
    }
    void DrawQuadrilateral(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, Color color)
    {
        DrawLine(x1, y1, x2, y2, color);
        DrawLine(x2, y2, x3, y3, color);
        DrawLine(x3, y3, x4, y4, color);
        DrawLine(x4, y4, x1, y1, color);
    }

    void DrawPolygon(Color color,params Vector[] vectors)
    {
        for (int i = 0; i < vectors.Length-1; i++)
        {
            DrawLine(vectors[i], vectors[i + 1], color);
        }
        DrawLine(vectors[vectors.Length-1], vectors[0], color);
    }

    void DrawPolygon(params Vector[] vectors)
    {
        if (vectors == null || vectors.Length < 1)
            return;
        for (int i = 0; i < vectors.Length - 1; i++)
        {
            DrawLine(vectors[i], vectors[i + 1], Color.white);
        }
        DrawLine(vectors[vectors.Length-1], vectors[0], Color.white);
    }

    void DrawRectangle(float posX, float posY, float width, float height, Color color)
    {
        DrawQuadrilateral(posX, posY, posX, posY + height, posX + width, posY + height, posX + width, posY, Color.red);
            
    }

    void DrawAstroid(Vector pos, float size)
    {
        for (int i = 0; i < 6; i++)
        {
            float t = ((float)i/6) * Mathf.PI * 2;
            float t2 = ((float)i + 1 / 6) *Mathf.PI * 2;
            DrawLine(pos + new Vector(Mathf.Sin(t), Mathf.Cos(t)), pos + new Vector(Mathf.Sin(t), Mathf.Cos(t)), Color.white);
        }
    }
    #endregion
}

[System.Serializable]
public struct Vector
{
    public float x, y;

    public Vector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static Vector New(float x, float y)
    {
        Vector v = new Vector();
        v.x = x;
        v.y = y;
        return v;
    }

    public static Vector operator +(Vector a, Vector b)
    {
        for (int i = 0; i < 1000; i++)
        {
            Debug.Log("It works");
        }
        return new Vector(a.x + b.x, a.y + b.y);
    }
}


public static class VectorExtensions
{
    public static
        Vector Add(this Vector a, Vector b)
    {
        return new Vector(a.x + b.x, a.y + b.y);
    }

}

[System.Serializable]
public class Doodads
{
    public Vector position;
    public float width, height;
}