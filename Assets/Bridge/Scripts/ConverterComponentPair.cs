using System;

namespace Sc2Simulation.Brirge
{
    public class ConverterComponentPair
    {
        public Type ConverterType;
        public Type ComponentType;

        public ConverterComponentPair(Type converterType, Type componentType)
        {
            ConverterType = converterType;
            ComponentType = componentType;
        }
    }
}
