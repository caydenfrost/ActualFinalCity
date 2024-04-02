using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class CharacterUIManager : MonoBehaviour
{
    public GameObject CharacterUI;
    public TMP_Text nameText;
    public TMP_Text ageText;
    public TMP_Text housingStatus;
    public int age;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to detect objects clicked by the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Person"))
                {
                    //ui setactive
                    CharacterUI.SetActive(true);
                    //name
                    nameText.text = hit.collider.gameObject.name;
                    //age
                    ageText.text = age.ToString();
                    //home status
                    if (hit.collider.gameObject.GetComponent<CharacterHouseAssignement>().home != null)
                    {
                        housingStatus.text = "Housed";
                    }
                    else
                    {
                        housingStatus.text = "Homeless";
                    }
                }
            }
        }
    }

    public void UpdateAge(int newAge)
    {
        age = newAge;
    }
}
