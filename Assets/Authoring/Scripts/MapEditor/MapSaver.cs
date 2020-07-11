using Sc2Simulation.Brirge;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sc2Simulation.Authoring
{
    public class MapSaver : MonoBehaviour
    {
        [SerializeField]
        private Map _map;

        public void SaveMap()
        {
            //var types = typeof(MapSaver).Assembly.GetTypes().Where(t => t.GetCustomAttribute(typeof(ConvertedByAttribute)) != default).ToDictionary(t => t, t => (t.GetCustomAttribute(typeof(ConvertedByAttribute)) as ConvertedByAttribute).Type);

            var currentScene = SceneManager.GetActiveScene();
            var entitiesAuthoring = currentScene.GetRootGameObjects().Where(go => go.GetComponent<EntityAuthoring>() != default).Select(go => go.GetComponent<EntityAuthoring>()).ToArray();
            var infos = new EntityInfo[entitiesAuthoring.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                var entityAuthoring = entitiesAuthoring[i];

                var newInfo = new EntityInfo();
                newInfo.Entity = PrefabUtility.GetCorrespondingObjectFromOriginalSource(entityAuthoring.Entity);

                var gridOccupier = entityAuthoring.GetComponent<GridOccupier>();
                if (gridOccupier != default)
                {
                    newInfo.GridPosition = new Vector2Int(gridOccupier.X, gridOccupier.Y);
                    newInfo.GridRotation = gridOccupier.Rotation;
                }

                var entityHolderTransofrm = entityAuthoring.transform;
                newInfo.Position = new Vector2(entityHolderTransofrm.position.x, entityHolderTransofrm.position.z);
                newInfo.Rotation = new Vector4(entityHolderTransofrm.rotation.x, entityHolderTransofrm.rotation.y, entityHolderTransofrm.rotation.z, entityHolderTransofrm.rotation.w);

                var components = entityAuthoring.GetComponents<AuthoringComponent>();
                newInfo.Components = new SerializedComponentInfo[components.Length];
                for (int j = 0; j < components.Length; j++)
                    newInfo.Components[j] = components[j].ConvertToRuntime(entitiesAuthoring);

                infos[i] = newInfo;
            }

            _map.EntityInfos = infos;
            EditorUtility.SetDirty(_map);
        }
    }
}
