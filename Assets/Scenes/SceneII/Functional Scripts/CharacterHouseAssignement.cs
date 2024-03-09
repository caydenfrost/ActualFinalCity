using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHouseAssignement : MonoBehaviour
{
    public float doubleClickTimeThreshold = 1f; // Time window for double click detection
    [SerializeField] private float lastClickTime = 0f;
    public bool selected;
    public GameObject home;
    public ReturnHome returnHomeScript;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    if ((Time.time - lastClickTime) < doubleClickTimeThreshold)
                    {
                        selected = true;
                    }
                    lastClickTime = Time.time;
                }
                if (hit.collider.CompareTag("House") && selected)
                {
                    home = hit.collider.gameObject;
                    selected = false;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            selected = false;
        }
        if (selected)
        {
            returnHomeScript.AddSelectedObj(gameObject);
        }
        else
        {
            returnHomeScript.RemoveSelectedObj(gameObject);
        }
    }
}
