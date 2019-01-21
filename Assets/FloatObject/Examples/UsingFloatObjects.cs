using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.FloatObject;

public class UsingFloatObjects : MonoBehaviour
{
    public FloatObject printDelay;
    public FloatObject printDelay2;

    private void Update()
    {
        if(Time.time > printDelay)
        Debug.Log("hello");
    }
}
