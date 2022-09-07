using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private BallView _ballView;
    [SerializeField] private int _ballInitialHealth = 10;
    private BallPresenter _ballPresenter;

    private void Start()
    {
        var ballModel = new BallModel(_ballInitialHealth);
        _ballPresenter = new BallPresenter(_ballView, ballModel);
    }

    private void OnDestroy()
    {
        _ballPresenter.Dispose();
    }
}
