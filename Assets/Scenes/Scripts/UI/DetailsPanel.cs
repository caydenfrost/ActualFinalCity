using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetailsPanel : MonoBehaviour
{
    public CharacterData thisData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDetails(CharacterData panelData)
    {
        thisData = panelData;
    }
}
