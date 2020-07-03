using Unity.Entities;

[GenerateAuthoringComponent]
public struct Worker : IComponentData
{
    public float MineralsAmount;
    public float GasAmount;
}
