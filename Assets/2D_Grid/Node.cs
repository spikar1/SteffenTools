using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;

namespace SteffenTools.NodeSystem
{
    [System.Serializable]
    public class Node
    {
        public int x, y;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public static class NodeExtensions
    {
        public static Node RotateCW(this Node n)
        {
            int x = n.x;
            n.x = n.y;
            n.y = -x;
            return n;
        }

        public static Node RotateCCW(this Node n)
        {
            int y = n.y;
            n.y = n.x;
            n.x = -y;
            return n;
        }

        public static void Visualize(this Node n, Color color, int xOffset = 0, int yOffset = 0)
        {
            DebugDrawers.DrawCircle(new Vector2(n.x + xOffset, n.y + yOffset), .4f, Axis.Z, color, 0);
        }

        public static void Visualize(this Node n)
        {
            Visualize(n, Color.white);
        }

        public static void Visualize(this Node n, Color color, Vector2 offset)
        {
            Visualize(n, color, (int)offset.x, (int)offset.y);
        }

        public static void Visualize(this Node[] nodes)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                var n = nodes[i];
                n.Visualize();
                if (i < nodes.Length - 1)
                    Debug.DrawLine(n.ToVector2(), nodes[i + 1].ToVector2());
            }
        }

        public static Vector2 ToVector2(this Node n)
        {
            return new Vector2(n.x, n.y);
        }
    } 
}