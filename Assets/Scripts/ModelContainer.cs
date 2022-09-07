using System.Collections;
using System.Collections.Generic;
using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using UnityEngine;

public sealed class ModelContainer : DependencyContainerBase
{
    [SerializeField] private int _ballInitialHealth = 10;

    protected override void ComposeDependencies(ICanRegisterContainerBuilder builder)
    {
        builder.RegisterFromMethod(() => new BallModel(_ballInitialHealth));
    }
}