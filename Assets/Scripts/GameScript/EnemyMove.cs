using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public bool chase;
    public bool patrol;

    NavMeshAgent navMeshAgent;

    public GameObject targetObject;

    EnemySearch search;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        search = GetComponent<EnemySearch>();
    }

    void Update()
    {
            Chase(); 
    }

    public void Chase()
    {
        if (search.playerOn)
        {
            navMeshAgent.destination = targetObject.transform.position;
            chase = true;
        }
    }
}
