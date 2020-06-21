using Unity.Entities;

[GenerateAuthoringComponent]
public struct SupplyProvider : IComponentData
{
    public byte Amount;
}
