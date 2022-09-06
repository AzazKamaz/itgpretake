using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameObject[] _goals;
    private float _nextSwap;

    public float goalTimeout;

    private void Awake()
    {
        _goals = GameObject.FindGameObjectsWithTag("Finish");
    }

    private void Start()
    {
        SwapGoal();
    }

    private void Update()
    {
        if (Time.time > _nextSwap)
            SwapGoal();
    }

    private void SwapGoal()
    {
        var id = (int) (Random.value * (_goals.Length - 1));
        var goal = _goals[id];

        transform.position = Vector3.ProjectOnPlane(goal.transform.position, Vector3.up);

        _nextSwap = Time.time + goalTimeout;
    }
}