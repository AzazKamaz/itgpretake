using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class DirectionSwitchSystem : SystemBase
{
    protected override void OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Entities.ForEach((ref PlayerMovementData movement) =>
            {
                movement.secondaryDirection = !movement.secondaryDirection;
            }).Schedule();
        }
    }
}