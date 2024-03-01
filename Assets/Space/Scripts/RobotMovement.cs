using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotMovement : MonoBehaviour
{
    GameObject m_robot;
    public GameObject m_goal;
    NavMeshAgent m_agent;

    // Start is called before the first frame update
    void Start()
    {
        m_robot = this.gameObject;
        m_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        m_agent.destination = m_goal.transform.position;   
    }
}
