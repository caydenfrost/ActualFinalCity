using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersInHouse : MonoBehaviour
{
    public HouseData HouseData;
    private List<GameObject> charactersInHouse = new List<GameObject>();
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    print(charactersInHouse);
                }
            }
        }
    }
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
                charactersInHouse.Add(other.gameObject);
                other.gameObject.SetActive(false);
            }
        }
        print("Trying to accesss id: " + other.gameObject.GetInstanceID());
    }
}
