using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[UpdateAfter(typeof(BugRiseSystem))]
public partial struct BugWalkSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState staet)
    {
        var dt = SystemAPI.Time.DeltaTime;
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        var tomatoEntity = SystemAPI.GetSingletonEntity<EntityTag<TomatoMono>>();
        var tomatoScale = SystemAPI.GetComponent<LocalTransform>(tomatoEntity).Scale;
        var tomatoRadius = tomatoScale * 5.0f + 0.5f;

        new BugWalkJob
        {
            DeltaTime = dt,
            TargetHitRadius = tomatoRadius,
            Ecb = ecb.CreateCommandBuffer(staet.WorldUnmanaged).AsParallelWriter(),
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct BugWalkJob : IJobEntity
{
    public float DeltaTime;
    public float TargetHitRadius;
    public EntityCommandBuffer.ParallelWriter Ecb;

    [BurstCompile]
    private void Execute(BugWalkAspect bug, [EntityIndexInQuery] int sortKey)
    {
        bug.Walk(DeltaTime);

        if(bug.IsInRange(float3.zero, TargetHitRadius))
        {
            Ecb.SetComponentEnabled<BugWalkProperties>(sortKey, bug.Entity, false);
            Ecb.SetComponentEnabled<BugAttackProperties>(sortKey, bug.Entity, true);
        }

    }
}