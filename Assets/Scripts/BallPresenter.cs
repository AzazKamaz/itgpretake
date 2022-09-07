using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPresenter : MonoBehaviour
{
    private BallView _ballView;
    private BallModel _ballModel;

    public void Construct(BallView ballView, BallModel ballModel)
    {
        _ballView = ballView;
        _ballModel = ballModel;
    }

    private void Awake()
    {
        _ballModel.HealthChanged += OnHealthChanged;
        _ballView.CollidedWithBox += OnCollidedWithBox;

        OnHealthChanged();
    }

    private void OnDestroy()
    {
        _ballModel.HealthChanged -= OnHealthChanged;
        _ballView.CollidedWithBox -= OnCollidedWithBox;
    }

    private void OnCollidedWithBox()
    {
        _ballModel.OnCollidedWithBox();
    }

    private void OnHealthChanged()
    {
        _ballView.SetHealthText(_ballModel.Health.ToString());
    }
}