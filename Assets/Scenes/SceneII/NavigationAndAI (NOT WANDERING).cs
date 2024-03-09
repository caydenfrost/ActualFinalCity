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
    void Start()
    {
        wander = GetComponent<Wander>();
        houseAccess = GetComponent<CharacterHouseAssignement>();
        agent = GetComponent<NavMeshAgent>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == houseAccess.home)
        {
            returning = false;
        }
    }
    public void ReturnHome()
    {
        returning = true;
        houseAccess.selected = false;
        agent.SetDestination(new Vector3(houseAccess.home.transform.position.x, gameObject.transform.position.y, houseAccess.home.transform.position.z));
    }
}
