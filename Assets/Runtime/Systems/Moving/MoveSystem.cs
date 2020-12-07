using Assets.Runtime.Components.Moving;
using Unity.Collections;
using Unity.Entities;
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
                new EntityQueryDesc() { All = new ComponentType[] { ComponentType.ReadOnly<Destination>(), ComponentType.ReadOnly<Movable>() } }
            );
            _miningEndBarrier = World.GetOrCreateSystem<MiningEndBarrier>();
        }

        private struct MoveJob : IJobChunk
        {
            [ReadOnly] public ComponentTypeHandle<Translation> TranslationType;
            [ReadOnly] public EntityTypeHandle EntityType;

            public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
            {
                for (var i = 0; i < chunk.Count; i++)
                {

                }
            }
        }

        protected override void OnUpdate()
        {
            var job = new MoveJob()
            {
                TranslationType = GetComponentTypeHandle<Translation>(),
                EntityType = GetEntityTypeHandle()
            };

            Dependency = job.Schedule(_destinationQuery, Dependency);
        }
    }
}
