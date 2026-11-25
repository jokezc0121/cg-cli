using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimientoEnemigo : MonoBehaviour
{
    public Transform player;

    public Transform[] waypoints;
    public float waypointDistance = 1f;
    public float rangoPlayer = 15f;

    private NavMeshAgent agent;
    private int currentWaypoint = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        // PLAYER CERCA -> perseguir
        if (distancia < rangoPlayer)
        {
            agent.SetDestination(player.position);
        }
        // PLAYER LEJOS -> patrullar
        else
        {
            // Cuando llegue al waypoint actual
            if (!agent.pathPending && agent.remainingDistance <= waypointDistance)
            {
                GoToNextWaypoint();
            }
        }
    }

    void GoToNextWaypoint()
    {
        currentWaypoint++;

        if (currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }

        agent.SetDestination(waypoints[currentWaypoint].position);
    }
}
