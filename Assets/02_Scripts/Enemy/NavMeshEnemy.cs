using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemy : MonoBehaviour
{
    public VisionCone script;
    public Transform player;
    public Transform[] waypoint;
    private NavMeshAgent agent;
    private int index;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoint[0].position);
    }

    void Update()
    {
        if (agent.remainingDistance < 1) {
            agent.SetDestination(waypoint[index].position);
            if (index >= waypoint.Length - 1) index = 0;
            else index++;
        }

        if (script.hunt == true)
        {
            agent.SetDestination(player.position);
            agent.speed = 5;
        }
        else agent.speed = 2;

    }
}