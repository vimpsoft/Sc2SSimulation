using UnityEngine;

namespace Sc2Simulation.Authoring
{
    /// <summary>
    /// Используется во-первых как маркер, что это энтити для авторинга.
    /// И во-вторых указывает какую собственно энтитю он авторит.
    /// </summary>
    public class EntityAuthoring : MonoBehaviour
    {
        public GameObject Entity;
    }
}
