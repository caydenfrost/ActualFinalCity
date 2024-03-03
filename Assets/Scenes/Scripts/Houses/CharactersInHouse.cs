using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersInHouse : MonoBehaviour
{
    public HouseData HouseData;

    // Assuming the character's ID is stored in its instance ID
    private void OnCollisionEnter(Collision other)
    {
        if (HouseData != null)
        {
            int characterID = other.gameObject.GetInstanceID();

            // Check if the character's ID exists in the characterHouses dictionary
            if (HouseData.characterHouses.ContainsKey(characterID))
            {
                GameObject house = HouseData.characterHouses[characterID];
                HouseData.AddCharacter(other.gameObject, house, null);
                print(house.name);
            }
        }
    }
}
