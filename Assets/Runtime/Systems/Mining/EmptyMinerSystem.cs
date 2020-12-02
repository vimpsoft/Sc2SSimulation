using Sc2Simulation.Runtime.Mining;
using Unity.Collections;
using Unity.Entities;

namespace Sc2Simulation.Runtime.Mining
{
    /// <summary>
    /// Система отвечает за поведение пустого майнера. Логика такая:
    /// </summary>
    [AlwaysUpdateSystem] //чтобы подхватить пустого рабочего сразу же после отдачи минералов
    public class EmptyMinerSystem : SystemBase
    {
        private MiningEndBarrier _miningEndBarrier;
        private EntityQuery _query;

        protected override void OnCreate()
        {
            _query = GetEntityQuery(new EntityQueryDesc()
            {
                None = new ComponentType[] { ComponentType.ReadOnly<Minerals>(), ComponentType.ReadOnly<Assets.Runtime.Components.Moving.Moving>() },
                All = new ComponentType[] { ComponentType.ReadOnly<MineCommand>() }
            });

            _miningEndBarrier = World.GetOrCreateSystem<MiningEndBarrier>();
        }

        private struct EmptyMinerJob : IJobChunk
        {
            public ComponentTypeHandle<MineCommand> MineCommandType;
            [ReadOnly] public EntityTypeHandle EntityTypeHandle;
            public EntityCommandBuffer.ParallelWriter Ecb;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                //var mineCommands = chunk.GetNativeArray(MineCommandType);
                var entities = chunk.GetNativeArray(EntityTypeHandle);

                for (var i = 0; i < chunk.Count; i++)
                {
                    Ecb.AddComponent<Assets.Runtime.Components.Moving.Moving>(i, entities[i]);

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
            var ecb = _miningEndBarrier.CreateCommandBuffer().AsParallelWriter();
            var job = new EmptyMinerJob()
            {
                MineCommandType = GetComponentTypeHandle<MineCommand>(),
                EntityTypeHandle = GetEntityTypeHandle(),
                Ecb = ecb
            };

            Dependency = job.Schedule(_query, Dependency);
            _miningEndBarrier.AddJobHandleForProducer(Dependency);
        }
    }
}
