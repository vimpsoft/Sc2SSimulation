using Unity.Entities;

namespace Sc2Simulation.Runtime.Moving
{
    public struct Movable : IComponentData
    {
        public float Acceleration;
        public float MaxSpeed;
    }
}
