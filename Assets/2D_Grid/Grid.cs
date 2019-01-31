using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;

namespace SteffenTools.NodeSystem {
    public class Grid : MonoBehaviour
    {
        private int ySize = 6;
        private int xSize = 6;
        Node[,] nodes = new Node[0, 0];

        private void Start()
        {
            nodes = new Node[xSize, ySize];
            for (int y = 0; y < ySize; y++)
            {
                for (int x = 0; x < xSize; x++)
                {
                    nodes[x, y] = new Node(x, y);
                }
            }
        }

        private void Update()
        {
            foreach (var n in nodes)
            {
                n.Visualize();
            }
        }
    }
}