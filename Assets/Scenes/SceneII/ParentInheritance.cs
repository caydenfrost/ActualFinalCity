using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentInheritance : MonoBehaviour
{
    public int generation;
    public TraitSet traits;
    public GameObject parent1;
    public ParentInheritance parent1data;
    public GameObject parent2;
    public ParentInheritance parent2data;
    
    void Start()
    {
        if (parent1 != null)
            parent1data = parent1.GetComponent<ParentInheritance>();

        if (parent2 != null)
            parent2data = parent2.GetComponent<ParentInheritance>();

        // Calculate the generation of the character
        if (parent1data != null && parent2data != null)
        {
            generation = Mathf.Max(parent1data.generation, parent2data.generation) + 1;
        }
        else
        {
            generation = 1; // Assume first generation if parents are not defined
        }

        // Initialize random traits for the first generation
        if (generation == 1)
        {
            traits = new TraitSet();
            traits.InitializeRandom();
        }
        else // Inherit traits from parents for subsequent generations
        {
            traits = InheritTraits(parent1data?.traits, parent2data?.traits);
        }

        // Perform actions based on inherited traits
        // Example: Adjust movement speed based on speed trait
        float movementSpeed = traits.GetTraitValue(TraitType.Speed);
        
        // Example: Change color based on color trait
        Color color = traits.GetTraitColor(TraitType.Color);
        GetComponent<Renderer>().material.color = color;
        
        // Example: Adjust size based on size trait
        float height = Mathf.Clamp(traits.GetTraitValue(TraitType.Height), 0.05f, 0.15f);
        float width = Mathf.Clamp(traits.GetTraitValue(TraitType.Width), 0.05f, 0.15f);
        transform.localScale = new Vector3(width, height, width); // Set both width and depth to width value
    }

    TraitSet InheritTraits(TraitSet traits1, TraitSet traits2)
    {
        TraitSet inheritedTraits = new TraitSet();
        foreach (var traitType in TraitTypeExtensions.AllTypes)
        {
            Trait trait1 = traits1?.GetTrait(traitType);
            Trait trait2 = traits2?.GetTrait(traitType);
            Trait inheritedTrait = trait1?.Inherit(trait2) ?? trait2;
            inheritedTraits.SetTrait(traitType, inheritedTrait);
        }
        return inheritedTraits;
    }

    public float GetTraitValue(TraitType traitType)
    {
        return traits.GetTraitValue(traitType);
    }
}

public enum TraitType
{
    Speed,
    Health,
    Strength,
    Intelligence,
    Color,
    Height,
    Width
}

public interface Trait
{
    Trait Inherit(Trait other);
    object GetValue(); // Changed the return type to object
}

public class QuantitativeTrait : Trait
{
    float value;

    public QuantitativeTrait(float value)
    {
        this.value = value;
    }

    public Trait Inherit(Trait other)
    {
        float inheritedValue = (value + ((QuantitativeTrait)other).value) / 2.0f;
        return new QuantitativeTrait(inheritedValue);
    }

    public object GetValue() // Changed the return type to object
    {
        return value;
    }
}

public class QualitativeTrait : Trait
{
    float value;

    public QualitativeTrait(float value)
    {
        this.value = value;
    }

    public Trait Inherit(Trait other)
    {
        // For qualitative traits, randomly choose one of the parent values
        float inheritedValue = Random.Range(0f, 1f) < 0.5f ? value : ((QualitativeTrait)other).value;
        return new QualitativeTrait(inheritedValue);
    }

    public object GetValue() // Changed the return type to object
    {
        return value;
    }
}

public class ColorTrait : Trait // New class to handle color traits
{
    Color value;

    public ColorTrait(Color value)
    {
        this.value = value;
    }

    public Trait Inherit(Trait other)
    {
        // For color traits, randomly choose one of the parent values
        Color inheritedValue = Random.Range(0f, 1f) < 0.5f ? value : ((ColorTrait)other).value;
        return new ColorTrait(inheritedValue);
    }

    public object GetValue() // Changed the return type to object
    {
        return value;
    }
}

public class TraitSet
{
    Dictionary<TraitType, Trait> traits;

    public void InitializeRandom()
    {
        traits = new Dictionary<TraitType, Trait>();
        foreach (var traitType in TraitTypeExtensions.AllTypes)
        {
            if (traitType == TraitType.Color)
            {
                traits.Add(traitType, new ColorTrait(new Color(Random.value, Random.value, Random.value))); // Random color
            }
            else if (traitType == TraitType.Height || traitType == TraitType.Width)
            {
                // Random height and width within reasonable range
                float randomValue = Random.Range(0.05f, 0.15f);
                traits.Add(traitType, new QuantitativeTrait(randomValue));
            }
            else
            {
                traits.Add(traitType, new QuantitativeTrait(Random.Range(0f, 1f)));
            }
        }
    }

    public Trait GetTrait(TraitType traitType)
    {
        return traits[traitType];
    }

    public void SetTrait(TraitType traitType, Trait trait)
    {
        traits[traitType] = trait;
    }

    public float GetTraitValue(TraitType traitType)
    {
        return (float)traits[traitType].GetValue();
    }

    public Color GetTraitColor(TraitType traitType) // Method to get color trait value
    {
        return (Color)traits[traitType].GetValue();
    }
}

public static class TraitTypeExtensions
{
    public static IEnumerable<TraitType> AllTypes
    {
        get
        {
            return (TraitType[])System.Enum.GetValues(typeof(TraitType));
        }
    }
}
