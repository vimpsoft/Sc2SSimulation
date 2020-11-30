using Sc2Simulation.Runtime.Mining;
using Unity.Entities;

namespace Sc2Simulation.Runtime.Mining
{
    /// <summary>
    /// Система отвечает за поведение пустого майнера. Логика такая:
    /// </summary>
    //[UpdateAfter(typeof(FullMinerSystem))] //чтобы подхватить пустого рабочего сразу же после отдачи минералов
    //public class EmptyMinerSystem : SystemBase
    //{
    //    private EntityQuery _query;

    //    protected override void OnCreate() => GetEntityQuery(new EntityQueryDesc()
    //    {
    //        None = new ComponentType[] { ComponentType.ReadOnly<Minerals>() },
    //        All = new ComponentType[] { ComponentType.ReadOnly<MineCommand>() }
    //    });

    //    private struct EmptyMinerJob : IJobChunk
    //    {
    //        public ArchetypeChunkComponentType<MineCommand> MineCommandType;

    //        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
    //        {
    //            var mineCommands = chunk.GetNativeArray(MineCommandType);
    //            for (var i = 0; i < chunk.Count; i++)
    //            {
    //                var rotation = chunkRotations[i];
    //                var rotationSpeed = chunkRotationSpeeds[i];

    //                // Rotate something about its up vector at the speed given by RotationSpeed_IJobChunk.
    //                chunkRotations[i] = new Rotation
    //                {
    //                    Value = math.mul(math.normalize(rotation.Value),
    //                        quaternion.AxisAngle(math.up(), rotationSpeed.RadiansPerSecond * DeltaTime))
    //                };
    //            }
    //        }
    //    }

    //    protected override void OnUpdate()
    //    {
    //        var job = new EmptyMinerJob()
    //        {
    //            MineCommandType = GetArchetypeChunkComponentType<MineCommand>()
    //        };

    //        Dependency = job.Schedule(_query, Dependency);
    //    }
    //}
}
