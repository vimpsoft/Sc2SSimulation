using Sc2Simulation.Runtime.Buildings;
using System;
using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public class MineralDruseConversion : ComponentConvertion
    {
        public override object Convert(Entity[] orderedEntities) => new MineralDruse();
    }
}
