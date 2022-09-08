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
public partial class EndGameSystem : SystemBase
{
    struct TriggerHandleJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<PlayerMovementData> PlayerMovementDataGroup;
        public ComponentDataFromEntity<EndGameTriggerData> EndGameTriggerDataGroup;

        private void Workload(Entity trigger, Entity body)
        {
            if (!PlayerMovementDataGroup.HasComponent(body) || !EndGameTriggerDataGroup.HasComponent(trigger)) return;

            var move = PlayerMovementDataGroup[body];
            if (move.movementEnabled)
            {
                move.movementEnabled = false;
                PlayerMovementDataGroup[body] = move;

                Debug.Log(EndGameTriggerDataGroup[trigger].message.ToString());
            }
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

    protected override void OnCreate()
    {
        base.OnCreate();
        _stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
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
            PlayerMovementDataGroup = GetComponentDataFromEntity<PlayerMovementData>(),
            EndGameTriggerDataGroup = GetComponentDataFromEntity<EndGameTriggerData>(),
        }.Schedule(_stepPhysicsWorld.Simulation, Dependency);
    }
}