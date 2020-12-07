using Assets.Runtime.Components.Moving;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sc2Simulation.Runtime.Moving
{
    public class MoveSystem : SystemBase
    {
        private EntityQuery _destinationQuery;
        private MiningEndBarrier _miningEndBarrier;

        protected override void OnCreate()
        {
            base.OnCreate();

            _destinationQuery = GetEntityQuery
            (
                new EntityQueryDesc() { All = new ComponentType[] { ComponentType.ReadOnly<Destination>(), ComponentType.ReadOnly<Speed>() } }
            );
            _miningEndBarrier = World.GetOrCreateSystem<MiningEndBarrier>();
        }

        private struct MoveJob : IJobChunk
        {
            [ReadOnly] public ComponentTypeHandle<Speed> SpeedType;
            [ReadOnly] public ComponentTypeHandle<Destination> DestinationType;
            public ComponentTypeHandle<Translation> TranslationType;
            public float DeltaTime;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                var translations = chunk.GetNativeArray(TranslationType);
                var speeds = chunk.GetNativeArray(SpeedType);
                var destinations = chunk.GetNativeArray(DestinationType);

                for (var i = 0; i < chunk.Count; i++)
                {
                    var direction = math.normalize(destinations[i].Value - translations[i].Value);
                    translations[i] = new Translation()
                        {Value = translations[i].Value + direction * speeds[i].Value * DeltaTime};
                }
            }
        }

        protected override void OnUpdate()
        {
            var job = new MoveJob()
            {
                DeltaTime = Time.DeltaTime,
                TranslationType = GetComponentTypeHandle<Translation>(),
                SpeedType = GetComponentTypeHandle<Speed>(),
                DestinationType = GetComponentTypeHandle<Destination>()
            };

            Dependency = job.Schedule(_destinationQuery, Dependency);
        }
    }
}
