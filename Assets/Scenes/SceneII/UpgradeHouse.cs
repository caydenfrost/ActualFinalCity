using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHouse : MonoBehaviour
{
    public GameObject prefab;
    private int upgradestage;
    public List<GameObject> stages;
    private GameObject houseupgrade;
    void Start()
    {
        if (prefab.name == "Basic House")
        {
            upgradestage = 0;
        }
    }
    void Update()
    {
        houseupgrade = stages[upgradestage];
    }
    void UgradeHouse()
    {
        upgradestage += 1;
        Instantiate(houseupgrade);
        Destroy(gameObject);
    }
}
