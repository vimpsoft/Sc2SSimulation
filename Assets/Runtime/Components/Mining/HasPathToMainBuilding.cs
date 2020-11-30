using Unity.Entities;

namespace Sc2Simulation.Runtime.Mining
{
    public struct HasPathToMainBuilding : IComponentData
    {
        public Entity MainBuilding;
    }
}
