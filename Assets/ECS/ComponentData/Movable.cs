using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct Movable : IComponentData
{
    public float MaxSpeed;
    public float Acceleration;

    public float CurrentSpeed;
    public float3 CurrentHeading;
}
