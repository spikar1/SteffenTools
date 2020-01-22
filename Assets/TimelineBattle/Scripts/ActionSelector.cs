using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public List<GameObject> buttons;
    public bool actionSelected;
    //public Action selectedAction;

   /* async public void PlanAction(TimelineAvatar avatar) {
        Task<Action> action = SelectAction(avatar);
        Task<TimelineAvatar> targetAvatar = selectTarget(avatar);

        
    }

    async Task<Action> SelectAction(TimelineAvatar avatar) {
        RemoveButtons();

    }
    async Task selectTarget() {
        var availableTargets = GameObject.FindObjectsOfType<TimelineAvatar>();

        for (int i = 0; i < availableTargets.Length; i++) {
            var avatar = availableTargets[i];
            print(avatar.gameObject.name + " is number " + i);
        }
        Task<TimelineAvatar> selectedAvatar = SelectionInput(availableTargets);
        
    }

    TimelineAvatar SelectionInput(TimelineAvatar[] availableAvatars) {


    }*/

    public void ShowSelection(TimelineAvatar avatar)
    {
        RemoveButtons();
        actionSelected = false;

        for (int i = 0; i < avatar.actions.Count; i++)
        {
            /*var action = avatar.actions[i];
            ActionButton button = Instantiate(buttonPrefab, transform).GetComponent<ActionButton>();
            button.Initialize(action, this);
            buttons.Add(button.gameObject);*/

            var action = avatar.actions[i];
            Button button = Instantiate(buttonPrefab, transform).GetComponent<Button>();
            var a = action;
            button.onClick.AddListener(delegate { SetAction(action); });
            button.transform.GetChild(0).GetComponent<Text>().text = action.name;

            buttons.Add(button.gameObject);
        }
    }

    void SetAction(Action action) {
        print(action.name);
    }

    public void RemoveButtons()
    {
        int failsafe = 0;
        for (int i = buttons.Count - 1; i >= 0; i--)
        {
            Destroy(buttons[i].gameObject);
            failsafe++;
            if (failsafe > 1000)
                throw new System.Exception("Failed");
        }
    }
}
