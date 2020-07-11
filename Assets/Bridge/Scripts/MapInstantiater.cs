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
            var entityPrefabs = new Dictionary<GameObject, Entity>();
            var entityInstances = new Entity[entityInfos.Length];
            for (int i = 0; i < entityInfos.Length; i++)
            {
                var currentInfo = entityInfos[i];
                if (!entityPrefabs.ContainsKey(currentInfo.Entity))
                {
                    var entityPrefab = ConvertGameObjectHierarchy(currentInfo.Entity, settings);
                    entityPrefabs.Add(currentInfo.Entity, entityPrefab);
                }
                var instance = entityManager.Instantiate(entityPrefabs[currentInfo.Entity]);

                entityManager.SetComponentData(instance, new Translation { Value = new float3(currentInfo.Position.x, 0, currentInfo.Position.y) });
                entityManager.SetComponentData(instance, new Rotation { Value = new quaternion(currentInfo.Rotation.x, currentInfo.Rotation.y, currentInfo.Rotation.z, currentInfo.Rotation.w) });

                entityInstances[i] = instance;
            }

            for (int i = 0; i < entityInstances.Length; i++)
            {
                var infos = entityInfos[i].Components;
                var entity = entityInstances[i];

                for (int j = 0; j < infos.Length; j++)
                {
                    var info = infos[j];
                    var converterType = ComponentsTable.GetComponentData(info.Id);
                    entityManager.AddComponentObject(entity, (JsonUtility.FromJson(info.Data, converterType) as ComponentConvertion).Convert(entityInstances));
                }
            }
        }
    }
}
