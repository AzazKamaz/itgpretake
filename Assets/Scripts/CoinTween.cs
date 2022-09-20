using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CoinTween : MonoBehaviour
{
    private RectTransform _rect;

    public UnityEvent onComplete;

    private void Awake()
    {
        onComplete ??= new UnityEvent();
        _rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        _rect.DOPivot(new Vector2(1, 1), 1);
        _rect.DOAnchorPos(new Vector2(-20, -20), 1);
        _rect.DOAnchorMin(new Vector2(1, 1), 1);
        _rect.DOAnchorMax(new Vector2(1, 1), 1)
            .OnComplete(TweenComplete);
    }

    private void TweenComplete()
    {
        onComplete.Invoke();
        Destroy(gameObject);
    }
}
