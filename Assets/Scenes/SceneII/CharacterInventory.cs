using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void CollectItem(Item item)
    {
        bool success = inventory.AddItem(item);
        if (success)
        {
            Debug.Log("Collected: " + item.itemName);
        }
        else
        {
            Debug.Log("Failed to collect: " + item.itemName);
        }
    }

    public void UseItem(Item item)
    {
        bool success = inventory.RemoveItem(item);
        if (success)
        {
            Debug.Log("Used: " + item.itemName);
        }
        else
        {
            Debug.Log("Failed to use: " + item.itemName);
        }
    }
}

