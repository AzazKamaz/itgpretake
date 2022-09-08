using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
[GenerateAuthoringComponent]
public struct PlayerMovementData : IComponentData
{
    public bool movementEnabled;
    public bool secondaryDirection;
}
