using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct EndGameTriggerData : IComponentData
{
    public FixedString128Bytes message;
}
