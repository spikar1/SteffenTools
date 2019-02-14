using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu()]
public class JSONStuff : ScriptableObject
{
    public MyClass myClass = new MyClass();
    public List<Article> articles;

    public void Write() {
        string path = Application.dataPath + "/JSON/test.json";
        var json = JsonUtility.ToJson(myClass, true);

        Debug.Log(json);
        
        File.WriteAllText(path, json);  
    }

    public void Read() {
        string path = Application.dataPath + "/JSON/test.json";
        var characters = File.ReadAllText(path);
        
        var jsonObjects = new List<string>();
        
        int i = 0;
        foreach (var c in characters) {

            if(jsonObjects.Count < i + 1) {
                string s = "";
                jsonObjects.Add(s);
            }

            if (c == '}') {
                jsonObjects[i] = string.Concat(jsonObjects[i], c);
                i++;
                if (i > 1000)
                    break;
            }
            else {
                string ss = jsonObjects[i];
                jsonObjects[i] = string.Concat(jsonObjects[i], c);
            }
        }

        articles.Clear();

        foreach (var json in jsonObjects) {
            var temp = JsonUtility.FromJson<Article>(json);
            articles.Add(temp);

        }

    }
}

[System.Serializable]
public class MyClass {
    public int a, b, c;
    public string[] strArr ;
    public MyClass[] arrClass;
}

[System.Serializable]
public class Article {
    public string title = ""; //
    public string id = ""; //
    public string[] authors = new string[0]; //
    public string venue = ""; // 
    public int year = -1; //
    public int n_citation = 0; //
    public string[] references = new string[0]; // 
    public string Abstract = ""; //
}

[System.Serializable]
public class Articles {
    public Article[] articles;
}