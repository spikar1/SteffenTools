using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Action action;
    [HideInInspector]
    public ActionSelector actionSelector;

    public void Initialize(Action action, ActionSelector actionSelector)
    {
        this.actionSelector = actionSelector;
        this.action = action;
        name = action.name;
        transform.GetChild(0).GetComponent<Text>().text = name;
    }

    /*public void OnClicked()
    {
        actionSelector.selectedAction = action;
        actionSelector.actionSelected = true;
        actionSelector.RemoveButtons();
    }*/
}
