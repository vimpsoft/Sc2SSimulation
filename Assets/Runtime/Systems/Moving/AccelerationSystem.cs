using Assets.Runtime.Components.Moving;
using Sc2Simulation.Runtime.Moving;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateAfter(typeof(MiningEndBarrier))]
public class AccelerationSystem : SystemBase
{
    private EntityQuery _query;

    protected override void OnCreate()
    {
        _query = GetEntityQuery(new EntityQueryDesc()
        {
            All = new ComponentType[]
            {
                ComponentType.ReadOnly<Movable>(),
                ComponentType.ReadWrite<Speed>(),
                ComponentType.ReadOnly<Translation>(),
                ComponentType.ReadOnly<Destination>()
            }
        });
    }

    private struct AccelerationJob : IJobChunk
    {
        public float DeltaTime;
        [ReadOnly] public ComponentTypeHandle<Movable> MovableType;
        [ReadOnly] public ComponentTypeHandle<Destination> DestinationType;
        [ReadOnly] public ComponentTypeHandle<Translation> TranslationType;
        public ComponentTypeHandle<Speed> SpeedType;

        public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
        {
            var translations = chunk.GetNativeArray(TranslationType);
            var speeds = chunk.GetNativeArray(SpeedType);
            var movables = chunk.GetNativeArray(MovableType);
            var destinations = chunk.GetNativeArray(DestinationType);

            /*
             * Мы должны замедляться незадолго до прибытия в пункт назначения
             * и в то же время ускоряться если мы далеко
             * Акселерация в Movable - юниты в секунду. Но замедляться мы должны в зависимости
             * от расстояния.
             *
             * Пока мы достаточно далеко мы разгоняемся равномерно в соответствии
             * с акселерацией до максимальной скорости и затем поддерживаем ее.
             *
             * Когда же мы подходим к цели мы должны замедлиться. Чтобы замедлиться с заданной
             * заранее скоростью замедления мы должны понять сколько мы пройдем. Для простоты
             * очень грубо округлим это расстояние до MaxSpeed * (MaxSpeed / Acc) * 0.5f
             *
             * Если мы замедляемся ровно за 1 секунду, то с какой бы скоростью мы ни были сейчас
             * мы знаем с какой мы должны быть - остаток расстояния.
             *
             * Наша скорость 9, мы в 8, снижаем скорость до 8, мы в 1, снижаем до 1, и т.д.
             *
             * Короче пока будем замедляться только так - за 1 секунду.
             */
            for (var i = 0; i < chunk.Count; i++)
            {
                var distanceLeft= math.distance(destinations[i].Value, translations[i].Value);
                var distanceToStartDecelerate = movables[i].MaxSpeed;
                if (distanceToStartDecelerate > distanceLeft)
                    speeds[i] = new Speed() { Value = distanceLeft };
                else if (speeds[i].Value < movables[i].MaxSpeed)
                    speeds[i] = new Speed() { Value = math.min(speeds[i].Value + DeltaTime * movables[i].Acceleration, movables[i].MaxSpeed) };
            }
        }
    }

    protected override void OnUpdate()
    {
        var job = new AccelerationJob()
        {
            DeltaTime = Time.DeltaTime,
            TranslationType = GetComponentTypeHandle<Translation>(),
            SpeedType = GetComponentTypeHandle<Speed>(),
            MovableType = GetComponentTypeHandle<Movable>(),
            DestinationType = GetComponentTypeHandle<Destination>()
        };

        Dependency = job.Schedule(_query, Dependency);
    }
}
