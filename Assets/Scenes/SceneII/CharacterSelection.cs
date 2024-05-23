using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public InventoryUIUpdater inventoryUI; // Reference to the InventoryUI script

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Person"))
                {
                    // Assuming each character has an Inventory component
                    Inventory characterInventory = hit.collider.GetComponent<Inventory>();

                    if (characterInventory != null)
                    {
                        inventoryUI.SetInventory(characterInventory);
                    }
                }
            }
        }
    }
}