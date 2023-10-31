using Unity.Entities;

public struct BugWalkProperties : IComponentData, IEnableableComponent
{
    public float WalkSpeed;
    public float WalkAmp;
    public float WalkFreq;
}

