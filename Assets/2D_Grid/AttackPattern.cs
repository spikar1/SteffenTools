using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteffenTools.NodeSystem {
    [CreateAssetMenu()]
    public class AttackPattern : ScriptableObject
    {
        public int sizeX = 2, sizeY = 2;
        private bool[][] pattern;
        public bool[][] Pattern {
            get {
                if (pattern == null || pattern.Length < 1)
                {
                    pattern = new bool[sizeX][];
                    for (int i = 0; i < sizeX; i++)
                    {
                        pattern[i] = new bool[sizeY];
                    }
                }
                return pattern;
            }
            set {
                pattern = value;
            }
        }
    }
}