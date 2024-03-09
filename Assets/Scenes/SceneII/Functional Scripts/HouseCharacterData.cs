using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class HouseCharacterData : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> searchCharacters;
    [SerializeField] private TMP_InputField searchBar;
    [SerializeField] private TMP_Text characterListObj;
    [SerializeField] private UnityEngine.UI.Button leaveHouseButton;
    public DetailsPanel DetailsPanelUpdater;
    public LeaveHome LeaveHouseScript;
    public bool amSelected = false;
    void Start()
    {
        // ACCESSES THE OBJECTS REFERENCED FOR THE PREFAB
        GameObject leaveHome = GameObject.Find("LeaveHomeButton");
        if (leaveHome != null)
        {
            leaveHouseButton = leaveHome.GetComponent<UnityEngine.UI.Button>();
        }
        GameObject leaveHomeFunction = GameObject.Find("LeaveHomeButtonFunction");
        if (leaveHomeFunction != null)
        {
            LeaveHouseScript = leaveHomeFunction.GetComponent<LeaveHome>();
        }
        GameObject inputFieldObj = GameObject.Find("CharacterSearch");
        if (inputFieldObj != null)
        {
            searchBar = inputFieldObj.GetComponent<TMP_InputField>();
        }
        GameObject textObj = GameObject.Find("CharacterList");
        if (textObj != null)
        {
            characterListObj = textObj.GetComponent<TMP_Text>();
        }
        GameObject panelUpdater = GameObject.Find("DetailsPanelUpdater");
        if (panelUpdater != null)
        {
            DetailsPanelUpdater = panelUpdater.GetComponent<DetailsPanel>();
        }
        searchBar.onValueChanged.AddListener(delegate 
        {
            if (amSelected)
            {
                Search();
            }
        });
    }
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
            LeaveHouseScript.ActiveHouse(gameObject);
            UpdateCharacterList();
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Person") && other.gameObject.GetComponent<CharacterHouseAssignement>().home == gameObject && other.gameObject.GetComponent<NavigationAndAI>().returning == true)
        {
            searchCharacters.Add(other.gameObject);
            characters.Add(other.gameObject);
            other.gameObject.SetActive(false);
            UpdateCharacterList();
        }
    }
    public void Search()
    {
        string searchValue = searchBar.text.ToLower();
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

        UpdateCharacterList();
    }
    private void UpdateCharacterList()
    {
        string list = "";
        foreach (GameObject character in searchCharacters)
        {
            list += character.name + "\n";
        }
        if (amSelected)
        {
            characterListObj.text = list;
        }
    }
}
