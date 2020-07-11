using Sc2Simulation.Runtime;
using System;
using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public class MineCommandConversion : ComponentConvertion
    {
        public int TargetDruse;

        public override object Convert(Entity[] orderedEntities) => new MineCommand() { TargetDruse = orderedEntities[TargetDruse] };
    }
}
