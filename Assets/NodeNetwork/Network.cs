using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeNetwork
{
    public class Network : MonoBehaviour
    {
        public List<Node> nodes = new List<Node>();

        private void Start()
        {
            var mainNode = new Node(null, Vector3.zero, 1);
            nodes.Add(mainNode);

            var lastNode = mainNode;
            for (int i = 0; i < 10; i++)
            {
                var newNode = new Node(lastNode, lastNode.pos + Vector3.forward * lastNode.size * 2, lastNode.size * .9f);
                nodes.Add(newNode);
                lastNode = newNode;
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var node in nodes)
            {
                Gizmos.DrawSphere(node.pos, node.size);
                foreach (var neighbour in node.neighbours)
                {
                    Gizmos.DrawLine(node.pos, neighbour.pos);
                }
            }
        }
    }
}