using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterHitbox : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody[] _ragdollBodies;
    private Collider[] _ragdollColliders;
    public UnityEvent onEndgame;

    private void Awake()
    {
        onEndgame ??= new UnityEvent();
        _anim = GetComponentInChildren<Animator>();
        _ragdollBodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        SetRagdoll(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        
        onEndgame.Invoke();
        SetRagdoll(true);
    }

    private void SetRagdoll(bool enable)
    {
        _anim.enabled = !enable;
        foreach (var i in _ragdollBodies)
        {
            i.isKinematic = !enable ^ (i.gameObject == gameObject);
        }
        foreach (var i in _ragdollColliders)
        {
            i.enabled = enable ^ (i.gameObject == gameObject);
        }
    }
}
