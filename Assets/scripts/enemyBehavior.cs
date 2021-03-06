using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyBehavior : MonoBehaviour
{
    public Transform objective;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = objective.position;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = objective.position;
    }
}
