using Unity.Entities;
using UnityEngine;

public class BugMono : MonoBehaviour
{
    public float RiseRate;

    public float WalkSpeed;
    public float WalkAmp;
    public float WalkFreq;
}

public class BugBaker : Baker<BugMono>
{
    public override void Bake(BugMono authoring)
    {
        var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
        AddComponent(entity, new BugRiseRate { Rate = authoring.RiseRate });
        AddComponent(entity, new BugWalkProperties
        {
            WalkSpeed = authoring.WalkSpeed,
            WalkAmp = authoring.WalkAmp,
            WalkFreq = authoring.WalkFreq,
        });
        AddComponent<EntityTimer>(entity);
        AddComponent<EntityFacing>(entity);
        AddComponent<EntityTag<BugWalkProperties>>(entity);
    }
}