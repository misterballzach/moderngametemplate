using System.Collections.Generic;

namespace Lex.Sim
{
    /// <summary>
    /// Holds the entire state for a single battle simulation instance.
    /// This includes all entities, the action queue, and core services.
    /// </summary>
    public sealed class SimContext
    {
        public readonly ActionQueue Queue;
        public readonly RngStream Rng;
        public readonly BattleLog Log;
        public readonly Calculators Calcs;
        public readonly Dictionary<EntityId, Entity> Entities;
        public Entity Player { get; private set; }
        public Entity Enemy { get; private set; }

        private int _nextEntityId = 0;

        public SimContext(ulong seed)
        {
            Queue = new ActionQueue();
            Rng = new RngStream(seed);
            Log = new BattleLog();
            Calcs = new Calculators();
            Entities = new Dictionary<EntityId, Entity>();
        }

        public void SetPlayer(Entity player) => Player = player;
        public void SetEnemy(Entity enemy) => Enemy = enemy;

        public Entity GetOpponent(Entity entity)
        {
            if (entity.Id == Player?.Id) return Enemy;
            if (entity.Id == Enemy?.Id) return Player;
            return null;
        }

        /// <summary>
        /// Creates a new entity, assigns it a unique ID, and adds it to the simulation.
        /// </summary>
        public Entity CreateEntity(StatBlock stats)
        {
            var entityId = new EntityId(_nextEntityId++);
            var entity = new Entity(entityId, stats);
            Entities.Add(entityId, entity);
            return entity;
        }

        /// <summary>
        /// Resolves the action queue until it is empty.
        /// </summary>
        public void Resolve()
        {
            Queue.Resolve(this);
        }
    }
}
