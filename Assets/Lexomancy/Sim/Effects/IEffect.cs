namespace Lex.Sim.Effects
{
    /// <summary>
    /// Represents the result of "planning" an effect. Contains all the concrete
    /// data needed to enqueue the commands for the simulation.
    /// </summary>
    public interface IPlannedEffect
    {
        void Enqueue(SimContext ctx);
    }

    /// <summary>
    /// Represents the definition of an effect, which can be planned into an IPlannedEffect.
    /// This is the logic part of our data-driven ability system.
    /// </summary>
    public interface IEffectDefinition
    {
        IPlannedEffect Plan(PlanContext ctx);
    }
}
