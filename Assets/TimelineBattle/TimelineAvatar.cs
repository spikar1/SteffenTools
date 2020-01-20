using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineAvatar : MonoBehaviour
{
    public bool busy;
    public List<Action> actions = new List<Action>() { new Action("Attack", 1) };
    public Color color;
}

[System.Serializable]
public class Action
{
    public string name;
    public float length;

    public Action(string name, float length)
    {
        this.name = name;
        this.length = length;
    }
}