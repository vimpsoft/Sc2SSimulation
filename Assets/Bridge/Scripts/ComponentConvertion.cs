using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    public abstract class ComponentConvertion
    {
        public abstract object Convert(Entity[] orderedEntities);
    }
}