using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!other.TryGetComponent<DetachCube>(out var dc)) return; // Destroy only cubes
        dc.Detach();
        Destroy(other.gameObject);
    }
}
