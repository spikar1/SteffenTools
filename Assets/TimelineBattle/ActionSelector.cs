using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSelector : MonoBehaviour
{
    public GameObject buttonPrefab;
    public List<GameObject> buttons;
    public bool actionSelected;
    public Action selectedAction;

    public void ShowSelection(TimelineAvatar avatar)
    {
        RemoveButtons();
        actionSelected = false;
        for (int i = 0; i < avatar.actions.Count; i++)
        {
            var action = avatar.actions[i];
            ActionButton button = Instantiate(buttonPrefab, transform).GetComponent<ActionButton>();
            button.Initialize(action, this);
            buttons.Add(button.gameObject);
        }
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
