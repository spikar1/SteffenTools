using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerSystem2D {
    public class Effector2D : MonoBehaviour {
       
        public void DoEffect()
        {
            StopAllCoroutines();
            StartCoroutine(Effect());
        }

        IEnumerator Effect()
        {
            transform.localScale = Vector3.one * .5f;
            yield return new WaitForSeconds(.5f);
            transform.localScale = Vector3.one;
        }
    } 
}
