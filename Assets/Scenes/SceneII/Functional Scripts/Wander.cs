using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public bool isWandering = true;
    public float wanderRadius;
    private NavMeshAgent agent;
    public float speed;
    private CharacterHouseAssignement characterHouseAssignment;
    private NavigationAndAI AINav;

    void Start()
    {
        AINav = GetComponent<NavigationAndAI>();
        agent = GetComponent<NavMeshAgent>();
        if (agent == null )
        {
            Debug.LogError("NavMeshAgent not found");
            return;
        }
        characterHouseAssignment = GetComponent<CharacterHouseAssignement>();
        agent = GetComponent<NavMeshAgent>();

        transform.position = new Vector3(transform.position.x, transform.localScale.y, transform.position.z);
    }

    void Update()
    {
        // WANDER CONDITIONS
        if (characterHouseAssignment.selected == false && AINav.returning == false)
        {
            isWandering = true;
        }
        else
        {
            isWandering = false;
        }


        if (DetectFloor() && isWandering)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
            Vector3 finalPosition = hit.position;

            
            agent.SetDestination(finalPosition);
        }
        agent.speed = speed * Time.deltaTime;


        if (characterHouseAssignment.selected == true)
        {
            isWandering = false;
            agent.SetDestination(transform.position);
        }
    }
    bool DetectFloor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                return true;
            }
        }
        return false;
    }
}

