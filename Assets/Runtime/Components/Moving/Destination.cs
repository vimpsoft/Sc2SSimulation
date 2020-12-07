using Unity.Entities;
using Unity.Mathematics;

namespace Assets.Runtime.Components.Moving
{
    public struct Destination : IComponentData
    {
        public float3 Value;
    }
}
