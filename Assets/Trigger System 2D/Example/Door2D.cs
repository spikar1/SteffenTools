using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2D : MonoBehaviour
{
    #region Parameters
    [Tooltip("Meters per second")]
    public float doorSpeed = 10;
    [Tooltip("Amount of meters to move door")]
    public float openDist = 3;
    #endregion


    private Vector3 orgPos;
    private void Awake() {
        orgPos = transform.position;
    }

    public void SetDoorOpen(bool open) {
        StopAllCoroutines();
        if (open) {
            StartCoroutine(GoToPos(orgPos + Vector3.up * openDist));
        }
        else {
            StartCoroutine(GoToPos(orgPos));
        }
    }

    IEnumerator GoToPos(Vector3 targetPos) {
        while(Vector3.Distance(transform.position, targetPos) > .05f) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * doorSpeed);
            yield return null;
        }
    }
}
