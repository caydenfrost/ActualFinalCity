using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRockRotation : MonoBehaviour
{
    void Start()
    {
        float scaleChange = Random.Range(0.2f, 2.1f);
        gameObject.transform.rotation = Random.rotation;
        gameObject.transform.localScale = new Vector3(scaleChange, scaleChange, scaleChange);
    }
}