using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;

public class CharacterUIManager : MonoBehaviour
{
    public ParentInheritance characterStats;
    public GameObject CharacterUI;
    public TMP_Text nameText;
    public TMP_Text ageText;
    public TMP_Text housingStatus;
    public TMP_Text speedText;
    public TMP_Text healthText;
    public TMP_Text strengthText;
    public TMP_Text intelligenceText;
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
                    //collect stats
                    characterStats = hit.collider.gameObject.GetComponent<ParentInheritance>();
                    speedText.text = RoundToNearestTenth(characterStats.GetTraitValue(TraitType.Speed)).ToString();
                    healthText.text = RoundToNearestTenth(characterStats.GetTraitValue(TraitType.Health)).ToString();
                    strengthText.text = RoundToNearestTenth(characterStats.GetTraitValue(TraitType.Strength)).ToString();
                    intelligenceText.text = RoundToNearestTenth(characterStats.GetTraitValue(TraitType.Intelligence)).ToString();
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
                else
                {
                    CharacterUI.SetActive(false);
                }
            }
        }
    }

    public void UpdateAge(int newAge)
    {
        age = newAge;
    }
    float RoundToNearestTenth(float value)
    {
        return Mathf.Round(value * 10f) / 10f;
    }
}
