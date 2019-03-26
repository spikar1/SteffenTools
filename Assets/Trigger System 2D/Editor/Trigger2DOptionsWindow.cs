using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TriggerSystem2D
{
    public class Trigger2DOptionsWindow : EditorWindow
    {
        string myString = "Hello World";
        bool groupEnabled;
        bool myBool = true;
        public int myInt;
        public TriggerOptions2D triggerOptions2D;

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/My Window")]
    static void Init()
        {
            // Get existing open window or if none, make a new one:
            Trigger2DOptionsWindow window = (Trigger2DOptionsWindow)EditorWindow.GetWindow(typeof(Trigger2DOptionsWindow));
            window.Show();

        }

        void OnGUI()
        {

            //EditorGUILayout.ObjectField(

            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Text Field", myString);
            myInt = triggerOptions2D.myInt;

            Undo.RecordObject(triggerOptions2D, "Changed Trigger2D options");
            //groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
            //myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myInt = EditorGUILayout.IntField("Slider", myInt);
            //EditorGUILayout.EndToggleGroup();

            triggerOptions2D.myInt = myInt;
        }
    }
}