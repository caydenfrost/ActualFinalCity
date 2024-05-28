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
    public GameObject currentResource;
    public TreeSizer trees;
    private float timer = 3f;
    private float timeInit;
    public float timeUntilBroken;
    
    void Update()
    {
        if (currentResource != null)
        {
            trees = currentResource.GetComponent<TreeSizer>();
        }
    }
    public void CollectResource(GameObject resource)
    {
        if (currentResource.CompareTag("Tree"))
        {
            timeInit = Time.time;
            timeUntilBroken = timeInit + timer * currentResource.transform.localScale.x * 2 - Time.deltaTime;
            if (Time.deltaTime > timeInit + timer * currentResource.transform.localScale.x * 2)
            {
                currentResource.SetActive(false);
                AddToInventory(ResourceType.Wood,  Mathf.RoundToInt(14 * currentResource.transform.localScale.x)); //CHANGE THIS TO BE IF CURRENT RSS.TAG IS TREE AND ADD THE SAME FOR ROCK
                trees.ResetGameObject();
                isCollecting = false;
                currentResource = null;
            }
        }
        if (currentResource.CompareTag("Rock"))
        {
            timeInit = Time.time;
            timeUntilBroken = timeInit + timer * currentResource.transform.localScale.x * 2 - Time.deltaTime;
            if (Time.deltaTime > timeInit + timer * currentResource.transform.localScale.x * 4)
            {
                AddToInventory(ResourceType.Stone,  Mathf.RoundToInt(4 * currentResource.transform.localScale.x)); //CHANGE THIS TO BE IF CURRENT RSS.TAG IS TREE AND ADD THE SAME FOR ROCK
                currentResource.SetActive(false);
                isCollecting = false;
                currentResource = null;
            }
        }
    }

    private void AddToInventory(ResourceType type, int amount)
    {
        // Add the resource to the character's inventory
        // Implement your own inventory management logic here
        Debug.Log("Collected " + amount + " " + type);
    }
}