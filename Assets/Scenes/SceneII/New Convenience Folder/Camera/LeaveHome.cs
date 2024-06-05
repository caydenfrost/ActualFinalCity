using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveHome : MonoBehaviour
{
    private HouseDataManager characterData;
    public void ActiveHouse(GameObject house)
    {
        characterData = house.GetComponent<HouseDataManager>();
    }

    public void LeaveHouse()
    {
        List<GameObject> charactersToRemove = new List<GameObject>(characterData.searchCharacters);
        foreach (GameObject character in charactersToRemove)
        {
            character.gameObject.SetActive(true);
            character.gameObject.GetComponent<NavigationAndAI>().returning = false;
            characterData.searchCharacters.Remove(character);
            characterData.charactersInHouse.Remove(character);
        }
    }
}
