using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct InitializeBugSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var bug in SystemAPI.Query<BugWalkAspect>().WithAll<EntityTag<BugWalkProperties>>())
        {
            ecb.RemoveComponent<EntityTag<BugWalkProperties>>(bug.Entity);
            ecb.SetComponentEnabled<BugWalkProperties>(bug.Entity, false);
        }
        ecb.Playback(state.EntityManager);
    }
}