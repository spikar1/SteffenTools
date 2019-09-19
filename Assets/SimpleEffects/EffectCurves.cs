using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCurves : MonoBehaviour
{
    public AnimationCurve linear = new AnimationCurve(
        new Keyframe(0,0,0,0,0,0), 
        new Keyframe(1, 1,0,0,0,0));
    public AnimationCurve linearEase = new AnimationCurve(
        new Keyframe(0, 0), 
        new Keyframe(1, 1));
    public AnimationCurve bell = new AnimationCurve(
        new Keyframe(0, 0, 0, 0, 0, 0), 
        new Keyframe(.5f, 1, 0, 0, 1, 1),
        new Keyframe(1, 0, 0, 0, 0, 0));

    [ContextMenu("ReadValues")]
    public void ReadValues()
    {
        Debug.Log("time - "+ bell.keys[0].time);
        Debug.Log("value - "+ bell.keys[0].value);
        Debug.Log("inTan - "+ bell.keys[0].inTangent);
        Debug.Log("outTan - "+ bell.keys[0].outTangent);
        Debug.Log("inWgt - "+ bell.keys[0].inWeight);
        Debug.Log("outWgt - "+ bell.keys[0].outWeight);
    }
}
