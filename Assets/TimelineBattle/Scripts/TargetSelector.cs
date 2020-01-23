using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public TimelineAvatar selectedTarget;

    List<TimelineAvatar> targetableAvatars = new List<TimelineAvatar>();

    public void ShowTargets(TimelineAvatar avatar)
    {
        selectedTarget = null;
        StartCoroutine(ShowTargetsCoroutine(avatar));
    }

    private void Update()
    {
        
    }

    public IEnumerator ShowTargetsCoroutine(TimelineAvatar avatar)
    {
        targetableAvatars = FindObjectsOfType<TimelineAvatar>().ToList();

        TimelineAvatar avatarSelected = null;
        for (int i = 0; i < targetableAvatars.Count; i++)
        {
            var targetAvatar = targetableAvatars[i];
            
        }
        while (avatarSelected == null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                print("Getting Input");
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.up, Color.white, 1);
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var col = Physics2D.OverlapPoint(point);
                if (!col)
                {
                    yield return null;
                    continue;
                }
                var clickedAvatar = col.GetComponent<TimelineAvatar>();
                if (clickedAvatar)
                {
                    selectedTarget = avatarSelected = clickedAvatar;
                    targetableAvatars.Clear();
                }
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < targetableAvatars.Count; i++)
        {
            var avatar = targetableAvatars[i];
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(avatar.transform.position, Vector3.one * 1.1f);
        }
    }
}
