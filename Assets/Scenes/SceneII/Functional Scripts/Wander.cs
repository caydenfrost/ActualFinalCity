using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public bool isWandering = true;
    public float wanderRadius = 10f;
    public float minWanderDistance = 5f;
    public float maxWanderDistance = 20f;
    public float wanderSpeed = 3f;
    public float maxAngleChange = 45f;

    private NavMeshAgent agent;
    private Vector3 targetPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false; // Prevents the agent from slowing down as it approaches the target
        agent.speed = wanderSpeed;
        targetPosition = GetRandomPointInNavMesh(transform.position, wanderRadius);
        MoveToTarget();
    }

    void Update()
    {
        if (isWandering)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                targetPosition = GetRandomPointInNavMesh(transform.position, wanderRadius);
                MoveToTarget();
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    Vector3 GetRandomPointInNavMesh(Vector3 origin, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += origin;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas);
        return hit.position;
    }

    void MoveToTarget()
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angleToTarget = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);
        float maxAllowedAngleChange = Mathf.Clamp(angleToTarget, -maxAngleChange, maxAngleChange);
        Quaternion newRotation = Quaternion.AngleAxis(maxAllowedAngleChange, Vector3.up) * transform.rotation;
        Vector3 newDirection = newRotation * Vector3.forward;

        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + newDirection * wanderRadius, out hit, wanderRadius, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
    }
}
