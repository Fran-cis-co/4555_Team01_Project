using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private void Start()
    {
        navAgent =  GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            // create a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // check if ray hits the ground
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                // move the agent
                navAgent.SetDestination(hit.point);
            }
        }
    }
}
