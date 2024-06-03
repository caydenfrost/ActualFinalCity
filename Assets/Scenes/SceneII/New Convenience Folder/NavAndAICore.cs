using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAndAICore : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        MovingToDestination,
        Wandering
    }
    private NavMeshAgent agent;
    private AIState currentState;
    public float wanderRadius = 10f; // Radius within which the AI will wander
    public float wanderInterval = 5f; // Time between wandering movements

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = AIState.Idle;
        StartCoroutine(Wander());
    }

    public void MoveTo(Vector3 destination)
    {
        currentState = AIState.MovingToDestination;
        agent.SetDestination(destination);
    }

    public void ToggleWander(bool shouldWander)
    {
        if (shouldWander)
        {
            currentState = AIState.Wandering;
        }
        else
        {
            currentState = AIState.Idle;
            agent.ResetPath();
        }
    }

    private void WanderToRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
        {
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            if (currentState == AIState.Wandering)
            {
                WanderToRandomDestination();
            }
            yield return new WaitForSeconds(wanderInterval);
        }
    }
}