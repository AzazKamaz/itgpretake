using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody[] _rbs;
    public float destroyDelay = 0;

    private void Awake()
    {
        _rbs = gameObject.GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        foreach (var i in _rbs)
        {
            i.isKinematic = true;
        }
    }

    public void Kill()
    {
        Destroy(gameObject, destroyDelay);
        foreach (var i in _rbs)
        {
            i.isKinematic = false;
        }
    }
}
