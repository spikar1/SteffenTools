using UnityEngine;
using System;

namespace SteffenTools.FloatObject
{
    [Serializable]
    [CreateAssetMenu(fileName = "new Float Object", menuName = "MyTools/Float Object")]
    public class FloatObject : ScriptableObject
    {
        [SerializeField]
        private float myFloat = 2;

        public static implicit operator float(FloatObject FO)
        {
            if (FO == null)
            {
                Debug.LogError("The FloatObject you are referencing does not exist");
                return 0f;
            }
            return FO.myFloat;
        }
    } 
}