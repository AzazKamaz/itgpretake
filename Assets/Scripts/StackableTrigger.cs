using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableTrigger : MonoBehaviour
{
    private PlayerStack _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerStack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _player.AddCube();
        gameObject.SetActive(false);
    }
}
