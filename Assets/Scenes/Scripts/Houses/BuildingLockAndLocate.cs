using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLockAndLocate : MonoBehaviour
{
    public float coordinateLock;
    public HouseData HouseData;
    void Start()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Round(currentPosition.x / coordinateLock) * coordinateLock;
        currentPosition.z = Mathf.Round(currentPosition.z / coordinateLock) * coordinateLock;
        currentPosition.y = 0.5f;
        transform.position = currentPosition;
        HouseData.AddHouse(gameObject, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
