using Unity.Entities;
using UnityEngine;

[assembly: RegisterGenericComponentType(typeof(EntityTag<TomatoMono>))]

public class TomatoMono : MonoBehaviour
{
    public float Health;
}

public class TomatoBaker : Baker<TomatoMono>
{
    public override void Bake(TomatoMono authoring)
    {
        var entity = GetEntity(authoring, TransformUsageFlags.Dynamic);
        AddComponent<EntityTag<TomatoMono>>(entity);
        AddComponent(entity, new EntityHealth { Value = authoring.Health, Max = authoring.Health });
        AddBuffer<TomatoDamageBufferElement>(entity);
    }
}
