using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingLockAndLocate : MonoBehaviour
{
    public float coordinateLock;
    void Start()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Round(currentPosition.x / coordinateLock) * coordinateLock;
        currentPosition.z = Mathf.Round(currentPosition.z / coordinateLock) * coordinateLock;
        currentPosition.y = gameObject.transform.localScale.y/2;
        transform.position = currentPosition;
    }
}
