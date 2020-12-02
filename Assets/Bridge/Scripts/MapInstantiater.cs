using System.Collections.Generic;
using Sc2Simulation.Runtime.Mining;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Sc2Simulation.Brirge
{
    public class MapInstantiater : MonoBehaviour
    {
        [SerializeField]
        private Map _map;

        private void Start()
        {
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
            settings.ConversionFlags = GameObjectConversionUtility.ConversionFlags.AssignName;
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var entityInfos = _map.EntityInfos;
            var entityPrefabs = new Dictionary<GameObject, Entity>();
            var entityInstances = new Entity[entityInfos.Length];
            for (int i = 0; i < entityInfos.Length; i++)
            {
                var currentInfo = entityInfos[i];
                if (!entityPrefabs.ContainsKey(currentInfo.Entity))
                {
                    var entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(currentInfo.Entity, settings);
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
                    /*
                     * Итак, у нас есть имя типа конвертера, энтитя, список энтитей, ворлд... что еще нужно? Да вроде бы все...
                     * Нам тут при инстанцировании важно не знать что мы инстанцируем, поэтому всю олологику придется пхать в один класс
                     */
                    var info = infos[j];
                    //var converterType = ComponentsTable.GetComponentData(info.ConverterTypeId);
                    //var componentObject =
                    //    (JsonUtility.FromJson(info.SerializedData, converterType) as ComponentConvertion)
                    //    .Convert(entityInstances);
                    //var type = componentObject.GetType();
                    //var mine = new MineCommand(){ TargetDruse = entity };
                    //entityManager.AddComponentData<MineCommand>(entity, mine);
                    World.DefaultGameObjectInjectionWorld.AddComponentData(info.ConverterTypeId, entity, entityInstances, info.SerializedData);
                    //entityManager.AddComponentObject(entity, componentObject);
                    //entityManager.AddComponent()
                }
            }
        }
    }
}
