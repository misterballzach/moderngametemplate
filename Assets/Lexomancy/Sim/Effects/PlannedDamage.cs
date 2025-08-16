using Lex.Sim.Commands;

namespace Lex.Sim.Effects
{
    public readonly struct PlannedDamage : IPlannedEffect
    {
        private readonly EntityId _targetId;
        private readonly int _amount;

        public PlannedDamage(EntityId targetId, int amount)
        {
            _targetId = targetId;
            _amount = amount;
        }

        public void Enqueue(SimContext ctx)
        {
            // A planned effect's job is to translate data into concrete commands.
            ctx.Queue.Enqueue(new DealDamage(_targetId, _amount));
        }
    }
}
