using Sc2Simulation.Runtime.Mining;
using System;
using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public class MineralsCarrierConversion : ComponentConvertion
    {
        public float Count;

        public override object Convert(Entity[] orderedEntities) => new MineralsCarrier() { Count = Count };
    }
}
