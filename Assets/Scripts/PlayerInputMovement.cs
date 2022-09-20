using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputMovement : MonoBehaviour
{
    private PlayerInput _input;

    public float range = 2f;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        UpdatePosition();
    }

    private void FixedUpdate()
    {
        if (_input.Active)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        transform.localPosition = Vector3.right * (_input.Value * range);
    }
}
