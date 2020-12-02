using Sc2Simulation.Brirge;
using System;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    public class MineralsCarrierAuthoring : AuthoringComponent
    {
        public float Count;

        public override SerializedComponentInfo ConvertToRuntime(EntityAuthoring[] entitiesAuthoring) => new SerializedComponentInfo(nameof(MineralsCarrierConversion), JsonUtility.ToJson(new MineralsCarrierConversion() { Count = Count }));
    }
}
