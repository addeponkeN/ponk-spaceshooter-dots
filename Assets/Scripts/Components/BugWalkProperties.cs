using Unity.Entities;

public struct BugWalkProperties : IComponentData, IEnableableComponent
{
    public float Speed;
    public float SwayAmp;
    public float SwayFreq;
}

public struct BugAttackProperties : IComponentData, IEnableableComponent
{
    public float Damage;
    public float Amp;
    public float Freq;
}