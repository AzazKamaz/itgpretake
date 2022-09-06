using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var target = collision.gameObject.GetComponentInParent<Target>();
        if (!target) return;
        
        target.Kill();
        Destroy(gameObject, 1);
    }
}
