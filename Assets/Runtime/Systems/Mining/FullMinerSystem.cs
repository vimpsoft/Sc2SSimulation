using Sc2Simulation.Runtime.Buildings;
using System.Linq;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sc2Simulation.Runtime.Mining
{
    /// <summary>
    /// Система отвечает за поведение полного майнера. Логика такая:
    /// Если у нас есть минералы и задание добывать:
    ///     Есть хаты?
    ///         Да. Ищем ближайшую. Мы в пределах досягаемости этой хаты?
    ///             Да. Сдаем минералы в хату. Дальше рабочего подхватывает EmptyMinerSystem
    ///             Нет. Путь до ближайшей хаты рассчитан?
    ///                 Да. Идем к хате (ждем пока мув-система дойдет до нее)
    ///                 Нет. Ставим эту хату целью для системы поиска пути
    ///         Нет. Снимаем задание добывать - некуда нести.
    /// </summary>
    //[UpdateBefore(typeof(MiningEndBarrier))]
    //public class FullMinerSystem : SystemBase
    //{
    //    private EntityQuery _query;
    //    private MiningEndBarrier _miningEndBarrier;

    //    protected override void OnCreate()
    //    {
    //        base.OnCreate();

    //        _query = GetEntityQuery
    //        (
    //            new EntityQueryDesc() { All = new ComponentType[] { ComponentType.ReadOnly<MineCommand>(), ComponentType.ReadOnly<Minerals>() } },
    //            new EntityQueryDesc() { All = new ComponentType[] { ComponentType.ReadOnly<MainBuilding>() } }
    //        );
    //        _miningEndBarrier = World.GetOrCreateSystem<MiningEndBarrier>();
    //    }

    //    private struct EmptyMinerJob : IJobChunk
    //    {
    //        public ArchetypeChunkComponentType<MineCommand> MineCommandType;
    //        public ArchetypeChunkComponentType<Translation> TranslationType;
    //        public ArchetypeChunkComponentType<HasPathToMainBuilding> HasPathToMainBuildingType;
    //        public ArchetypeChunkComponentType<MainBuilding> MainBuildingType;
    //        public EntityCommandBuffer.Concurrent Ecb;
    //        public ArchetypeChunkEntityType EntityType;

    //        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
    //        {
    //            var mineCommands = chunk.GetNativeArray(MineCommandType);
    //            var mainBuildings = chunk.GetNativeArray(MainBuildingType);
    //            var entities = chunk.GetNativeArray(EntityType);

    //            if (chunk.Has(HasPathToMainBuildingType))
    //            {
    //                var translations = chunk.GetNativeArray(TranslationType);

    //                for (int i = 0; i < chunk.Count; i++)
    //                {
    //                    var nearestMainBuilding = mainBuildings[]
    //                    var distance = math.distance();
    //                }
    //            }
    //            for (var i = 0; i < chunk.Count; i++)
    //            {
    //                if (mainBuildings.Length == 0)
    //                    Ecb.RemoveComponent<MineCommand>(i, entities[i]);
    //                else
    //                {

    //                }
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
    //        var ecb = _miningEndBarrier.CreateCommandBuffer().ToConcurrent();
    //        var entityType = GetArchetypeChunkEntityType();
    //        var job = new EmptyMinerJob()
    //        {
    //            MineCommandType = GetArchetypeChunkComponentType<MineCommand>(),
    //            HasPathToMainBuildingType = GetArchetypeChunkComponentType<HasPathToMainBuilding>(),
    //            TranslationType = GetArchetypeChunkComponentType<Translation>(),
    //            MainBuildingType = GetArchetypeChunkComponentType<MainBuilding>(),
    //            Ecb = ecb,
    //            EntityType = entityType
    //        };

    //        Dependency = job.Schedule(_query, Dependency);
    //    }
    //}
}
