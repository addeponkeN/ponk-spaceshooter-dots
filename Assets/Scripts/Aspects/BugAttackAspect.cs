using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct BugAttackAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<LocalTransform> _transform;
    private readonly RefRW<EntityTimer> _timer;
    private readonly RefRO<BugAttackProperties> _attackProperties;
    private readonly RefRO<EntityFacing> _facing;

    private float Damage => _attackProperties.ValueRO.Damage;
    private float AttackAmp => _attackProperties.ValueRO.Amp;
    private float AttackFreq => _attackProperties.ValueRO.Freq;
    private float Facing => _facing.ValueRO.Value;

    private float AttackTimer
    {
        get => _timer.ValueRO.Value;
        set => _timer.ValueRW.Value = value;
    }

    public void Attack(float dt, EntityCommandBuffer.ParallelWriter ecb, int sortKey, Entity target)
    {
        AttackTimer += dt;
        var angle = AttackAmp * math.sin(AttackFreq * AttackTimer);
        _transform.ValueRW.Rotation = quaternion.Euler(angle, Facing, 0);

        var dmg = Damage * dt;
        var dmgBuffer = new TomatoDamageBufferElement { Value = dmg };
        ecb.AppendToBuffer(sortKey, target, dmgBuffer);
    }

}