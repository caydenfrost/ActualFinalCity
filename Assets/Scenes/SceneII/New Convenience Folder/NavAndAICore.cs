using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// ATTACHED TO CHARACTER
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
    public float wanderRadius = 10f;
    public float wanderInterval = 5f;
    private Vector3 previousDestination;
    private float maxAngleChange = 45f; // Maximum allowed angle change in degrees

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentState = AIState.Idle;
        previousDestination = transform.position;
        StartCoroutine(Wander());
    }

    void Update()
    {
        if (currentState == AIState.Wandering && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            WanderToRandomDestination();
        }
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
        Vector3 newDestination;
        float angleChange;

        do
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1))
            {
                newDestination = hit.position;
                Vector3 directionToNewDestination = newDestination - transform.position;

                angleChange = Vector3.Angle(previousDestination - transform.position, directionToNewDestination);
            }
            else
            {
                newDestination = transform.position;
                angleChange = 0;
            }
        }
        while (angleChange > maxAngleChange);

        agent.SetDestination(newDestination);
        previousDestination = newDestination;
    }

    private IEnumerator Wander()
    {
        while (true)
        {
            if (currentState == AIState.Wandering)
            {
                if (!agent.pathPending && agent.remainingDistance < 0.5f)
                {
                    WanderToRandomDestination();
                }
            }
            yield return new WaitForSeconds(wanderInterval);
        }
    }
}
