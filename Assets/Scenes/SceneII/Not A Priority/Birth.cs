using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birth : MonoBehaviour
{
    public GameObject spouse;
    public GameObject characterPrefab;
    public void MakeChild()
    {
        GameObject child = Instantiate(characterPrefab, Vector3.zero, Quaternion.identity);
        ParentInheritance childtraits = child.GetComponent<ParentInheritance>();
        if (childtraits != null)
        {
            childtraits.parent1 = gameObject;
            childtraits.parent2 = spouse;
        }
    }
}
