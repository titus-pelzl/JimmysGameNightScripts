using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerPointNClick : MonoBehaviour
{
    [HideInInspector]
    public Camera cam;
    [HideInInspector]
    public NavMeshAgent agent;



    private void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();

    }
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}