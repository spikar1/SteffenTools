using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEffects2D : MonoBehaviour
{



    EffectCurves curve;
    private void Start()
    {
        if (curve == null)
            curve = new GameObject().AddComponent<EffectCurves>();
        ScaleEffect(transform, curve.bell, .2f, 1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Start();
    }


    Coroutine scaleCor;
    public void ScaleEffect(Transform affected, AnimationCurve curve, float time, float apex)
    {


        if (scaleCor != null)
            StopCoroutine(scaleCor);
        scaleCor = StartCoroutine(StartScaleEffect(affected, curve, time, apex));
    }

    Vector3 orgScale;
    IEnumerator StartScaleEffect(Transform affected, AnimationCurve curve, float time, float apex)
    {
        Vector3 orgScale = affected.localScale;
        float t = 0;
        while (t < time)
        {
            Evaluate();
            t += Time.deltaTime;
            yield return null;
        }
        t = time;
        Evaluate();

        void Evaluate()
        {
            affected.localScale = orgScale * (1 + curve.Evaluate(t / time) * apex);
        }
    }
}
