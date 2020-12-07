using Sc2Simulation.Runtime.Buildings;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sc2Simulation.Runtime.Mining
{
    /// <summary>
    /// Система отвечает за поведение пустого майнера. Логика такая:
    /// </summary>
    [AlwaysUpdateSystem] //чтобы подхватить пустого рабочего сразу же после отдачи минералов
    public class EmptyMinerSystem : SystemBase
    {
        private MiningEndBarrier _miningEndBarrier;
        private EntityQuery _queryMiners;

        protected override void OnCreate()
        {
            //var queryDescription0 = new EntityQueryDesc
            //{
            //    None = new[] { ComponentType.ReadOnly<Minerals>(), ComponentType.ReadOnly<Assets.Runtime.Components.Moving.Destination>() },
            //    All = new[] { ComponentType.ReadOnly<MineCommand>() }
            //};

            //var queryDescription1 = new EntityQueryDesc
            //{
            //    All = new [] { ComponentType.ReadOnly<MineralDruse>(), ComponentType.ReadOnly<Translation>() }
            //};

            //_query = GetEntityQuery(queryDescription0, queryDescription1);
            _queryMiners = GetEntityQuery(new EntityQueryDesc()
            {
                None = new[] { ComponentType.ReadOnly<Minerals>(), ComponentType.ReadOnly<Assets.Runtime.Components.Moving.Destination>() },
                All = new[] { ComponentType.ReadOnly<MineCommand>() }
            });

            _miningEndBarrier = World.GetOrCreateSystem<MiningEndBarrier>();
        }

        private struct EmptyMinerJob : IJobChunk
        {
            public ComponentTypeHandle<MineCommand> MineCommandType;
            public ComponentTypeHandle<MineralDruse> MineralDruseType;
            [ReadOnly] public EntityTypeHandle EntityTypeHandle;
            public EntityCommandBuffer.ParallelWriter Ecb; 
            [ReadOnly] public ComponentDataFromEntity<Translation> MineralDrusePositions;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var mineCommands = chunk.GetNativeArray(MineCommandType);
                //var mineralDruses = chunk.GetNativeArray(MineralDruseType);
                var entities = chunk.GetNativeArray(EntityTypeHandle);

                for (var i = 0; i < chunk.Count; i++)
                {
                    var druseDestination = new Assets.Runtime.Components.Moving.Destination();
                    druseDestination.Position = MineralDrusePositions[mineCommands[i].TargetDruse].Value;
                    Ecb.AddComponent(i, entities[i], druseDestination);

                    //var rotation = chunkRotations[i];
                    //var rotationSpeed = chunkRotationSpeeds[i];

                    //// Rotate something about its up vector at the speed given by RotationSpeed_IJobChunk.
                    //chunkRotations[i] = new Rotation
                    //{
                    //    Value = math.mul(math.normalize(rotation.Value),
                    //        quaternion.AxisAngle(math.up(), rotationSpeed.RadiansPerSecond * DeltaTime))
                    //};
                }
            }
        }

        protected override void OnUpdate()
        {
            //var cellAlignment = new NativeArray<float3>(boidCount, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            //var allMineralDruses = Entities
            //    .ForEach((int entityInQueryIndex, in MineralDruse mineralDruse, in Translation translation) =>
            //    {
            //        cellAlignment[entityInQueryIndex] = localToWorld.Forward;
            //    })
            //    .ScheduleParallel(Dependency);

            var ecb = _miningEndBarrier.CreateCommandBuffer().AsParallelWriter();
            var job = new EmptyMinerJob()
            {
                MineCommandType = GetComponentTypeHandle<MineCommand>(),
                MineralDruseType = GetComponentTypeHandle<MineralDruse>(),
                EntityTypeHandle = GetEntityTypeHandle(),
                MineralDrusePositions = GetComponentDataFromEntity<Translation>(true),
                Ecb = ecb
            };

            Dependency = job.Schedule(_queryMiners, Dependency);
            _miningEndBarrier.AddJobHandleForProducer(Dependency);
        }
    }
}
