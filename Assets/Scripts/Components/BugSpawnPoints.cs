using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct BugSpawnPoints : IComponentData
{
    public BlobAssetReference<BugSpawnPointsBlob> AssetArray;
}

public struct BugSpawnPointsBlob
{
    public BlobArray<float3> BlobArray;
}
