using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMove : MonoBehaviour
{
    public CharacterHousingManager homeAssign;
    public Wander wander;
    public UIManager uiManager;
    private Vector3 initialMousePosition;
    private Vector3 initialCharacterPosition;
    private bool isDragging = false;
    public float doubleClickTimeThreshold = 0.5f; // Time window for double click detection
    private float lastClickTime = 0f;
    [SerializeField]
    private GameObject home;
    public bool selected;
    private static int _counter;
    public int id;

    void Start()
    {
        _counter++;
        id = _counter;
        homeAssign.UpdateDict(id, null);
        print(id + "logged");
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
                    uiManager.UpdateSelectionUI(id, gameObject);
                    print(id);
                }
/*
                if (!hit.collider.gameObject.CompareTag("Person"))
                {
                    uiManager.UpdateSelectionUI(0);
                }
*/
            }
            if (Physics.Raycast(ray, out hit) && selected)
            {
                if (hit.collider.CompareTag("House"))
                {
                    home = hit.collider.gameObject;
                    print("home" + home);
                    selected = false;
                    homeAssign.UpdateDict(id, home);
                    print(id + "" + home);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("House"))
        {
            if (home != null)
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(home.transform.position);
                GetComponent<UnityEngine.AI.NavMeshAgent>().speed = wander.speed;
            }
        }
    }
}
