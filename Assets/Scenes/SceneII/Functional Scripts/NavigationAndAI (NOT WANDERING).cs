using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NavigationAndAI : MonoBehaviour
{
    private Wander wander;
    private CharacterHouseAssignement houseAccess;
    private NavMeshAgent agent;
    public bool returning = false;
    public Vector3 rssLocation;
    public GameObject rssObj;
    public ResourceCollector rssColl;
    void Start()
    {
        wander = GetComponent<Wander>();
        houseAccess = GetComponent<CharacterHouseAssignement>();
        agent = GetComponent<NavMeshAgent>();
        rssColl = GetComponent<ResourceCollector>();
    }

    void Update()
    {
        if (houseAccess.selected == true && Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Tree") || hit.collider.gameObject.CompareTag("Rock"))
                    {
                        rssObj = hit.collider.gameObject;
                        rssColl.currentResource = rssObj;
                        rssLocation = rssObj.transform.position;
                        CollectRss();
                    }
                }
            }
        }
        if (rssObj != null)
        {
            if (rssObj.activeInHierarchy == false)
            {
                wander.isWandering = true;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == rssObj)
        {
            rssColl.CollectResource(rssObj);
            agent.SetDestination(transform.position);
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject == rssObj)
        {
            returning = false;
            wander.isWandering = true;
            rssObj = null;
            rssLocation = new Vector3(0, 0, 0);
        }
    }

    public void CollectRss()
    {
        returning = true;
        houseAccess.selected = false;
        agent.SetDestination(rssLocation);
    }
    public void ReturnHome()
    {
        returning = true;
        houseAccess.selected = false;
        agent.SetDestination(new Vector3(houseAccess.home.transform.position.x, gameObject.transform.position.y, houseAccess.home.transform.position.z));
    }
}
