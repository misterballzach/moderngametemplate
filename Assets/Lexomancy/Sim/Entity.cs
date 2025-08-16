namespace Lex.Sim
{
    public class Entity
    {
        public EntityId Id { get; }
        public StatBlock Stats { get; }

        public System.Collections.Generic.List<Status> Statuses { get; } = new System.Collections.Generic.List<Status>();

        public bool IsAlive => Stats.Health > 0;

        public Entity(EntityId id, StatBlock stats)
        {
            Id = id;
            Stats = stats;
        }
    }
}
