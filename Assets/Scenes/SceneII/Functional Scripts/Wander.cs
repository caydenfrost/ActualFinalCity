using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public bool isWandering = true;
    public float wanderRadius = 10f; // Adjust as needed
    public float minWanderInterval = 3f; // Minimum time between wander direction changes
    public float maxWanderInterval = 8f; // Maximum time between wander direction changes
    public float minWanderDistance = 3f; // Minimum distance to wander before changing direction
    private NavMeshAgent agent;
    public float wanderSpeed = 500f; // Adjust as needed (initial wander speed)
    private float currentSpeed; // Actual speed adjusted based on wanderSpeed
    private CharacterHouseAssignement characterHouseAssignment;
    private NavigationAndAI AINav;
    private Vector3 targetPosition;
    private bool isWaiting;

    void Start()
    {
        AINav = GetComponent<NavigationAndAI>();
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent not found");
            return;
        }
        characterHouseAssignment = GetComponent<CharacterHouseAssignement>();
        agent = GetComponent<NavMeshAgent>();

        // Start wandering immediately
        StartCoroutine(WanderRoutine());
    }

    void Update()
    {
        // Handle interruptions to wandering
        if (characterHouseAssignment.selected || AINav.returning)
        {
            isWandering = false;
        }
        else
        {
            isWandering = true;
        }

        // Stop agent when selected
        if (characterHouseAssignment.selected)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        
        // Adjust agent speed based on wanderSpeed
        agent.speed = currentSpeed;
    }

    IEnumerator WanderRoutine()
    {
        while (true)
        {
            if (isWandering && !isWaiting)
            {
                // Wait for a random duration before changing wander direction
                yield return new WaitForSeconds(Random.Range(minWanderInterval, maxWanderInterval));
                isWaiting = false;

                // Generate a random target position within wander radius
                Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
                randomDirection += transform.position;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
                targetPosition = hit.position;

                // Set the destination for the agent
                agent.SetDestination(targetPosition);

                // Adjust speed based on wanderSpeed
                currentSpeed = wanderSpeed;
            }

            // Check if agent reached the target position or needs to change direction
            if (!agent.pathPending && agent.remainingDistance <= minWanderDistance && !isWaiting)
            {
                // Wait before changing wander direction
                isWaiting = true;
                yield return new WaitForSeconds(Random.Range(minWanderInterval, maxWanderInterval));
                isWaiting = false;
            }

            yield return null;
        }
    }

    // Optional: Add more realistic behaviors such as looking around or interacting with environment
}
