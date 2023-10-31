using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct BugRiseAspect : IAspect
{
    public readonly Entity Entity;

    public readonly RefRW<LocalTransform> _transform;
    public readonly RefRO<BugRiseRate> _riseRate;

    public bool IsAboveGround => _transform.ValueRO.Position.y >= 0.0f;

    public void Rise(float dt)
    {
        _transform.ValueRW.Position += math.up() * _riseRate.ValueRO.Rate * dt;
    }

    public void SetAtGround()
    {
        var pos = _transform.ValueRO.Position;
        pos.y = 0;
        _transform.ValueRW.Position = pos;
    }

}
