using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnBugSystem : ISystem
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
        var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        new SpawnBugJob
        {
            DeltaTime = deltaTime,
            ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
        }.Schedule();
    }
}

[BurstCompile]
public partial struct SpawnBugJob : IJobEntity
{
    public float DeltaTime;
    public EntityCommandBuffer ecb;
    private void Execute(LevelAspect level)
    {
        level.BugSpawnTimer -= DeltaTime;

        if (!level.TimeToSpawnBug) 
            return;

        if (!level.BugSpawnPointInited()) 
            return;

        level.BugSpawnTimer = level.BugSpawnRate;
        var bug = ecb.Instantiate(level.BugPrefab);

        var tfBug = level.GetBugSpawnPoint();
        ecb.SetComponent(bug, tfBug);

        var bugFacing = MathHelper.LookAt(tfBug.Position, level.Position);
        ecb.SetComponent(bug, new EntityFacing { Value = bugFacing });
    }
}