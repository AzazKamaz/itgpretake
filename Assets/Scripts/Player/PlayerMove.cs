using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth), typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerHealth _hp;
    
    private int _current = 0;

    public Vector3[] velocities = new[] {Vector3.forward * 10, Vector3.right * 10};

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _hp = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _current = (_current + 1) % velocities.Length;
        }
    }

    private void FixedUpdate()
    {
        if (_hp.IsAlive)
            _rb.velocity = new Vector3(
                velocities[_current].x,
                _rb.velocity.y,
                velocities[_current].z
            );
        else
            _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
    }
}