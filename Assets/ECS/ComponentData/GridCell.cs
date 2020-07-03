using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct GridCell : IComponentData
{
    public int2 Coodinates;
    public bool IsOccupied;
}
