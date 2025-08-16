namespace Lex.Sim.Effects
{
    /// <summary>
    /// Provides the context for planning an effect. This includes the source of the
    /// effect, the simulation state, and potentially targets.
    /// </summary>
    public sealed class PlanContext
    {
        public readonly SimContext Sim;
        public readonly Entity Source;
        public readonly TargetSet Targets;
        public readonly Card SourceCard;

        public PlanContext(SimContext sim, Entity source, Card sourceCard, TargetSet targets)
        {
            Sim = sim;
            Source = source;
            SourceCard = sourceCard;
            Targets = targets;
        }
    }
}
