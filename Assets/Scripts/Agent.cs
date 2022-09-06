using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Agent : MonoBehaviour
{
    private NavMeshAgent _ag;
    
    public Transform goal;

    private void Awake()
    {
        _ag = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _ag.SetDestination(goal.position);
    }
}
