using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAndAICore : MonoBehaviour
{
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        ToggleWander(false);
        agent.SetDestination(destination);
    }

    public void ToggleWander(bool shouldWander)
    {
        if (shouldWander)
        {
            WanderToRandomDestination();
        }
        else
        {
            agent.ResetPath();
        }
    }

    void WanderToRandomDestination()
    {
        
    }
}
