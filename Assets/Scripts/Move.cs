using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController _cc;
    private Vector3 _actualVel;
    private Vector3 _actualVelVel;
    private Vector3 _desiredVel;
    private Quaternion _desiredRot;

    public float speed = 4;
    public float velSmoothTime = 0.075f;
    public float rotSpeed = 360 * 8;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var dir = GetDesiredDirection();
        _desiredVel = dir * speed;

        if (dir.magnitude > 0.1)
            _desiredRot = Quaternion.LookRotation(dir, transform.up);

        _actualVel = Vector3.SmoothDamp(_actualVel, _desiredVel, ref _actualVelVel, velSmoothTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _desiredRot, rotSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _cc.Move(_actualVel * Time.fixedDeltaTime);
    }

    private Vector3 GetDesiredDirection()
    {
        // GetAxis returns already smoothed value but it's kinda slow so here we will make our own
        var z = Input.GetAxisRaw("Vertical");
        var x = Input.GetAxisRaw("Horizontal");
        return new Vector3(x, 0, z);
    }
}