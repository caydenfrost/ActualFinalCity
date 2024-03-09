using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHome : MonoBehaviour
{
    private HouseCharacterData characterData;
    public void ActiveHouse(GameObject house)
    {
        characterData = house.GetComponent<HouseCharacterData>();
    }

    public void LeaveHouse()
    {
        List<GameObject> charactersToRemove = new List<GameObject>(characterData.searchCharacters);
        foreach (GameObject character in charactersToRemove)
        {
            character.gameObject.SetActive(true);
            character.gameObject.GetComponent<NavigationAndAI>().returning = false;
            characterData.searchCharacters.Remove(character);
            characterData.characters.Remove(character);
        }
    }
}
