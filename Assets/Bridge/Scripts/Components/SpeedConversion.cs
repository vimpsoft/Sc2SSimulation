using System;
using Sc2Simulation.Runtime.Moving;
using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public class SpeedConversion : ComponentConvertion
    {
        public float Value;

        public override object Convert(Entity[] orderedEntities) => new Speed()
            { Value = Value };
    }
}
