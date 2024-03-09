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
        public int id;
    }

    // Dictionary to hold houses and their characters
    public Dictionary<GameObject, Dictionary<GameObject, CharacterData>> houseDictionary = new Dictionary<GameObject, Dictionary<GameObject, CharacterData>>();

    // Dictionary to hold character houses
    public Dictionary<int, GameObject> characterHouses = new Dictionary<int, GameObject>();

    // Method to add a house and its characters
    public void AddHouse(GameObject house, Dictionary<GameObject, CharacterData> characters)
    {
        houseDictionary.Add(house, characters);
    }

    // Method to add a character to a house
    public void AddCharacter(GameObject house, GameObject character, CharacterData characterData)
    {
        if (houseDictionary.ContainsKey(house))
        {
            houseDictionary[house].Add(character, characterData);
        }
        else
        {
            Dictionary<GameObject, CharacterData> newHouse = new Dictionary<GameObject, CharacterData>();
            newHouse.Add(character, characterData);
            houseDictionary.Add(house, newHouse);
        }
    }

    // Method to remove a character from a house
    public void RemoveCharacter(GameObject house, GameObject character)
    {
        if (houseDictionary.ContainsKey(house))
        {
            houseDictionary[house].Remove(character);
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
