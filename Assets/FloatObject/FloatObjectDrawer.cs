using UnityEngine;
using UnityEditor;

namespace SteffenTools.FloatObject
{
    [CustomPropertyDrawer(typeof(FloatObject))]
    public class FloatObjectDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //I've been told this is necessary

            EditorGUI.BeginProperty(position, label, property);
            {
                //Makes prefix labels not appear elseweyr
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                //get the indent level - makes arrays work properly
                var indent = EditorGUI.indentLevel;
                var indentWidth = indent * 16;

                EditorGUI.indentLevel = 0;

                //Define property rects (pos/scale)
                var valueRect = new Rect(position.x, position.y, 30 + indentWidth, position.height);
                var objectRect = new Rect(position.x + 35 + indentWidth, position.y, position.width - 35 - indentWidth, position.height);

                //If the is a FloatObject attached - show value contained
                if (property.objectReferenceValue != null && property.objectReferenceValue.GetType() == typeof(FloatObject))
                {
                    //Get a reference to the FloatObject
                    var floatSO = new SerializedObject(property.objectReferenceValue);
                    EditorGUI.BeginChangeCheck();
                    EditorGUI.PropertyField(valueRect, floatSO.FindProperty("myFloat"), GUIContent.none);
                    floatSO.ApplyModifiedProperties();
                }
                else //If there is no FloatObject, display "Not A Number"
                {
                    EditorGUI.LabelField(valueRect, "nan");
                }
                EditorGUI.PropertyField(objectRect, property, GUIContent.none);

                EditorGUI.indentLevel = indent;
            }
            EditorGUI.EndProperty();
        }
    }
}