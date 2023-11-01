using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateAfter(typeof(BugWalkSystem))]
public partial struct BugAttackSystem : ISystem
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
    public void OnUpdate(ref SystemState state)
    {
        var dt = SystemAPI.Time.DeltaTime;
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        //  for now only target the tomato
        var target = SystemAPI.GetSingletonEntity<EntityTag<TomatoMono>>();

        new BugAttackJob
        {
            DeltaTime = dt,
            TargetEntity = target,
            Ecb = ecb.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct BugAttackJob : IJobEntity
{
    public float DeltaTime;
    public Entity TargetEntity;
    public EntityCommandBuffer.ParallelWriter Ecb;

    [BurstCompile]
    private void Execute(BugAttackAspect bug, [EntityIndexInQuery] int sortKey)
    {
        bug.Attack(DeltaTime, Ecb, sortKey, TargetEntity);
    }
}