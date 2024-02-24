using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    private Dictionary<GameObject, TMP_Text> characterStorage;
    public GameObject currentCharacter;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateHousedCharacters(GameObject player, TMP_Text name)
    {
        if (characterStorage.ContainsKey(player))
        {
            characterStorage[player] = name;
        }
        else
        {
            characterStorage.Add(player, name);
        }
    }
    public void RemoveCharacter()
    {
        currentCharacter.SetActive(true);
        if (characterStorage.ContainsKey(currentCharacter.gameObject))
        {
            characterStorage.Remove(currentCharacter.gameObject);
        }
    }
}
