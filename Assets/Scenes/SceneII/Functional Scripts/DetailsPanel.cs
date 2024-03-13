using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetailsPanel : MonoBehaviour
{
    public GameObject selectedHouse;
    public GameObject panel;
    public TMP_Text characterListObj; // Reference to the TMP_Text component

    void Start()
    {
        // Get reference to the TMP_Text component
        characterListObj = GameObject.Find("CharacterList").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (selectedHouse == null)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }

    public void UpdateDetailsPanel(GameObject house)
    {
        selectedHouse = house;
        // Update the character list UI through the TMP_Text component
        if (house != null)
        {
            HouseCharacterData characterData = house.GetComponent<HouseCharacterData>();
            if (characterData != null)
            {
                characterListObj.text = GetCharacterListText(characterData.searchCharacters);
            }
        }
        else
        {
            characterListObj.text = "";
        }
    }

    public void UpdateCharacterListUI(List<GameObject> characters)
    {
        characterListObj.text = GetCharacterListText(characters);
    }

    private string GetCharacterListText(List<GameObject> characters)
    {
        string list = "";
        foreach (GameObject character in characters)
        {
            list += character.name + "\n";
        }
        return list;
    }
}