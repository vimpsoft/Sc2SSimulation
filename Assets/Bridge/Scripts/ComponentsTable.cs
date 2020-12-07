using System;
using System.Collections.Generic;
using Sc2Simulation.Runtime.Buildings;
using Sc2Simulation.Runtime.Mining;
using Sc2Simulation.Runtime.Moving;
using Unity.Entities;
using UnityEngine;

namespace Sc2Simulation.Brirge
{
    /// <summary>
    /// Используем этот класс как таблицу для сопоставления типов по именам. Можно было бы воспользоваться
    /// рефлексией, чтобы сделать более изящно, но что-то мне подсказывает, что dots и рефлексия не очень
    /// сочетаются.
    /// </summary>
    public static class ComponentsTable
    {
        private static Dictionary<string, (Type converterType, Action<World, Entity, object> addingFunction)> _convertersMap = new Dictionary<string, (Type converterType, Action<World, Entity, object>)>()
        {
            { nameof(MineCommandConversion), (typeof(MineCommandConversion), (w, e, o) => w.EntityManager.AddComponentData(e, (MineCommand) o))}
            , { nameof(MineralsCarrierConversion), (typeof(MineralsCarrierConversion), (w, e, o) => w.EntityManager.AddComponentData(e, (MineralsCarrier) o))}
            , { nameof(MineralDruseConversion), (typeof(MineralDruseConversion), (w, e, o) => w.EntityManager.AddComponentData(e, (MineralDruse) o))}
            , { nameof(MovableConversion), (typeof(MovableConversion), (w, e, o) => w.EntityManager.AddComponentData(e, (Movable) o))}
            , { nameof(SpeedConversion), (typeof(SpeedConversion), (w, e, o) => w.EntityManager.AddComponentData(e, (Speed) o))}
        };

        public static void AddComponentData(this World world, string converterTypeId, Entity target, Entity[] allEntities,
            string serializedData)
        {
            var converterType = _convertersMap[converterTypeId];
            var componentObject =
                (JsonUtility.FromJson(serializedData, converterType.converterType) as ComponentConvertion)
                .Convert(allEntities);
            converterType.addingFunction(world, target, componentObject);
        }
    }
}
