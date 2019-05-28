using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Westerdals_Del1 : MonoBehaviour
{
    //Lære om spillmotorer
    //Enkle "Draw" funksjoner
    //tegne kvadrat, rektangel, trekant
    //"Gravity!"

    private void Update()
    {
        DrawLine(100, 0, 100, 600, Color.red);
        DrawLine(90, 0, 90, 600, Color.red);
        DrawLine(80, 0, 80, 600, Color.red);
        DrawLine(70, 0, 70, 600, Color.red);
    }

    #region Draw Functions
    void DrawLine(float x1, float y1, float x2, float y2, Color color)
    {
        Debug.DrawLine(new Vector3(x1, y1), new Vector3(x2, y2), color);
    }
    void DrawTrapezoid(float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4, Color color)
    {

    }
    #endregion
}
