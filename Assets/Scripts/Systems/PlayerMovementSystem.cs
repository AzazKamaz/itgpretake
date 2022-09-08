using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public partial class PlayerMovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PhysicsVelocity velocity, in PlayerMovementData movement) =>
        {
            velocity.Angular = 0;
            
            if (movement.movementEnabled)
            {
                velocity.Linear.xz = movement.secondaryDirection ? new float2(10, 0) : new float2(0, 10);                
            } else {
                velocity.Linear.xz = 0;
            }
        }).Schedule();
        
        Entities.ForEach((ref Rotation rotation, in PlayerMovementData movement) =>
        {
            rotation.Value = quaternion.identity;
        }).Schedule();
    }
}