using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    public UpdateDetails detailPanel;
    private CharacterManager characterManager;
    private CharacterData characterData;

    public GameObject parent1;
    public GameObject parent2;

    private int parent1Strength;
    private int parent1Speed;
    private int parent1Health;
    private int parent1Intelligence;
    private string parent1Religion;
    private string parent1PoliticalView;

    private int parent2Strength;
    private int parent2Speed;
    private int parent2Health;
    private int parent2Intelligence;
    private string parent2Religion;
    private string parent2PoliticalView;

    public int selfSpeed;
    public string selfReligion;
    public int selfIntelligence;
    public int selfStrength;
    public int selfHealth;
    public string selfPoliticalView;

    public float mutationChance;
    public int mutationRange;
    private int age = 0;
    private int selfChildren;

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        if (characterManager != null)
        {
            // Set children to zero
            selfChildren = 0;

            // Get CharacterData components from parent GameObjects
            if (parent1 != null)
            {
                CharacterData parent1Data = parent1.GetComponent<CharacterData>();
                if (parent1Data != null)
                {
                    parent1Strength = parent1Data.strength;
                    parent1Speed = parent1Data.speed;
                    parent1Health = parent1Data.health;
                    parent1Religion = parent1Data.religion;
                    parent1PoliticalView = parent1Data.politicalView; // Retrieve political view
                }
            }

            if (parent2 != null)
            {
                CharacterData parent2Data = parent2.GetComponent<CharacterData>();
                if (parent2Data != null)
                {
                    parent2Strength = parent2Data.strength;
                    parent2Speed = parent2Data.speed;
                    parent2Health = parent2Data.health;
                    parent2Religion = parent2Data.religion;
                    parent2PoliticalView = parent2Data.politicalView; // Retrieve political view
                }
            }

            // Apply inheritance and mutation for speed, intelligence, strength, and health
            InheritAndMutate(ref selfSpeed, parent1Speed, parent2Speed);
            InheritAndMutate(ref selfIntelligence, parent1Intelligence, parent2Intelligence);
            InheritAndMutate(ref selfStrength, parent1Strength, parent2Strength);
            InheritAndMutate(ref selfHealth, parent1Health, parent2Health);

            // Inherit and set selfReligion
            selfReligion = UnityEngine.Random.value < 0.5f ? parent1Religion : parent2Religion;

            // Inherit and set selfPoliticalView
            selfPoliticalView = UnityEngine.Random.value < 0.5f ? parent1PoliticalView : parent2PoliticalView;

            // Add or update character data in CharacterManager dictionary
            AddOrUpdateCharacter();

            // Check references of parent1 and parent2 in the dictionary
            CheckParentReferences();
            // Update private character data for ui
            characterData.children = selfChildren; characterData.strength = selfStrength; characterData.speed = selfSpeed; characterData.health = selfHealth; characterData.intelligence = selfIntelligence; characterData.religion = selfReligion; characterData.age = age; characterData.politicalView = selfPoliticalView;
        }
    }

    private void Update()
    {
        // Check references of parent1 and parent2 in the dictionary
        CheckParentReferences();

        age += Mathf.RoundToInt(Time.deltaTime) / 300 / 92 / 4;
        if (Input.GetMouseButtonDown(0))
        {
            // Perform a raycast to detect objects clicked by the mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (!hit.collider.gameObject == gameObject)
                {
                    detailPanel.UpdateDetailPanel(this.characterData);
                }
            }
        }
    }

    // Method to apply inheritance and mutation
    private void InheritAndMutate(ref int selfAttribute, int parent1Attribute, int parent2Attribute)
    {
        // Calculate min and max range values for self attribute
        int minRange = Mathf.Min(parent1Attribute, parent2Attribute);
        int maxRange = Mathf.Max(parent1Attribute, parent2Attribute);

        // Check if the min and max range values are equal
        if (minRange == maxRange)
        {
            // Apply 2x mutation chance if min and max are equal
            mutationChance *= 2f;
        }

        // Generate a random number within the range of self attribute
        selfAttribute = UnityEngine.Random.Range(minRange, maxRange + 1);

        // Check for mutation
        if (selfAttribute == minRange || selfAttribute == maxRange)
        {
            // Generate a random number to determine if mutation occurs
            float mutationRoll = UnityEngine.Random.value;

            if (mutationRoll <= mutationChance)
            {
                // Apply mutation within the mutation range
                int mutation = UnityEngine.Random.Range(-mutationRange, mutationRange + 1);
                selfAttribute += mutation;

                // Ensure self attribute stays within the parent range
                selfAttribute = Mathf.Clamp(selfAttribute, minRange, maxRange);
            }
        }
    }

    // Method to add or update CharacterData in CharacterManager dictionary
    private void AddOrUpdateCharacter()
    {
        if (characterManager.characterDictionary.ContainsKey(gameObject))
        {
            // Update existing character data
            CharacterData myCharacterData = characterManager.characterDictionary[gameObject];
            myCharacterData.speed = selfSpeed;
            myCharacterData.intelligence = selfIntelligence;
            myCharacterData.strength = selfStrength;
            myCharacterData.health = selfHealth;
            myCharacterData.religion = selfReligion;
            myCharacterData.politicalView = selfPoliticalView; // Update political view
            characterManager.characterDictionary[gameObject] = myCharacterData;
        }
        else
        {
            // Add new character data
            CharacterData newCharacterData = new CharacterData
            {
                name = gameObject.name,
                speed = selfSpeed,
                intelligence = selfIntelligence,
                strength = selfStrength,
                health = selfHealth,
                religion = selfReligion,
                politicalView = selfPoliticalView // Assign political view
            };
            characterManager.AddCharacter(gameObject, newCharacterData);
        }
    }

    // Method to check parent references in CharacterManager dictionary
    private void CheckParentReferences()
    {
        if (characterManager != null)
        {
            int matchingReferences = 0;

            foreach (var entry in characterManager.characterDictionary)
            {
                if (entry.Value.parent1 == gameObject || entry.Value.parent2 == gameObject)
                {
                    matchingReferences++;
                }
            }

            selfChildren = matchingReferences;
        }
    }
}
