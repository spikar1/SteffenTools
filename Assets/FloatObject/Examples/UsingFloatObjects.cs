using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.FloatObject;

public class UsingFloatObjects : MonoBehaviour
{
    public float myFloat;
    public FloatObject myFloatObject;
    public FloatObject myFloatObject1;
    public FloatObject myFloatObject2;
    public float myNormieFloat = 2;

    private void Update()
    {
        if(Time.time > myFloatObject)
        Debug.Log(myFloatObject + 1);
    }
}
