using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class AttachXZ : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _setPos;
    
    public AttachTarget target;
    public UnityEvent onDetach;

    private void Awake()
    {
        onDetach ??= new UnityEvent();
        
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _setPos = _rb.position;
        target.onFixedUpdate.AddListener(OnFixedUpdate);
    }

    private void OnDestroy()
    {
        target.onFixedUpdate.RemoveListener(OnFixedUpdate);
    }

    private void OnFixedUpdate()
    {
        // Detach cube if it hits some wall and can't move further
        if (Vector3.ProjectOnPlane(_setPos - _rb.position, Vector3.up).magnitude > 0.1)
        {
            onDetach.Invoke();
            target.onFixedUpdate.RemoveListener(OnFixedUpdate);
            enabled = false;
            return;
        }
        
        // Calculate new desired position of cube to follow the player
        _setPos = target.transform.position;
        _setPos.y = _rb.position.y;
        
        // Actually move snapped cube. I tried to move it by velocity and by physical joints,
        // but then it jumps on very little collisions on corners, so it works awful 
        _rb.MovePosition(_setPos);
        _rb.MoveRotation(target.transform.rotation);
    }
}
