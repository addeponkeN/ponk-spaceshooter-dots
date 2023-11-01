using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(EntityTag<BugMono>))]

public class BugMono : MonoBehaviour
{
    public float RiseRate;

    public float WalkSpeed;
    public float WalkAmp;
    public float WalkFreq;

    public float AttackDamage;
    public float AttackAmp;
    public float AttackFreq;
}

public class BugBaker : Baker<BugMono>
{
    public override void Bake(BugMono authoring)
    {
        var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
        AddComponent(entity, new BugRiseRate { Rate = authoring.RiseRate });
        AddComponent(entity, new BugWalkProperties
        {
            Speed = authoring.WalkSpeed,
            SwayAmp = authoring.WalkAmp,
            SwayFreq = authoring.WalkFreq,
        });
        AddComponent(entity, new BugAttackProperties
        {
            Damage = authoring.AttackDamage,
            Amp = authoring.AttackAmp,
            Freq = authoring.AttackFreq,
        });
        AddComponent<EntityTimer>(entity);
        AddComponent<EntityFacing>(entity);
        AddComponent<EntityTag<BugMono>>(entity);
    }
}