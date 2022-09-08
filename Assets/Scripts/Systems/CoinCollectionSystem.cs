using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateAfter(typeof(ExportPhysicsWorld))]
[UpdateBefore(typeof(EndFramePhysicsSystem))]
public partial class CoinCollectionSystem : SystemBase
{
    [BurstCompile]
    struct TriggerHandleJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<PlayerScoreData> PlayerScoreDataGroup;
        public ComponentDataFromEntity<CoinData> CoinDataGroup;
        public EntityCommandBuffer CommandBuffer;

        private void Workload(Entity trigger, Entity body)
        {
            if (!PlayerScoreDataGroup.HasComponent(body) || !CoinDataGroup.HasComponent(trigger)) return;

            var score = PlayerScoreDataGroup[body];
            score.score += 1;
            PlayerScoreDataGroup[body] = score;
            
            CommandBuffer.DestroyEntity(trigger);

            Debug.Log($"Coin collected, score is {score.score}");
        }

        public void Execute(TriggerEvent triggerEvent)
        {
            Entity entityA = triggerEvent.EntityA;
            Entity entityB = triggerEvent.EntityB;

            Workload(entityA, entityB);
            Workload(entityB, entityA);
        }
    }

    private StepPhysicsWorld _stepPhysicsWorld;
    private EndSimulationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
        _commandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();
        this.RegisterPhysicsRuntimeSystemReadOnly();
    }

    protected override void OnUpdate()
    {
        Dependency = new TriggerHandleJob
        {
            PlayerScoreDataGroup = GetComponentDataFromEntity<PlayerScoreData>(),
            CoinDataGroup = GetComponentDataFromEntity<CoinData>(),
            CommandBuffer = _commandBufferSystem.CreateCommandBuffer(),
        }.Schedule(_stepPhysicsWorld.Simulation, Dependency);

        _commandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}