using Unity.Entities;
using Unity.Mathematics;

public struct BugSpawnPoints : IComponentData
{
    public BlobAssetReference<BugSpawnPointsBlob> Value;
}

public struct BugSpawnPointsBlob
{
    public BlobArray<float3> Value;
}
