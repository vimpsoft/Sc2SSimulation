using System;
using System.Collections.Generic;

namespace Sc2Simulation.Brirge
{
    /// <summary>
    /// Используем этот класс как таблицу для сопоставления типов по именам. Можно было бы воспользоваться
    /// рефлексией, чтобы сделать более изящно, но что-то мне подсказывает, что dots и рефлексия не очень
    /// сочетаются.
    /// </summary>
    public static class ComponentsTable
    {

        private static Dictionary<string, Type> _map = new Dictionary<string, Type>()
        {
            { nameof(MineCommandConversion), typeof(MineCommandConversion) }
        };

        public static Type GetComponentData(string componentId) => _map[componentId];
    }
}
