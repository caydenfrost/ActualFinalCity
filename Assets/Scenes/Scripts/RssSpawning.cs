using UnityEngine;
using System.Collections.Generic;

public class RssSpawning : MonoBehaviour
{
    public GameObject rssPrefab;
    public int poolSize; // Number of objects to pool
    public Bounds navMeshBounds;
    public int maxNumberOfTrees; // Maximum number of trees allowed
    public List<string> obstacleTags; // List of obstacle tags

    private List<GameObject> rssPool = new List<GameObject>(); // Object pool for RSS objects
    private List<GameObject> treePool = new List<GameObject>(); // Object pool for trees

    void Start()
    {
        // Create object pools
        for (int i = 0; i < poolSize; i++)
        {
            GameObject rss = Instantiate(rssPrefab, Vector3.zero, Quaternion.identity);
            rss.SetActive(false); // Deactivate objects initially
            rssPool.Add(rss);
        }

        InvokeRepeating("SpawnRSS", 0f, 1f); // Adjust the spawn interval as needed
        InvokeRepeating("SpawnTrees", 0f, 0f); // Adjust the spawn interval for trees as needed
    }

    void SpawnRSS()
    {
        // Your existing RSS spawning logic goes here...
    }

    void SpawnTrees()
    {
        // Find all active GameObjects with the "Tree" tag
        GameObject[] activeTrees = GameObject.FindGameObjectsWithTag("Tree");

        // Check if the current number of trees exceeds the maximum allowed
        if (activeTrees.Length >= maxNumberOfTrees)
        {
            return; // Don't spawn more trees if the limit is reached
        }

        // Find an inactive object from the object pool
        GameObject tree = FindInactiveObject(treePool);

        if (tree != null)
        {
            Vector3 randomPosition = GenerateRandomPosition();

            // Check for collision with obstacles
            Collider[] colliders = Physics.OverlapSphere(randomPosition, 1f);
            bool clearArea = true;

            foreach (Collider collider in colliders)
            {
                foreach (string tag in obstacleTags)
                {
                    if (collider.CompareTag(tag))
                    {
                        clearArea = false;
                        break;
                    }
                }
                if (!clearArea)
                {
                    break;
                }
            }

            if (clearArea)
            {
                // Activate the object and set its position
                tree.SetActive(true);
                tree.transform.position = randomPosition;
            }
        }
    }

    GameObject FindInactiveObject(List<GameObject> objectPool)
    {
        // Find the first inactive object in the object pool
        foreach (GameObject obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return null; // Return null if no inactive object is found
    }

    Vector3 GenerateRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        bool clearArea = false;
        int maxAttempts = 10;
        int attempts = 0;

        while (!clearArea && attempts < maxAttempts)
        {
            randomPosition = new Vector3(
                Random.Range(navMeshBounds.min.x, navMeshBounds.max.x),
                Random.Range(navMeshBounds.min.y, navMeshBounds.max.y),
                Random.Range(navMeshBounds.min.z, navMeshBounds.max.z)
            );

            // Check for collision with obstacles
            Collider[] colliders = Physics.OverlapSphere(randomPosition, 1f);

            clearArea = true; // Assume the area is clear until proven otherwise
            foreach (Collider collider in colliders)
            {
                foreach (string tag in obstacleTags)
                {
                    if (collider.CompareTag(tag))
                    {
                        clearArea = false;
                        break;
                    }
                }
                if (!clearArea)
                {
                    break;
                }
            }

            attempts++;
        }

        return clearArea ? randomPosition : Vector3.zero;
    }
}
