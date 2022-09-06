using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator _an;
    private CharacterController _cc;
    private Vector3 _actualVel;
    private Vector3 _actualVelVel;
    private Vector3 _desiredVel;

    public float speed = 4;
    public float velSmoothTime = 0.075f;

    private void Awake()
    {
        _an = GetComponent<Animator>();
        _cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var dir = GetDesiredDirection();
        _desiredVel = dir * speed;

        _actualVel = Vector3.SmoothDamp(_actualVel, _desiredVel, ref _actualVelVel, velSmoothTime);

        _an.SetFloat("VelRight", Vector3.Dot(_actualVel, transform.right));
        _an.SetFloat("VelForward", Vector3.Dot(_actualVel, transform.forward));
    }

    private void FixedUpdate()
    {
        _cc.Move(_actualVel * Time.fixedDeltaTime);
    }

    private Vector3 GetDesiredDirection()
    {
        // GetAxis returns already smoothed value but it's kinda slow so here we will make our own
        var z = Input.GetAxisRaw("Vertical");
        z = z < 0 ? z * 0.6f : z;
        var x = Input.GetAxisRaw("Horizontal") * 0.8f;
        return Quaternion.LookRotation(transform.forward, transform.up) * new Vector3(x, 0, z);
    }
}