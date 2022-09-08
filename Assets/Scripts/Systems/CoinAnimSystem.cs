using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

public partial class CoinAnimSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var time = (float)Time.ElapsedTime;
        Entities.ForEach((ref Translation translation, ref Rotation rotation, in CoinAnimData _) =>
        {
            translation.Value.y = math.sin(time * 1.5f) * 0.5f;
            rotation.Value = quaternion.AxisAngle(new float3(0, 1, 0), time / math.PI * 2);
        }).Schedule();
    }
}
