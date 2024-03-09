using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public HouseData homeAssign;
    public Wander wander;
    public UIManager uiManager;
    private Vector3 initialMousePosition;
    private Vector3 initialCharacterPosition;
    private bool isDragging = false;
    public float doubleClickTimeThreshold = 0.5f; // Time window for double click detection
    private float lastClickTime = 0f;
    public bool selected;
    private static int _counter;
    public bool deactive = false;
    public GameObject home;

    void Start()
    {
        homeAssign.UpdateCharacterHouse(gameObject.GetInstanceID(), null);
        print(gameObject.GetInstanceID() + " added to dict");
        uiManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        // Handle mouse drag to move character
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
                    if ((Time.time - lastClickTime) < doubleClickTimeThreshold)
                    {
                        selected = true;
                    }
                    lastClickTime = Time.time;
                    uiManager.UpdateSelectionUI(gameObject.GetInstanceID(), gameObject);
                }
            }
            if (Physics.Raycast(ray, out hit) && selected)
            {
                if (hit.collider.CompareTag("House"))
                {
                    home = hit.collider.gameObject;
                    selected = false;
                    homeAssign.UpdateCharacterHouse(gameObject.GetInstanceID(), home);
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

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDragging = false;
            selected = false;
        }

        RaycastHit Hit;
        Ray Ray = Camera.main.ScreenPointToRay(transform.position);
        if (Physics.Raycast(Ray, out Hit))
        {
            if (Hit.collider.gameObject == CompareTag("House"))
            {
                deactive = true;
            }
        }
    }

    // Cancel Selection
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isDragging = false;
            selected = false;
        }
    }
}
