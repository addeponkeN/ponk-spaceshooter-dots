using Unity.Entities;
using UnityEngine;

public class BugMono : MonoBehaviour
{
    public float RiseRate;
}

public class BugBaker : Baker<BugMono>
{
    public override void Bake(BugMono authoring)
    {
        var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
        AddComponent(entity, new BugRiseRate { Rate = authoring.RiseRate });
    }
}