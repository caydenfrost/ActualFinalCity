using UnityEngine;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public Dictionary<GameObject, CharacterData> characterDictionary = new Dictionary<GameObject, CharacterData>();

    public void AddCharacter(GameObject characterObject, CharacterData characterData)
    {
        if (!characterDictionary.ContainsKey(characterObject))
        {
            characterDictionary.Add(characterObject, characterData);
        }
    }

    public void RemoveCharacter(GameObject characterObject)
    {
        if (characterDictionary.ContainsKey(characterObject))
        {
            characterDictionary.Remove(characterObject);
        }
    }
}