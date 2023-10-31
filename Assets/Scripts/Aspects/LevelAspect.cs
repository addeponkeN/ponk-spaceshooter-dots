using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct LevelAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<LocalTransform> _transform;

    private readonly RefRO<LevelProperties> _properties;
    private readonly RefRW<UtilRandom> _random;
    private readonly RefRW<BugSpawnPoints> _bugSpawnPoints;
    private readonly RefRW<BugSpawnTimer> _bugSpawnTimer;

    public int RocksToSpawn => _properties.ValueRO.RockCount;
    public Entity RockPrefab => _properties.ValueRO.RockPrefab;
    public Entity BugPrefab => _properties.ValueRO.BugPrefab;

    public BlobArray<float3> BugSpawnPoints
    {
        get => _bugSpawnPoints.ValueRO.Value.Value.Value;
        set => _bugSpawnPoints.ValueRW.Value.Value.Value = value;
    }

    public bool BugSpawnPointInited()
    {
        return _bugSpawnPoints.ValueRO.Value.IsCreated && BugSpawnPointCount > 0;
    }

    private int BugSpawnPointCount => _bugSpawnPoints.ValueRO.Value.Value.Value.Length;

    public LocalTransform GetRandomTombstoneTransform()
    {
        return new LocalTransform
        {
            Position = GetRandomPosition(),
            Rotation = quaternion.identity,
            Scale = 1f,
        };
    }

    private float3 GetRandomPosition()
    {
        float3 p;
        p = _random.ValueRW.Value.NextFloat3(LevelMin, LevelMax);
        return p;
    }

    private float3 LevelMin => _transform.ValueRO.Position - Size2;
    private float3 LevelMax => _transform.ValueRO.Position + Size2;

    private float3 Size2 => new()
    {
        x = _properties.ValueRO.Size.x * 0.5f,
        y = 0.0f,
        z = _properties.ValueRO.Size.y * 0.5f
    };

    public float BugSpawnTimer
    {
        get => _bugSpawnTimer.ValueRO.Value;
        set => _bugSpawnTimer.ValueRW.Value = value;
    }

    public float BugSpawnRate => _properties.ValueRO.BugSpawnRate;
    public bool TimeToSpawnBug => BugSpawnTimer <= 0.0f;

    public float3 Position => _transform.ValueRO.Position;

    public LocalTransform GetBugSpawnPoint()
    {
        var position = GetRandomBugSpawnPoint();
        return new LocalTransform
        {
            Position = position,
            Rotation = quaternion.RotateY(MathHelper.LookAt(position, _transform.ValueRO.Position)),
            Scale = 1.0f
        };
    }

    private float3 GetRandomBugSpawnPoint()
    {
        int spawnPointsCount = _bugSpawnPoints.ValueRO.Value.Value.Value.Length;
        var index = _random.ValueRW.Value.NextInt(spawnPointsCount);
        var position = _bugSpawnPoints.ValueRO.Value.Value.Value[index];
        return position;
    }

}
