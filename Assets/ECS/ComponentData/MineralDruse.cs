using Unity.Entities;

[GenerateAuthoringComponent]
public struct MineralDruse : IComponentData
{
    public float Amount;
    public float MinedPortion;
}
