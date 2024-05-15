using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRockRotation : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.rotation = Random.rotation;
    }
}