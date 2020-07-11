using Unity.Entities;

namespace Sc2Simulation.Brirge
{
    /// <summary>
    /// Базовый класс для промежуточной версии. Нужен он в основном для того, чтобы сконвертировать 
    /// ссылки на юнити-объекты в ссылки энтити-типа, т.е. по индексу. Прдеполагается что очередность
    /// энтитей при сериализации и десериализации одинаковая, поэтому надо просто подставлять в 
    /// десериализованном компоненте энтити по индексу из orderedEntities.
    /// </summary>
    public abstract class ComponentConvertion
    {
        public abstract object Convert(Entity[] orderedEntities);
    }
}