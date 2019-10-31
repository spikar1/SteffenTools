using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="newCharacter", menuName = "Create New Character")]
public class ScriptableDatabase : ScriptableObject
{
    public int myInt;
    public List<float> floatList;
}
