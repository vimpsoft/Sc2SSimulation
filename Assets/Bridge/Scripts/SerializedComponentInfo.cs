using System;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public struct SerializedComponentInfo
    {
        public string ConverterTypeId;
        public string SerializedData;

        public SerializedComponentInfo(string converterTypeId, string serializedData)
        {
            ConverterTypeId = converterTypeId;
            SerializedData = serializedData;
        }
    }
}
