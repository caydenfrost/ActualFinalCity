using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public int quantity;
    public bool isStackable;

    public Item(string name, Sprite icon, int quantity, bool isStackable)
    {
        this.itemName = name;
        this.icon = icon;
        this.quantity = quantity;
        this.isStackable = isStackable;
    }
}

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int maxCapacity = 10;

    public bool AddItem(Item item)
    {
        if (items.Count >= maxCapacity && !item.isStackable)
        {
            Debug.Log("Inventory is full!");
            return false;
        }
        
        if (item.isStackable)
        {
            foreach (Item i in items)
            {
                if (i.itemName == item.itemName)
                {
                    i.quantity += item.quantity;
                    return true;
                }
            }
        }
        
        if (items.Count < maxCapacity)
        {
            items.Add(item);
            return true;
        }
        else
        {
            Debug.Log("Inventory is full!");
            return false;
        }
    }

    public bool RemoveItem(Item item)
    {
        foreach (Item i in items)
        {
            if (i.itemName == item.itemName)
            {
                i.quantity -= item.quantity;
                if (i.quantity <= 0)
                {
                    items.Remove(i);
                }
                return true;
            }
        }

        Debug.Log("Item not found in inventory!");
        return false;
    }
}

