using Unity.Entities;


[assembly: RegisterGenericComponentType(typeof(EntityTag<BugWalkProperties>))]

public struct EntityTag<T> : IComponentData where T : struct
{ 
}