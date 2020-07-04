using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Entities.GameObjectConversionUtility;

namespace Sc2Simulation.Brirge
{
    public class MapInstantiater : MonoBehaviour
    {
        [SerializeField]
        private Map _map;

        private void Start()
        {
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            settings.ConversionFlags = ConversionFlags.AssignName;
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var entityInfos = _map.EntityInfos;
            var convertedPrefabs = new Dictionary<GameObject, Entity>();
            for (int i = 0; i < entityInfos.Length; i++)
            {
                var currentInfo = entityInfos[i];
                if (!convertedPrefabs.ContainsKey(currentInfo.Entity))
                {
                    var entityPrefab = ConvertGameObjectHierarchy(currentInfo.Entity, settings);
                    convertedPrefabs.Add(currentInfo.Entity, entityPrefab);
                }
                var instance = entityManager.Instantiate(convertedPrefabs[currentInfo.Entity]);

                entityManager.SetComponentData(instance, new Translation { Value = new float3(currentInfo.Position.x, 0, currentInfo.Position.y) });
                entityManager.SetComponentData(instance, new Rotation { Value = new quaternion(currentInfo.Rotation.x, currentInfo.Rotation.y, currentInfo.Rotation.z, currentInfo.Rotation.w) });
            }
        }
    }
}
