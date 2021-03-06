﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineAvatar : MonoBehaviour
{
    public bool hasQueuedAction;
    public List<Action> actions = new List<Action>() { new Action("Attack", 1) };
    public Color color;


    public float MoveSpeed = 5;

    private void Awake() {
        GetComponent<MeshRenderer>().material.color = color;
    }
}

[System.Serializable]
public class Action
{
    public string name;
    public float length;
    public float coolDown;
    public float charge;
    public float range;

    public Action(string name, float length, float coolDown = 0, float range = 0)
    {
        this.name = name;
        this.length = length;
        this.coolDown = coolDown;
        this.range = range;
    }
}