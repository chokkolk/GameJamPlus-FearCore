using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTestNavMesh : MonoBehaviour
{
    public VisionCone script;
    public Transform player;
    public Transform[] waypoint;
    private NavMeshAgent agent;
    private int index = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoint[0].position);
    }

    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            agent.SetDestination(waypoint[index].position);
            if (index >= waypoint.Length - 1) index = 0;
            else index++;
        }

        if (script.hunt == true)
        {
            agent.speed = 14;
        }
        else agent.speed = 10;

    }
}
