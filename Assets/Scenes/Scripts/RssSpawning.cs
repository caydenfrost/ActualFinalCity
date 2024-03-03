using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RssSpawning : MonoBehaviour
{
    public GameObject rssPrefab;
    public int poolSize; // Number of objects to pool
    public Bounds navMeshBounds;
    public int maxNumberOfTrees; // Maximum number of trees allowed
    public List<string> obstacleTags; // List of obstacle tags

    private List<GameObject> treePool = new List<GameObject>(); // Object pool for trees

    void Start()
    {
        // Create object pool
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tree = Instantiate(rssPrefab, Vector3.zero, Quaternion.identity);
            tree.SetActive(false); // Deactivate objects initially
            treePool.Add(tree);
        }

        InvokeRepeating("SpawnRSS", 0f, 0.1f); // Adjust the spawn interval as needed
    }

    void SpawnRSS()
    {
        // Find all active GameObjects with the "Tree" tag
        GameObject[] activeTrees = GameObject.FindGameObjectsWithTag("Tree");

        // Check if the current number of trees exceeds the maximum allowed
        if (activeTrees.Length >= maxNumberOfTrees)
        {
            return; // Don't spawn more trees if the limit is reached
        }

        // Find an inactive object from the object pool
        GameObject tree = FindInactiveObject();

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


    GameObject FindInactiveObject()
    {
        // Find the first inactive object in the object pool
        foreach (GameObject obj in treePool)
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
        // Generate a random position within the navMeshBounds
        return new Vector3(
            Random.Range(navMeshBounds.min.x, navMeshBounds.max.x),
            Random.Range(navMeshBounds.min.y, navMeshBounds.max.y),
            Random.Range(navMeshBounds.min.z, navMeshBounds.max.z)
        );
    }
}