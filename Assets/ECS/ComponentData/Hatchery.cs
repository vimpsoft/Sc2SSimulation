using Unity.Entities;

public struct Hatchery : IComponentData
{
    public int LarvaeSupply;
    public float NaturalLarvaeProductionTimer;
}
