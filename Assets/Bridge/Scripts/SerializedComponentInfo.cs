using System;

namespace Sc2Simulation.Brirge
{
    [Serializable]
    public struct SerializedComponentInfo
    {
        public string Id;
        public string Data;

        public SerializedComponentInfo(string id, string data)
        {
            Id = id;
            Data = data;
        }
    }
}
