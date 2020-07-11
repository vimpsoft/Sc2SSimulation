using Sc2Simulation.Brirge;
using System;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    public class MineCommandAuthoring : AuthoringComponent
    {
        public EntityAuthoring TargetDruse;

        public override SerializedComponentInfo ConvertToRuntime(EntityAuthoring[] entitiesAuthoring) => new SerializedComponentInfo(nameof(MineCommandConversion), JsonUtility.ToJson(new MineCommandConversion() { TargetDruse = Array.IndexOf(entitiesAuthoring, TargetDruse) }));
    }
}
