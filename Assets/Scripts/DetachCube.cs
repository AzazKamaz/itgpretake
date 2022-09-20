using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachCube : MonoBehaviour
{
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Detach()
    {
        // Allow it to just fall with constrains to stack with other cubes
        _rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX |
                          RigidbodyConstraints.FreezePositionZ;
    }
}
