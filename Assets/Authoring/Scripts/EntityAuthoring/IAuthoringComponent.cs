using Sc2Simulation.Brirge;
using UnityEngine;

namespace Sc2Simulation.Authoring
{
    /// <summary>
    /// Если мы хотим сделать какой-либо свой кастомный компонент (который не трансформация и не рендеринг),
    /// над надо наследоваться от этого класса, чтобы и добавлять его на авторскую энтитю. А также обеспечить
    /// конвертацию в промежуточную версию (ComponentConvertion), из которой потом десериализуется уже 
    /// рантаймовый компонент.
    /// </summary>
    public abstract class AuthoringComponent : MonoBehaviour
    {
        public virtual SerializedComponentInfo ConvertToRuntime(EntityAuthoring[] entitiesAuthoring) => new SerializedComponentInfo(GetType().Name.Substring(0, GetType().Name.Length - "Authoring".Length) + "Conversion", JsonUtility.ToJson(this));
    }
}
