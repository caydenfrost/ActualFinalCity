using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHousingManager : MonoBehaviour
{
    public Dictionary<int, GameObject> characterHouses = new Dictionary<int, GameObject>();
    void Start()
    {
        characterHouses.Add(0, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDict(int id, GameObject home)
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
