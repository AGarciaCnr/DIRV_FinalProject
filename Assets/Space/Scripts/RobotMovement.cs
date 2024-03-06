using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotMovement : MonoBehaviour
{

    public GameObject m_astronaut;
    public List<GameObject> m_waypoints;

    private NavMeshAgent m_agent;

    private int m_currentWaypointIndex = 0;
    private bool m_fleeing = false;
    private float flee_time= 2f;

    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Comprobar si el objetivo está cerca
        if (Vector3.Distance(transform.position, m_astronaut.transform.position) < 8f)
        {
            // Huir del objetivo
            Vector3 directionToGoal = transform.position - m_astronaut.transform.position;
            Vector3 fleePosition = transform.position + directionToGoal.normalized * 15f;
            m_agent.destination = fleePosition;

            m_fleeing = true;
            flee_time = 0;
        }
        else if (flee_time > 2f)
        {
            if (m_fleeing)
            {
                // Buscar el punto más cercano
                float closestDistance = Mathf.Infinity;
                GameObject closestWaypoint = null;

                foreach (GameObject waypoint in m_waypoints)
                {
                    float distance = Vector3.Distance(transform.position, waypoint.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestWaypoint = waypoint;

                        m_currentWaypointIndex = m_waypoints.IndexOf(waypoint);
                    }
                }

                m_fleeing = false;
            }

            // Establecer el destino del agente al waypoint actual
            m_agent.destination = m_waypoints[m_currentWaypointIndex].transform.position;

            // Comprobar si el agente ha llegado al waypoint actual
            if (Vector3.Distance(transform.position, m_waypoints[m_currentWaypointIndex].transform.position) < 2f)
            {
                // Pasar al siguiente waypoint
                m_currentWaypointIndex = (m_currentWaypointIndex + 1) % m_waypoints.Count;
            }
        }

        flee_time += Time.deltaTime;
    }
}
