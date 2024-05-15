using UnityEditor.iOS;
using UnityEngine;

public class ResourceCollector : MonoBehaviour
{
    public enum ResourceType
    {
        Wood,
        Stone,
        Food
    }
    private bool isCollecting = false;
    private ResourceType currentResourceType;
    private int currentResourceAmount;
    private GameObject currentResource; // Reference to the current resource being collected

    private void OnTriggerEnter(Collider other)
    {
        if (isCollecting || !other.CompareTag("Resource"))
            return;
        
        currentResource = other.gameObject; // Store reference to the current resource
        isCollecting = true;

        // Start a timer to collect the resource after 3 seconds
        Invoke("CollectResource", 3f);
    }

    private void OnTriggerExit(Collider other)
    {
        // If the character exits the resource trigger area, cancel collection
        if (isCollecting && other.CompareTag("Resource"))
        {
            isCollecting = false;
            CancelInvoke("CollectResource");
        }
    }

    private void CollectResource()
    {
        // Add the collected resource to the character's inventory
        
        // Destroy the collected resource
        Destroy(currentResource);

        // Reset collection variables
        isCollecting = false;
        currentResource = null;
    }

    private void AddToInventory(ResourceType type, int amount)
    {
        // Add the resource to the character's inventory
        // Implement your own inventory management logic here
        Debug.Log("Collected " + amount + " " + type);
    }
}