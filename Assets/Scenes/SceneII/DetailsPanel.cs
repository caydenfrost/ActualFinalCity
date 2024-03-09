using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetailsPanel : MonoBehaviour
{
    public GameObject selectedHouse;
    public GameObject panel;
    void Start()
    {
        
    }
    void Update()
    {
        if (selectedHouse == null)
        {
            panel.SetActive(false);
        }
        else
        {
            panel.SetActive(true);
        }
    }
    public void UpdateDetailsPanel(GameObject house)
    {
        selectedHouse = house;
    }
}
