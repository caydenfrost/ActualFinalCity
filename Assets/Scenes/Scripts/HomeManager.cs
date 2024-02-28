using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseData", menuName = "Custom/HouseData", order = 1)]
public class HouseData : ScriptableObject
{
    [System.Serializable]
    public class CharacterData
    {
        public string name;
        public int health;
        public int attack;
        // Add more stats as needed
    }

    // Dictionary to hold houses and their characters
    public Dictionary<string, Dictionary<string, CharacterData>> houseDictionary = new Dictionary<string, Dictionary<string, CharacterData>>();

    // Dictionary to hold character houses
    public Dictionary<int, GameObject> characterHouses = new Dictionary<int, GameObject>();

    // Number of characters
    private int numberOfCharacters;

    private void Awake()
    {
        // Initialize the character houses dictionary
        characterHouses.Add(0, null);
    }

    // Update is called once per frame
    public void Update()
    {
        if (numberOfCharacters < characterHouses.Count)
        {
            numberOfCharacters += 1;
        }
    }

    // Method to add a house and its characters
    public void AddHouse(string houseName, Dictionary<string, CharacterData> characters)
    {
        houseDictionary.Add(houseName, characters);
    }

    // Method to add a character to a house
    public void AddCharacter(string houseName, string characterName, CharacterData character)
    {
        if (houseDictionary.ContainsKey(houseName))
        {
            houseDictionary[houseName].Add(characterName, character);
        }
        else
        {
            Dictionary<string, CharacterData> newHouse = new Dictionary<string, CharacterData>();
            newHouse.Add(characterName, character);
            houseDictionary.Add(houseName, newHouse);
        }
    }

    // Method to remove a character from a house
    public void RemoveCharacter(string houseName, string characterName)
    {
        if (houseDictionary.ContainsKey(houseName))
        {
            houseDictionary[houseName].Remove(characterName);
        }
    }

    // Method to update character house dictionary
    public void UpdateCharacterHouse(int id, GameObject home)
    {
        if (characterHouses.ContainsKey(id))
        {
            characterHouses[id] = home;
        }
        else
        {
            characterHouses.Add(id, home);
        }
    }
}
