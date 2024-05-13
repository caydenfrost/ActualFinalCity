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
    float mutationFactor = 0.1f;
    private Rigidbody rb;
    private Collider characterCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        characterCollider = GetComponent<Collider>();

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

        // Initialize traits including fertility
        traits = new TraitSet();
        traits.InitializeRandom(mutationFactor, generation);

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

        // Update the Rigidbody's scale
        rb.transform.localScale = transform.localScale;
        characterCollider.transform.localScale = transform.localScale;
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
    Width,
    Fertility
}

public interface Trait
{
    Trait Inherit(Trait other);
    object GetValue();
}

public class QuantitativeTrait : Trait
{
    float value;
    float mutationFactor;

    public QuantitativeTrait(float value, float mutationFactor)
    {
        this.value = value;
        this.mutationFactor = mutationFactor;
    }

    public Trait Inherit(Trait other)
    {
        float inheritedValue = (value + ((QuantitativeTrait)other).value) / 2.0f;
        return new QuantitativeTrait(inheritedValue, mutationFactor);
    }

    public Trait Mutate()
    {
        float mutation = Mathf.PerlinNoise(Time.time, 0) * mutationFactor;
        return new QuantitativeTrait(value + mutation, mutationFactor);
    }

    public object GetValue()
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
        float inheritedValue = Random.Range(0f, 1f) < 0.5f ? value : ((QualitativeTrait)other).value;
        return new QualitativeTrait(inheritedValue);
    }

    public object GetValue()
    {
        return value;
    }
}

public class ColorTrait : Trait
{
    Color value;
    float mutationFactor;

    public ColorTrait(Color value, float mutationFactor)
    {
        this.value = value;
        this.mutationFactor = mutationFactor;
    }

    public Trait Inherit(Trait other)
    {
        Color inheritedValue = Random.Range(0f, 1f) < 0.5f ? value : ((ColorTrait)other).value;
        return new ColorTrait(inheritedValue, mutationFactor);
    }

    public Trait Mutate()
    {
        float mutationR = Mathf.PerlinNoise(Time.time, 0) * mutationFactor;
        float mutationG = Mathf.PerlinNoise(Time.time, 1) * mutationFactor;
        float mutationB = Mathf.PerlinNoise(Time.time, 2) * mutationFactor;

        float threshold = 0.1f * mutationFactor; // 10% of mutation factor
        bool randomizeColor = (mutationR < threshold || mutationR > (1 - threshold)) &&
                              (mutationG < threshold || mutationG > (1 - threshold)) &&
                              (mutationB < threshold || mutationB > (1 - threshold));

        Color mutatedColor;
        if (randomizeColor)
        {
            mutatedColor = new Color(Random.value, Random.value, Random.value);
        }
        else
        {
            mutatedColor = new Color(
                Mathf.Clamp01(value.r + mutationR),
                Mathf.Clamp01(value.g + mutationG),
                Mathf.Clamp01(value.b + mutationB)
            );
        }

        return new ColorTrait(mutatedColor, mutationFactor);
    }

    public object GetValue()
    {
        return value;
    }
}

public class TraitSet
{
    Dictionary<TraitType, Trait> traits;

    public void InitializeRandom(float mutationFactor, int generation)
    {
        traits = new Dictionary<TraitType, Trait>();
        foreach (var traitType in TraitTypeExtensions.AllTypes)
        {
            if (traitType == TraitType.Color)
            {
                traits.Add(traitType, new ColorTrait(new Color(Random.value, Random.value, Random.value), mutationFactor)); // Random color
            }
            else if (traitType == TraitType.Height || traitType == TraitType.Width)
            {
                float randomValue = Random.Range(0.05f, 0.15f);
                traits.Add(traitType, new QuantitativeTrait(randomValue, mutationFactor));
            }
            else if (traitType == TraitType.Fertility && generation == 1)
            {
                traits.Add(traitType, new QuantitativeTrait(0.05f, mutationFactor)); // Initial fertility value for generation 1
            }
            else
            {
                traits.Add(traitType, new QuantitativeTrait(Random.Range(0f, 1f), mutationFactor));
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

    public Color GetTraitColor(TraitType traitType)
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
