using Unity.Entities;
using Unity.Transforms;

public readonly partial struct TomatoAspect : IAspect
{
    public readonly Entity Entity;

    private readonly RefRW<LocalTransform> _transform;
    private readonly RefRW<EntityHealth> _health;
    private readonly DynamicBuffer<TomatoDamageBufferElement> _damageBuffer;

    public void DamageTomato()
    {
        foreach (var element in _damageBuffer)
        {
            _health.ValueRW.Value -= element.Value;
        }

        _damageBuffer.Clear();

        var health = _health.ValueRO;
        _transform.ValueRW.Scale = health.Value / health.Max;
    }

}