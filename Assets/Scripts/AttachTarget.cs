using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttachTarget : MonoBehaviour
{
    public UnityEvent onFixedUpdate;
    
    private void Awake()
    {
        onFixedUpdate ??= new UnityEvent();
    }

    private void FixedUpdate()
    {
        onFixedUpdate.Invoke();
    }
}
