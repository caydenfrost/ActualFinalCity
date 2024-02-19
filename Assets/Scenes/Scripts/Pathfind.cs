using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathfind : MonoBehaviour
{
    private NavMeshAgent agent;
    public CharacterHousingManager dictionary;
    public CharacterController character;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void GoHome(int id, GameObject home)
    {
        agent.SetDestination(home.transform.position);
    }
}
