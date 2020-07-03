using UnityEngine;

namespace Sc2Simulation.Brirge
{
    /// <summary>
    /// Ок, тут у нас должна храниться инфа, описывающая карту - а это в принципе все энтити и их координаты да и все.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(Map) + ".asset", menuName = "Sc2Simulation/" + nameof(Map), order = 0)]
    public class Map : ScriptableObject
    {
        public EntityInfo[] EntityInfos;
    }
}
