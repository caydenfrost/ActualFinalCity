using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MainUIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text rssText;
    [SerializeField] private TMP_Text homeUI;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text job;
    [SerializeField] private GameObject returnHome;

    public void InitializeUI(Wander wander, NavMeshAgent characterAgent)
    {

    }

    public void UpdateResourcesText(int wood, int stone, int steel, int coins)
    {

    }

    public void HandleZoning(bool zoning)
    {

    }

    public void HandleReturnHome(GameObject playerHouse, NavMeshAgent characterAgent, Wander wander)
    {

    }

    public void UpdateSelectionUI(int characterID, GameObject playerObj)
    {

    }

    public void OnButtonClick(bool zoning, GameObject zonePrefab, ref GameObject instantiatedZone, GameObject housePrefab)
    {

    }
}