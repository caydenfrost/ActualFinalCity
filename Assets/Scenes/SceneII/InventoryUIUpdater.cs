using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIUpdater : MonoBehaviour
{
    public GameObject itemUIPrefab; // Prefab for displaying items
    public Transform itemsContainer; // Container for item UI elements

    private Inventory currentInventory; // Reference to the currently selected inventory

    void Start()
    {
        // Initially, no inventory is selected
    }

    public void SetInventory(Inventory newInventory)
    {
        currentInventory = newInventory;
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (currentInventory == null)
        {
            return;
        }

        // Clear existing items
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        // Add new items
        foreach (var item in currentInventory.items)
        {
            GameObject itemUI = Instantiate(itemUIPrefab, itemsContainer);
            Image itemImage = itemUI.GetComponent<Image>();
            itemImage.sprite = item.icon;

            Text amountText = itemUI.transform.Find("AmountText").GetComponent<Text>();
            if (item.isStackable)
            {
                amountText.text = item.quantity.ToString();
            }
            else
            {
                amountText.text = ""; // No amount text for unstackable items
            }
        }
    }
}