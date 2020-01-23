using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class TimelineBattleTest : MonoBehaviour
{
    public ActionSelector actionSelector;
    public TargetSelector targetSelector;
    public List<TimelineObject> timelineObjects = new List<TimelineObject>();

    public List<TimelineAvatar> avatars = new List<TimelineAvatar>();

    public float width = 10;
    float Height => avatars.Count;

    public float currentTime = 0;
    private Transform currentAvatar;

    IEnumerator Start()
    {
        while (true)
        {
            for (int i = 0; i < timelineObjects.Count; i++)
            {
                var timelineObject = timelineObjects[i];
                if (currentTime > timelineObject.start + timelineObject.length)
                {
                    timelineObject.avatar.hasQueuedAction = false;
                    timelineObjects.Remove(timelineObject);
                }
            }
            for (int i = 0; i < avatars.Count; i++)
            {
                var avatar = avatars[i];

                if (!avatar.hasQueuedAction)
                {
                    avatar.hasQueuedAction = true;
                    yield return StartCoroutine(SelectAction(avatar));
                }
            }

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    enum SelectState { Action, Target, Path, Done}
    IEnumerator SelectAction(TimelineAvatar avatar)
    {
        var actionIsReady = false;
        var selectionState = SelectState.Action;
        currentAvatar = avatar.transform;
        while (!actionIsReady)
        {
            switch (selectionState)
            {
                case SelectState.Action:
                    actionSelector.ShowSelection(avatar);
                    while (!actionSelector.actionSelected)
                        yield return null;
                    selectionState = SelectState.Target;
                    break;
                case SelectState.Target:
                    targetSelector.ShowTargets(avatar);
                    while (!targetSelector.selectedTarget)
                        yield return null;
                    
                    selectionState = SelectState.Done;
                    break;
                case SelectState.Path:
                    //TODO: Make path function, Component and object

                    break;
                case SelectState.Done:
                    actionIsReady = true;
                    break;
                default:
                    break;
            }
            yield return null;
        }
        currentAvatar = null;

        timelineObjects.Add(new TimelineObject(avatar, actionSelector.selectedAction, targetSelector.selectedTarget, currentTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white * .7f;
        Gizmos.DrawWireCube(transform.position + Vector3.right * width / 2, new Vector3(width, Height));

        for (int i = 0; i < timelineObjects.Count; i++)
        {
            var to = timelineObjects[i];
            var ChargePos = new Vector2(to.start + to.length / 2, i - Height / 2 + .5f);
            var size = new Vector2(to.length, 1);
            Gizmos.color = to.avatar.color * .8f;
            Gizmos.DrawCube(ChargePos, size);
            
        }

        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(currentTime, Height / 2), new Vector3(currentTime, -Height / 2));

        if(currentAvatar)
            Gizmos.DrawWireSphere(currentAvatar.position + Vector3.forward * 2, .6f);
    }
}

[System.Serializable]
public class TimelineObject
{
    public float length;
    public float start;
    public Action action;
    public TimelineAvatar avatar;

    public TimelineObject(TimelineAvatar avatar, Action action, TimelineAvatar selectedTarget, float startTime)
    {
        this.action = action;
        this.length = action.length;
        this.start = startTime;
        this.avatar = avatar;
    }
}