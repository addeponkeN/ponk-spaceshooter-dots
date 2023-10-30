using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateAfter(typeof(SpawnBugSystem))]
public partial struct BugRiseSystem : ISystem
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
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        new BugRiseJob
        {
            DeltaTime = deltaTime,
            Ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct BugRiseJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer.ParallelWriter Ecb;

    private void Execute(BugRiseAspect bug, [EntityIndexInQuery] int sortKey)
    {
        bug.Rise(DeltaTime);
        if (!bug.IsAboveGround) return;

        bug.SetAtGround();
        Ecb.RemoveComponent<BugRiseRate>(sortKey, bug.Entity);
    }

}