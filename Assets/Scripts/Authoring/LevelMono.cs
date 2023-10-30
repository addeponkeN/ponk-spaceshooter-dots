using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class LevelMono : MonoBehaviour
{
    public float2 Size;
    public int RockCount;
    public GameObject RockPrefab;
    public uint RndSeed;
    public GameObject BugPrefab;
    public float BugSpawnRate;
}

public class LevelBaker : Baker<LevelMono>
{
    public override void Bake(LevelMono authoring)
    {
        var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
        var rockPrefab = GetEntity(authoring.RockPrefab, TransformUsageFlags.Dynamic);
        var bugPrefab = GetEntity(authoring.BugPrefab, TransformUsageFlags.Dynamic);
        AddComponent(entity, new LevelProperties
        {
            Size = authoring.Size,
            RockCount = authoring.RockCount,
            RockPrefab = rockPrefab,
            BugPrefab = bugPrefab,
            BugSpawnRate = authoring.BugSpawnRate,
        });

        AddComponent(entity, new UtilRandom
        {
            Value = Unity.Mathematics.Random.CreateFromIndex(authoring.RndSeed)
        });

        AddComponent<BugSpawnPoints>(entity);
        AddComponent<BugSpawnTimer>(entity);
    }
}