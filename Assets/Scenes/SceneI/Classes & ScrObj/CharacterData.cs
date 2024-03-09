using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterData
{
    public string name;
    public string politicalView;
    public string politicalStatus;
    public int strength;
    public int health;
    public string kingdom;
    public int intelligence;
    public int speed;
    public string village;
    public GameObject parent1;
    public GameObject parent2;
    public List<string> inventory;
    public int children;
    public int kills;
    public int age;
    public string religion;
    public string placeOfOrigin;

    public static List<string> PoliticalViewOptions = new List<string>{
        "Full Democracy", "Representative Democracy", "Monarchy", "Electocracy",
        "Technocracy", "Hereditary Monarchy", "Meritocracy", "Capitalism",
        "Totalitarian", "Anarchy", "Communist", "Kleptocracy", "Stratocracy",
        "Colonialist", "Autocracy", "Theocracy"
    };

    public static List<string> PoliticalStatusOptions = new List<string>{
        "Leader", "Priest", "Nobels", "Land Owner", "Upper Class", "Revolutionary",
        "Middle Class", "Lower Class", "Military"
    };
}