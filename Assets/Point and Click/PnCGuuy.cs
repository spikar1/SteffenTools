using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnCGuuy : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform waypointTrans;
    public LayerMask layerMask;
    Vector3 waypoint => waypointTrans.position;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoint);
    }
    private void Update() {
        if (Input.GetKeyDown("space"))
            Start();

        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            print("clicked");
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
            if(Physics.Raycast(ray, out hit, 100, layerMask)) {
                waypointTrans.position = hit.point;
                agent.SetDestination(waypoint);
            }
        }
    }
}
