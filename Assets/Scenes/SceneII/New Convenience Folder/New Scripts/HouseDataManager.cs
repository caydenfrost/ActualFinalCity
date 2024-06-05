using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
//ATTACHED TO HOUSE
public class HouseDataManager : MonoBehaviour
{
    public List<GameObject> charactersInHouse;
    public List<GameObject> searchCharacters;
    public PrefabReferencesScript prefabReferences;
    public DetailsPanel detailsPanelUpdater;
    public LeaveHome leaveHouseScript;
    public UserInputManager clicker;
    public bool amSelected = false;

    void Start()
    {
        prefabReferences = GameObject.Find("Prefab GameObject Reference").GetComponent<PrefabReferencesScript>();
        clicker = GameObject.Find("UserInputManager").GetComponent<UserInputManager>();

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
        detailsPanelUpdater = GameObject.Find("DetailsPanelUpdater").GetComponent<DetailsPanel>();
        leaveHouseScript = GameObject.Find("LeaveHomeButtonFunction").GetComponent<LeaveHome>();
        
        if (clicker.ClickObjWTag("House"))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                amSelected = false;
            }
            else
            {
                amSelected = true;
            }

            if (amSelected)
            {
                detailsPanelUpdater.UpdateDetailsPanel(gameObject);
                leaveHouseScript.ActiveHouse(gameObject);
            }
            else
            {
                detailsPanelUpdater.UpdateDetailsPanel(null);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Person") && other.gameObject.GetComponent<CharacterHouseAssignement>().home == gameObject && other.gameObject.GetComponent<NavigationAndAI>().returning == true)
        {
            searchCharacters.Add(other.gameObject);
            charactersInHouse.Add(other.gameObject);
            other.gameObject.SetActive(false);
            GameObject spouse = other.gameObject.GetComponent<Birth>().spouse;
            if (spouse != null && charactersInHouse.Contains(spouse))
            {
                other.gameObject.GetComponent<Birth>().MakeChild();
            }
        }
    }

    private void Search()
    {
        string searchValue = prefabReferences.CharacterSearchBar.GetComponent<TMP_InputField>().text.ToLower();
        searchCharacters.Clear();

        if (string.IsNullOrEmpty(searchValue))
        {
            searchCharacters.AddRange(charactersInHouse);
        }
        else
        {
            foreach (GameObject character in charactersInHouse)
            {
                string characterName = character.name.ToLower();
                if (characterName.Contains(searchValue))
                {
                    searchCharacters.Add(character);
                }
            }
        }

        // Update the UI through the DetailsPanel script
        detailsPanelUpdater.UpdateCharacterListUI(searchCharacters);
    }
}
