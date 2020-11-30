using Unity.Entities;

namespace Sc2Simulation.Runtime.Mining
{
    public struct MineCommand : IComponentData
    {
        public Entity TargetDruse;
    }
}
