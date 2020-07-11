using Unity.Entities;

namespace Sc2Simulation.Runtime
{
    public class MineCommand : IComponentData
    {
        public Entity TargetDruse;
    }
}
