using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using static UnityEngine.GraphicsBuffer;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct SpawnRockSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<LevelProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;
        var levelEntity = SystemAPI.GetSingletonEntity<LevelProperties>();
        var level = SystemAPI.GetAspect<LevelAspect>(levelEntity);

        var ecb = new EntityCommandBuffer(Allocator.Temp);
        var spawnPointsBuilder = new BlobBuilder(Allocator.Temp);
        ref var spawnPoints = ref spawnPointsBuilder.ConstructRoot<BugSpawnPointsBlob>();

        int rockCount = level.RocksToSpawn;

        var arrayBuilder = spawnPointsBuilder.Allocate(ref spawnPoints.Value, rockCount);

        var rockOffset = new float3(0.0f, -2.0f, 1.0f);
        var levelPosition = level.Position;
        LocalTransform rockTransform;

        for (int i = 0; i < rockCount; i++)
        {
            do
            {
                rockTransform = level.GetRandomTombstoneTransform();
            } while (math.distancesq(rockTransform.Position, levelPosition) < 30.0f);

            var rock = ecb.Instantiate(level.RockPrefab);
            ecb.SetComponent(rock, rockTransform);
            var bugSpawnPoint = rockTransform.Position + rockOffset;
            arrayBuilder[i] = bugSpawnPoint;
        }

        var blobAsset = spawnPointsBuilder.CreateBlobAssetReference<BugSpawnPointsBlob>(Allocator.Persistent);
        ecb.SetComponent(levelEntity, new BugSpawnPoints { Value = blobAsset });
        spawnPointsBuilder.Dispose();

        ecb.Playback(state.EntityManager);
    }
}