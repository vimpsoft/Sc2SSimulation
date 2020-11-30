using Sc2Simulation.Runtime.Mining;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Sc2Simulation.Runtime.Moving
{/*
  * Ок, для начала я сделаю такую логику: если 
  */
    public class MoveSystem : SystemBase
    {
        private EntityQuery _query;
        private MiningEndBarrier _miningEndBarrier;

        protected override void OnCreate()
        {
            base.OnCreate();

            _query = GetEntityQuery
            (
                new EntityQueryDesc() { All = new ComponentType[] { ComponentType.ReadOnly<MineCommand>() } }
            );
            _miningEndBarrier = World.GetOrCreateSystem<MiningEndBarrier>();
        }

        private struct EmptyMinerJob : IJobChunk
        {
            public ArchetypeChunkComponentType<MineCommand> MineCommandType;
            public ArchetypeChunkComponentType<Translation> TranslationType;
            public EntityCommandBuffer.Concurrent Ecb;
            public ArchetypeChunkEntityType EntityType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                //var mineCommands = chunk.GetNativeArray(MineCommandType);
                //var mainBuildings = chunk.GetNativeArray(MainBuildingType);
                //var entities = chunk.GetNativeArray(EntityType);

                //if (chunk.Has(HasPathToMainBuildingType))
                //{
                //    var translations = chunk.GetNativeArray(TranslationType);

                //    for (int i = 0; i < chunk.Count; i++)
                //    {
                //        var nearestMainBuilding = mainBuildings[]
                //            var distance = math.distance();
                //    }
                //}
                //for (var i = 0; i < chunk.Count; i++)
                //{
                //    if (mainBuildings.Length == 0)
                //        Ecb.RemoveComponent<MineCommand>(i, entities[i]);
                //    else
                //    {

                //    }
                //    var rotation = chunkRotations[i];
                //    var rotationSpeed = chunkRotationSpeeds[i];

                //    // Rotate something about its up vector at the speed given by RotationSpeed_IJobChunk.
                //    chunkRotations[i] = new Rotation
                //    {
                //        Value = math.mul(math.normalize(rotation.Value),
                //            quaternion.AxisAngle(math.up(), rotationSpeed.RadiansPerSecond * DeltaTime))
                //    };
                //}
            }
        }

        protected override void OnUpdate()
        {
            //var ecb = _miningEndBarrier.CreateCommandBuffer().ToConcurrent();
            //var entityType = GetArchetypeChunkEntityType();
            //var job = new EmptyMinerJob()
            //{
            //    MineCommandType = GetArchetypeChunkComponentType<MineCommand>(),
            //    TranslationType = GetArchetypeChunkComponentType<Translation>(),
            //    Ecb = ecb,
            //    EntityType = entityType
            //};

            //Dependency = job.Schedule(_query, Dependency);
        }
    }
}
