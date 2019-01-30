    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SteffenTools.Extensions;

public class SteffensTestScript : MonoBehaviour {
    public Node[] nodes;
    
    void Update() {

        if (Input.GetKeyDown(KeyCode.A)) {
            foreach (var n in nodes) {
                n.RotateCCW();
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            foreach (var n in nodes) {
                n.RotateCW();
            }
        }

        nodes.Visualize();
    }
}


