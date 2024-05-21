using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TreeSizer : MonoBehaviour
{
    public float checkpoint = 0;
    public float growTime = 5;
    public float timesincegrow = 0;
    public Vector3 originalScale = new Vector3(0.1f, 0.5f, 0.1f);
    public float growScale;
    public Vector3 currentScale = new Vector3();
    public Vector3 nextSize = new Vector3();
    public int maxGrowths;
    public int totalGrowths;
    void Start()
    {
        currentScale = gameObject.transform.localScale;
        growScale = Random.Range(1, 1.25f);
        maxGrowths = Random.Range(15, 45);
    }

    // Update is called once per frame
    void Update()
    {
        currentScale = gameObject.transform.localScale;
        nextSize = new Vector3(currentScale.x * growScale, currentScale.y * growScale, currentScale.z * growScale);
        timesincegrow += Time.deltaTime;
        if (timesincegrow >= checkpoint + growTime * Random.Range(3, 10) && maxGrowths >= totalGrowths)
        {
            checkpoint = Time.deltaTime;
            gameObject.transform.localScale = nextSize;
            totalGrowths += 1;
            growTime += Random.Range(7, 10);
            timesincegrow = 0;
        }
    }
    void ResetGameObject()
    {
        currentScale = originalScale;
        growScale = Random.Range(1, 1.25f);
        maxGrowths = Random.Range(15, 45);
    }
}
