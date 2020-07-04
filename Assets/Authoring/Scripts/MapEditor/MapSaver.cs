using Sc2Simulation.Brirge;
using System.Linq;
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
            var currentScene = SceneManager.GetActiveScene();
            var entitiesHolderGameObjects = currentScene.GetRootGameObjects().Where(go => go.GetComponent<EntityAuthoring>() != default).ToArray();
            var infos = new EntityInfo[entitiesHolderGameObjects.Length];
            for (int i = 0; i < infos.Length; i++)
            {
                var newInfo = new EntityInfo();
                var entityHolder = entitiesHolderGameObjects[i];
                var entityAuthoring = entityHolder.GetComponent<EntityAuthoring>();
                newInfo.Entity = PrefabUtility.GetCorrespondingObjectFromOriginalSource(entityAuthoring.Entity);

                var gridOccupier = entityHolder.GetComponent<GridOccupier>();
                if (gridOccupier != default)
                {
                    newInfo.GridPosition = new Vector2Int(gridOccupier.X, gridOccupier.Y);
                    newInfo.GridRotation = gridOccupier.Rotation;
                }

                var entityHolderTransofrm = entityHolder.transform;
                newInfo.Position = new Vector2(entityHolderTransofrm.position.x, entityHolderTransofrm.position.z);
                newInfo.Rotation = new Vector4(entityHolderTransofrm.rotation.x, entityHolderTransofrm.rotation.y, entityHolderTransofrm.rotation.z, entityHolderTransofrm.rotation.w);

                infos[i] = newInfo;
            }
            _map.EntityInfos = infos;
            EditorUtility.SetDirty(_map);
        }
    }
}
