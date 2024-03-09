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
    [SerializeField] private List<GameObject> characters;
    [SerializeField] private List<GameObject> searchCharacters;
    [SerializeField] private TMP_InputField searchBar;
    [SerializeField] private TMP_Text characterListObj;
    [SerializeField] private UnityEngine.UI.Button leaveHouseButton;
    public DetailsPanel DetailsPanelUpdater;
    public bool amSelected = false;
    void Start()
    {
        // ACCESSES THE OBJECTS REFERENCED FOR THE PREFAB
        GameObject leaveHome = GameObject.Find("LeaveHomeButton");
        if (leaveHome != null)
        {
            leaveHouseButton = leaveHome.GetComponent<UnityEngine.UI.Button>();
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
        searchBar.onValueChanged.AddListener(delegate { Search(); });
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
        string searchValue = searchBar.text;
        searchCharacters.Clear();

        if (string.IsNullOrEmpty(searchValue))
        {
            searchCharacters.AddRange(characters);
        }
        else
        {
            foreach (GameObject character in characters)
            {
                if (character.name.Contains(searchValue))
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
    public void LeaveHouse()
    {
        foreach (GameObject character in searchCharacters)
        {
            character.gameObject.SetActive(true);
        }
    }
}
