using Sc2Simulation.Brirge;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    public abstract class AuthoringComponent : MonoBehaviour
    {
        public virtual SerializedComponentInfo ConvertToRuntime(EntityAuthoring[] entitiesAuthoring) => new SerializedComponentInfo(GetType().Name.Substring(0, GetType().Name.Length - "Authoring".Length) + "Conversion", JsonUtility.ToJson(this));
    }
}
