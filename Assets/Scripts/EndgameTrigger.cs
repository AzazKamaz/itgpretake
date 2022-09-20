using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndgameTrigger : MonoBehaviour
{
    private LevelController _level;
    
    public TextMeshPro text;
    public float multiplier;

    private void Awake()
    {
        _level = FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        text.text = $"{multiplier:0.0}x";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        _level.SetMultiplier(multiplier);
        text.color = Color.green;
    }
}
