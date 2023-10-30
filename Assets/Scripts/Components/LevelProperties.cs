using Unity.Entities;
using Unity.Mathematics;

public struct LevelProperties : IComponentData
{
    public float2 Size;
    public int RockCount;
    public Entity RockPrefab;
    public Entity BugPrefab;
    public float BugSpawnRate;
}

public struct BugSpawnTimer : IComponentData
{
    public float Value;
}
