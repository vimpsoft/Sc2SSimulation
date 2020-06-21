using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct GridOccupier : IComponentData
{
    public int2 Position;
    public int2 Size;
}
