using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class CameraFollowSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.WithoutBurst().ForEach((ref Translation translation, in CameraFollowData follow) =>
        {
            var ft = EntityManager.GetComponentData<Translation>(follow.Following);
            translation.Value.xz = ft.Value.xz;
        }).Run();
    }
}
