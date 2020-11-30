using Unity.Entities;
using Unity.Mathematics;

namespace Sc2Simulation.Runtime.Moving
{
    public struct Waypoint : IComponentData
    {
        public float2 Coordinates;
    }
}
