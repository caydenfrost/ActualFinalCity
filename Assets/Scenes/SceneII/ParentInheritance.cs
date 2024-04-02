using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentInheritance : MonoBehaviour
{
    public int generation;
    public int speed;
    public int health;
    public int strength;
    public int intelligence;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (generation == 1)
        {
            speed = Random.RandomRange(1, 5);
            health = Random.RandomRange(5, 20);
            strength = Random.RandomRange(1, 5);
            intelligence = Random.RandomRange(1, 10);
        }
    }
}
