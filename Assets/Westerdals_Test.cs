using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Westerdals_Test : MonoBehaviour
{
    //Lære om spillmotorer
    //Enkle "Draw" funksjoner
    //tegne kvadrat, rektangel, trekant
    //"Gravity!"
    [SerializeField]
    Color mainColor = Color.white;

    public List<Doodads> doodads = new List<Doodads>();

    public float a = 100;

    public Point[] polyPoints;
    [Header("Ship")]
    public List<Point> shipPoints;
    public Point shipPosition = new Point(300, 550);
    public float shipSpeed = 30;

    private void Awake() {
        shipPoints.Add(new Point(-25, 0));
        shipPoints.Add(new Point(0, -35));
        shipPoints.Add(new Point(25, 0));
        shipPoints.Add(new Point(0, -13));
    }

    private void Update()
    {

        DrawQuadrilateral(90, 60, 140, 80, 130, 120, 60, 120, Color.white);


        /*
        
        DrawLine(0, 100, 200, 250, Color.red);
        DrawLine(0, 0, 10, 10, Color.red);

        DrawPolygon(
            new Point(100, 100),
            new Point(200, 300),
            new Point(300, 100),
            new Point(200, 150)
            );
        polyPoints = RotateVectorsAroundOrigin(polyPoints, new Point(400, 300), Time.deltaTime);
        DrawPolygon(polyPoints);


        if (Input.GetKey(KeyCode.A))
            shipPosition.x -= Time.deltaTime * shipSpeed;
        if (Input.GetKey(KeyCode.D))
            shipPosition.x += Time.deltaTime * shipSpeed;
        if (Input.GetKey(KeyCode.W))
            shipPosition.y -= Time.deltaTime * shipSpeed;
        if (Input.GetKey(KeyCode.S))
            shipPosition.y += Time.deltaTime * shipSpeed;

        DrawPolygon(shipPosition, shipPoints.ToArray());

        //DrawAstroid(new Point(500, 100), 70, 0);*/
    }



    Point[] RotateVectorsAroundOrigin(Point[] vectors, Point origin, float angle)
    {
        var s = Mathf.Sin(angle);
        var c = Mathf.Cos(angle);
        var newVectors = new Point[vectors.Length];
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

    Point[] RotateVectors(Point[] vectors, float angle)
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

        return RotateVectorsAroundOrigin(vectors, new Point(cx, cy), angle);
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

    void DrawLine(Point a, Point b, Color color)
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

    void DrawPolygon(Color color,params Point[] vectors)
    {
        for (int i = 0; i < vectors.Length-1; i++)
        {
            DrawLine(vectors[i], vectors[i + 1], color);
        }
        DrawLine(vectors[vectors.Length-1], vectors[0], color);
    }
    void DrawPolygon(params Point[] vectors) {
        DrawPolygon(new Point(0,0), vectors);
    }

    void DrawPolygon(Point offset, params Point[] vectors)
    {
        if (vectors == null || vectors.Length < 1)
            return;
        for (int i = 0; i < vectors.Length - 1; i++)
        {
            DrawLine(vectors[i] + offset, vectors[i + 1] + offset, Color.white);
        }
        DrawLine(vectors[vectors.Length-1] + offset, vectors[0] + offset, Color.white);
    }

    void DrawRectangle(float posX, float posY, float width, float height, Color color)
    {
        DrawQuadrilateral(posX, posY, posX, posY + height, posX + width, posY + height, posX + width, posY, Color.red);
            
    }

    void DrawAstroid(Point pos, float size, int seed)
    {
        Random.InitState(seed);
        for (int i = 0; i < 8; i++)
        {
            float t = (float)i/8 * Mathf.PI * 2;
            float t2 = (float)(i + 1) / 8 *Mathf.PI * 2;
            DrawLine(
                pos + new Point(Mathf.Sin(t), Mathf.Cos(t)) * size * Random.value, 
                pos + new Point(Mathf.Sin(t2), Mathf.Cos(t2)) * size * Random.value,
                Color.white);
        }
    }
    #endregion
}

[System.Serializable]
public struct Point
{
    public float x, y;

    public Point(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point New(float x, float y)
    {
        Point v = new Point();
        v.x = x;
        v.y = y;
        return v;
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }
    public static Point operator *(Point a, float f)
    {
        return new Point(a.x * f, a.y * f);
    }
}


public static class VectorExtensions
{
    public static
        Point Add(this Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }

}

[System.Serializable]
public class Doodads
{
    public Point position;
    public float width, height;
}