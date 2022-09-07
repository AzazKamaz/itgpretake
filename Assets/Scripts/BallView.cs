using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallView : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.TryGetComponent(out BoxTag _)) return;
        CollidedWithBox?.Invoke();
    }

    public void SetHealthText(string health)
    {
        _healthText.text = health;
    }

    public event Action CollidedWithBox;
}