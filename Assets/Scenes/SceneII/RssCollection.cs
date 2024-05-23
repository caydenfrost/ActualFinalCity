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
    private GameObject currentResource;
    
    public void CollectResource(GameObject resource)
    {
        resource.gameObject.SetActive(false);
        
        AddToInventory(ResourceType.Wood, 1); //CHANGE THIS TO BE IF CURRENT RSS.TAG IS TREE AND ADD THE SAME FOR ROCK
        
        Destroy(currentResource);

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