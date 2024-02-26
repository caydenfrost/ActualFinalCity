using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    public int maxResidentsPerHouse = 5; // Maximum allowed characters per house
    private Dictionary<GameObject, List<CharacterStats>> characterHouses; // Dictionary to store character stats per house

    void Start()
    {
        characterHouses = new Dictionary<GameObject, List<CharacterStats>>();
    }

    public void UpdateDict(int characterID, GameObject house)
    {
        if (characterID > 0 && house != null)
        {
            // Check if house already has characters
            if (!characterHouses.ContainsKey(house))
            {
                characterHouses.Add(house, new List<CharacterStats>());
            }

            // Check if house is full
            if (characterHouses[house].Count < maxResidentsPerHouse)
            {
                // Add character stats to house dictionary
                characterHouses[house].Add(new CharacterStats(characterID));
            }
            else
            {
                Debug.LogWarning("House is full!");
            }
        }
        else
        {
            Debug.LogWarning("Invalid character or house data.");
        }
    }

    public void RemoveCharacter(GameObject character, GameObject house)
    {
        if (characterHouses.ContainsKey(house))
        {
            // Find character in house list and remove
            for (int i = 0; i < characterHouses[house].Count; i++)
            {
                if (characterHouses[house][i].characterID == character.GetComponent<CharacterMove>().id)
                {
                    characterHouses[house].RemoveAt(i);
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("House not found in dictionary.");
        }
    }

    public List<CharacterStats> GetCharacterStats(GameObject house)
    {
        if (characterHouses.ContainsKey(house))
        {
            return characterHouses[house];
        }
        else
        {
            return null;
        }
    }
}

public class CharacterStats
{
    public int characterID;
    // Add other character statistics here (e.g., name, health, job, etc.)

    public CharacterStats(int id)
    {
        characterID = id;
        // Initialize other character stats here
    }
}
