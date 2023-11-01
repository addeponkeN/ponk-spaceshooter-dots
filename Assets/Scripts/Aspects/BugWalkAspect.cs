using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct BugWalkAspect : IAspect
{
    public readonly Entity Entity;

    public readonly RefRW<LocalTransform> _transform;
    public readonly RefRW<EntityTimer> _walkTimer;
    public readonly RefRO<BugWalkProperties> _walkProperties;
    public readonly RefRO<EntityFacing> _facing;

    private float WalkSpeed => _walkProperties.ValueRO.Speed;
    private float WalkAmp => _walkProperties.ValueRO.SwayAmp;
    private float WalkFreq => _walkProperties.ValueRO.SwayFreq;
    private float Facing => _facing.ValueRO.Value;

    private float WalkTimer
    {
        get => _walkTimer.ValueRO.Value;
        set => _walkTimer.ValueRW.Value = value;
    }

    public void Walk(float dt)
    {
        WalkTimer += dt;
        var walkWave = math.sin(WalkFreq * WalkTimer * WalkSpeed);
        _transform.ValueRW.Position += _transform.ValueRO.Forward() * WalkSpeed * dt;
        var swayAngle = WalkAmp * walkWave;
        _transform.ValueRW.Rotation = quaternion.Euler(swayAngle, Facing, 0.0f);
    }

    public bool IsInRange(float3 target, float range)
    {
        return math.distancesq(target, _transform.ValueRO.Position) <= range;
    }

}