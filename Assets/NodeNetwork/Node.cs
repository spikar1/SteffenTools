using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeNetwork
{
    [System.Serializable]
    public class Node 
    {
        public List<Node> neighbours = new List<Node>();
        public int distance;
        public Vector3 pos;
        public float size;

        public Node(Node parent, Vector3 pos, float size = 1)
        {
            if(parent != null)
                neighbours.Add(parent);
            this.pos = pos;
            this.size = size;
        }
    }
}