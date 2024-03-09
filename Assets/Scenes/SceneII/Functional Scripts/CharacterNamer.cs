using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNamer : MonoBehaviour
{
    public List<char> consonants = new List<char>();
    public List<char> vowels = new List<char>();
    public int nameLength;
    public string myName;
    void Start()
    {
        nameLength = Random.Range(3, 9);
        consonants.AddRange(new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'y', 'z' });
        vowels.AddRange(new char[] { 'a', 'e', 'i', 'o', 'u', 'y' });
        GenerateRandomName(nameLength);
    }
    void Update()
    {
        gameObject.name = myName;
    }
    void GenerateRandomName(int length)
    {
        // Initialize the name as an empty string
        myName = "";

        // Generate a random name of the desired length
        for (int i = 0; i < length; i++)
        {
            // Alternate between consonants and vowels to create a balanced name
            if (i % 2 == 0)
            {
                myName += GetRandomConsonant();
            }
            else
            {
                myName += GetRandomVowel();
            }
        }
        myName = char.ToUpper(myName[0]) + myName.Substring(1);
    }

    char GetRandomConsonant()
    {
        // Return a random consonant from the list
        return consonants[Random.Range(0, consonants.Count)];
    }

    char GetRandomVowel()
    {
        // Return a random vowel from the list
        return vowels[Random.Range(0, vowels.Count)];
    }
}
