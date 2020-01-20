using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TO = TimelineObject;
using C = UnityEngine.Color;
using G = UnityEngine.Gizmos;
using TA = TimelineAvatar;


public class TimelineBattleTest : MonoBehaviour
{
    public ActionSelector actionSelector;
    public List<TO> timelineObjects = new List<TO>();

    public List<TA> avatars = new List<TA>();

    public float width = 10;
    float Height => avatars.Count;

    public float currentTime = 0;

    IEnumerator Start()
    {
        while (true)
        {
            for (int i = 0; i < timelineObjects.Count; i++)
            {
                var to = timelineObjects[i];
                if (currentTime > to.start + to.length)
                {
                    to.avatar.busy = false;
                    timelineObjects.Remove(to);
                }
            }
            for (int i = 0; i < avatars.Count; i++)
            {
                var avatar = avatars[i];

                if (!avatar.busy)
                {
                    avatar.busy = true;
                    yield return StartCoroutine(SelectAction(avatar));
                }
            }

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator SelectAction(TA avatar)
    {
        actionSelector.ShowSelection(avatar);
        while (!actionSelector.actionSelected)
        {
            yield return null;
        }
        timelineObjects.Add(new TO(avatar, actionSelector.selectedAction, currentTime));
    }

    private void OnDrawGizmos()
    {
        G.color = C.white * .7f;
        G.DrawWireCube(transform.position + Vector3.right * width / 2, new Vector3(width, Height));

        for (int i = 0; i < timelineObjects.Count; i++)
        {
            var to = timelineObjects[i];
            var pos = new Vector2(to.start + to.length / 2, i - Height / 2 + .5f);
            var size = new Vector2(to.length, 1);
            G.color = to.avatar.color * .8f;
            G.DrawCube(pos, size);
            
        }

        G.color = C.white;
        G.DrawLine(new Vector3(currentTime, Height / 2), new Vector3(currentTime, -Height / 2));
    }
}

[System.Serializable]
public class TimelineObject
{
    public float length;
    public float start;
    public Action action;
    public TA avatar;

    public TimelineObject(TimelineAvatar avatar, Action action, float startTime)
    {
        this.action = action;
        this.length = action.length;
        this.start = startTime;
        this.avatar = avatar;
    }
}