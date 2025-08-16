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
                    var newHealth = target.Stats.TakeDamage(Amount);
                    ctx.RaiseHealthChanged(target.Id, newHealth);
                }
            }
        }
    }
}
