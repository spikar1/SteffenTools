using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.FloatObject;

public class UsingFloatObjects : MonoBehaviour
{
    public FloatObject printDelay;

    private void Update()
    {
        if(Time.time > printDelay)
        Debug.Log("hello");
    }
}
