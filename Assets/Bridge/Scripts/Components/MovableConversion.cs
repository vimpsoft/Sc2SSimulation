using System;
using Sc2Simulation.Runtime.Moving;
using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public class MovableConversion : ComponentConvertion
    {
        public float Acceleration;
        public float MaxSpeed;

        public override object Convert(Entity[] orderedEntities) => new Movable()
            {Acceleration = Acceleration, MaxSpeed = MaxSpeed};
    }
}
