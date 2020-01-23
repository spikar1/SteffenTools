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
    public Action selectedAction;

    public void ShowSelection(TimelineAvatar avatar)
    {
        RemoveButtons();
        actionSelected = false;

        for (int i = 0; i < avatar.actions.Count; i++)
        {
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
        actionSelected = true;
        selectedAction = action;
        RemoveButtons();
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
