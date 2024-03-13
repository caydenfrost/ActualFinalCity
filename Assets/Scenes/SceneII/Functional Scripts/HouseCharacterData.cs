using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class HouseCharacterData : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> searchCharacters;
    public PrefabReferencesScript prefabReferences;
    public DetailsPanel DetailsPanelUpdater;
    public LeaveHome leaveHouseScript;
    public bool amSelected = false;

    void Start()
    {
        prefabReferences = GameObject.Find("Prefab GameObject Reference").GetComponent<PrefabReferencesScript>();
        // Add listener for the search bar value change
        prefabReferences.CharacterSearchBar.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate 
        {
            if (amSelected)
            {
                Search();
            }
        });
    }

    void Update()
    {
        DetailsPanelUpdater = GameObject.Find("DetailsPanelUpdater").GetComponent<DetailsPanel>();
        leaveHouseScript = GameObject.Find("LeaveHomeButtonFunction").GetComponent<LeaveHome>();
        prefabReferences = GameObject.Find("Prefab GameObject Reference").GetComponent<PrefabReferencesScript>();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject) // Check if clicked object is the character
                {
                    amSelected = true;
                }
                else
                {
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        amSelected = false;
                    }
                }
                if (!hit.collider.CompareTag("House"))
                {
                    DetailsPanelUpdater.UpdateDetailsPanel(null);
                }
            }
        }
        if (amSelected)
        {
            DetailsPanelUpdater.UpdateDetailsPanel(gameObject);
            leaveHouseScript.ActiveHouse(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Person") && other.gameObject.GetComponent<CharacterHouseAssignement>().home == gameObject && other.gameObject.GetComponent<NavigationAndAI>().returning == true)
        {
            searchCharacters.Add(other.gameObject);
            characters.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }

    private void Search()
    {
        string searchValue = prefabReferences.CharacterSearchBar.GetComponent<TMP_InputField>().text.ToLower();
        searchCharacters.Clear();

        if (string.IsNullOrEmpty(searchValue))
        {
            searchCharacters.AddRange(characters);
        }
        else
        {
            foreach (GameObject character in characters)
            {
                string characterName = character.name.ToLower();
                if (characterName.Contains(searchValue))
                {
                    searchCharacters.Add(character);
                }
            }
        }

        // Update the UI through the DetailsPanel script
        DetailsPanelUpdater.UpdateCharacterListUI(searchCharacters);
    }
}
