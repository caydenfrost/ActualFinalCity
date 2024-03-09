using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HousePlacer : MonoBehaviour
{
    public bool canPlaceHouse = true;
    public bool zoning = false;
    public GameObject zonePrefab;
    public GameObject housePrefab;
    private GameObject instantiatedZone;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] houses = GameObject.FindGameObjectsWithTag("House");
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        canPlaceHouse = true;

        if (instantiatedZone != null)
        {
            foreach (GameObject house in houses)
            {
                if (house.transform.position.x == instantiatedZone.transform.position.x &&
                    house.transform.position.z == instantiatedZone.transform.position.z)
                {
                    canPlaceHouse = false;
                    break; // No need to check further, as we've found a house at the same position
                }
            }
            foreach (GameObject tree in trees)
            {
                float distance = Vector3.Distance(tree.transform.position, instantiatedZone.transform.position);
                if (distance <= 1f)
                {
                    canPlaceHouse = false;
                    break; // No need to check further, as we've found a house at the same position
                }
            }
        }
        if (zoning && Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(instantiatedZone.transform.root.gameObject);
            instantiatedZone = null;
            zoning = false;
        }
        if (zoning && Input.GetMouseButtonDown(0) && canPlaceHouse)
        {
            Instantiate(housePrefab, instantiatedZone.transform.position, Quaternion.identity);
            Destroy(instantiatedZone.transform.root.gameObject);
            instantiatedZone = null;
            zoning = false;
        }
    }
    public void OnButtonClick()
    {
        if (!zoning)
        {
            instantiatedZone = Instantiate(zonePrefab, Vector3.zero, Quaternion.identity);
            zoning = true;
        }
    }
}
