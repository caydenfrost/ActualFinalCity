using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNamer : MonoBehaviour
{
    public List<char> consonants = new List<char>();
    public List<char> vowels = new List<char>();
    public string[] doubleVowelLetters = { "ae", "ea", "ee", "oo", "ou", "oi", "ay", "ow", "ew", "oa", "ue", "eu" };
    public string[] doubleConsonantLetters = { "sh", "ch", "th", "ph", "gh", "wh", "ck", "ng", "qu", "ar", "ur", "kn", "wr", "eo", "igh", "oi" };
    public int nameLength;
    public string myName;

    void Start()
    {
        nameLength = Random.Range(3, 9);
        consonants.AddRange(new char[] { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'y' });
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

        bool startWithConsonant = Random.value < 0.9f;

        // Generate a random name of the desired length
        for (int i = 0; i < length; i++)
        {
            if (startWithConsonant)
            {
                myName += GetNextConsonant();
                startWithConsonant = false;
            }
            else
            {
                myName += GetNextVowel();
                startWithConsonant = true;
            }
        }
        myName = char.ToUpper(myName[0]) + myName.Substring(1);
    }

    char GetNextConsonant()
    {
        // Return a random consonant from the list
        if (Random.value < 0.5f)
        {
            // Check for double consonant
            string doubleConsonant = GetRandomDoubleConsonant();
            if (!string.IsNullOrEmpty(doubleConsonant))
                return doubleConsonant[0];
        }

        return consonants[Random.Range(0, consonants.Count)];
    }

    char GetNextVowel()
    {
        // Return a random vowel from the list
        if (Random.value < 0.5f)
        {
            // Check for double vowel
            string doubleVowel = GetRandomDoubleVowel();
            if (!string.IsNullOrEmpty(doubleVowel))
                return doubleVowel[0];
        }

        return vowels[Random.Range(0, vowels.Count)];
    }

    string GetRandomDoubleConsonant()
    {
        // Return a random double consonant combination
        return doubleConsonantLetters.Length > 0 ? doubleConsonantLetters[Random.Range(0, doubleConsonantLetters.Length)] : "";
    }

    string GetRandomDoubleVowel()
    {
        // Return a random double vowel combination
        return doubleVowelLetters.Length > 0 ? doubleVowelLetters[Random.Range(0, doubleVowelLetters.Length)] : "";
    }
}
