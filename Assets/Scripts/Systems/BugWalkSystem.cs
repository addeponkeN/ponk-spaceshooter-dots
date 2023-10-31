using Unity.Burst;
using Unity.Entities;

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
        new BugWalkJob
        {
            DeltaTime = dt,
        }.ScheduleParallel();
    }
}

[BurstCompile]
public partial struct BugWalkJob : IJobEntity
{
    public float DeltaTime;

    [BurstCompile]
    private void Execute(BugWalkAspect bug)
    {
        bug.Walk(DeltaTime);
    }
}