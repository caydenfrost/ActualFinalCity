using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterDragging : MonoBehaviour
{
    private Vector3 initialMousePosition;
    private Vector3 initialCharacterPosition;
    private bool isDragging = false;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // Check if clicked object is the character
                {
                    initialMousePosition = Input.mousePosition;
                    initialCharacterPosition = transform.position;
                    isDragging = true;
                }
            }
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;

            // Convert mouse position to world space
            Ray ray = Camera.main.ScreenPointToRay(currentMousePosition);
            Plane plane = new Plane(Vector3.up, initialCharacterPosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 targetPosition = ray.GetPoint(distance);
                transform.position = targetPosition;
            }
        }
        else if (!isDragging)
        {
            // If not dragging, teleport to the closest position on NavMesh
            if (navMeshAgent != null && navMeshAgent.isOnNavMesh)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                    // Zero out the Rigidbody's velocity
                    if (rb != null)
                        rb.velocity = Vector3.zero;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
