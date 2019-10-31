using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateJsonClass : MonoBehaviour
{

    public Foo MyFoo;
    static string path = "Assets/Resources/test.txt";

    [ContextMenu("Load")]
    void LoadJSON() {
        //TextAsset asset = Resources.Load<TextAsset>(path);

        MyFoo = JsonUtility.FromJson<Foo>(File.ReadAllText(path));
    }

    [ContextMenu("Save")]
    void SaveJSON() {


        /*var f = new Foo();
        f.floatList = new List<float>();
        f.myInt = -99;*/

        string json = JsonUtility.ToJson(MyFoo, true);

        File.WriteAllText(path, json);

    }
}

[System.Serializable]
public class Foo
{
    public int myInt;
    public List<float> floatList;
}