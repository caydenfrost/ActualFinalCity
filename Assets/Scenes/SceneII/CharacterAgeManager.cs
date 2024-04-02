using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAgeManager : MonoBehaviour
{
    public CharacterUIManager update;
    public int age;
    void Start()
    {
        GameObject updater = GameObject.Find("CharacterUIManagerObj");
        update = updater.GetComponent<CharacterUIManager>();
    }
    void Update()
    {
        age = Mathf.FloorToInt(Time.time/300);
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to detect objects clicked by the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    update.UpdateAge(age);
                }
            }
        }
    }
}
