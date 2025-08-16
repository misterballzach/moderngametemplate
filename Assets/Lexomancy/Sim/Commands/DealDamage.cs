namespace Lex.Sim.Commands
{
    public readonly struct DealDamage : ICommand
    {
        public readonly EntityId TargetId;
        public readonly int Amount;

        public DealDamage(EntityId targetId, int amount)
        {
            TargetId = targetId;
            Amount = amount;
        }

        public void Execute(SimContext ctx)
        {
            if (ctx.Entities.TryGetValue(TargetId, out var target))
            {
                if (target.IsAlive)
                {
                    target.Stats.TakeDamage(Amount);
                    // Later, we can have the log record this event
                    // ctx.Log.Record(new DamageEvent(...));
                }
            }
        }
    }
}
