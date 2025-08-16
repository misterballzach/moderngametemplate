using System.Collections.Generic;

namespace Lex.Sim
{
    /// <summary>
    /// A struct to hold a collection of entity IDs that are the targets of an effect.
    /// </summary>
    public readonly struct TargetSet
    {
        public readonly List<EntityId> Entities;

        public TargetSet(List<EntityId> entities)
        {
            Entities = entities;
        }

        public TargetSet(EntityId singleEntity)
        {
            Entities = new List<EntityId> { singleEntity };
        }

        public static TargetSet Empty() => new TargetSet(new List<EntityId>());
    }
}
