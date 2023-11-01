using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

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

        new BugWalkJob
        {
            DeltaTime = dt,
            Ecb = ecb.CreateCommandBuffer(staet.WorldUnmanaged).AsParallelWriter(),
            TargetHitRadius = 30.0f,
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
        }

    }
}