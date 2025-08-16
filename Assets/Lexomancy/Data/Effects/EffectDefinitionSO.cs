using UnityEngine;
using Lex.Sim;
using Lex.Sim.Effects;

namespace Lex.Data.Effects
{
    /// <summary>
    /// Abstract ScriptableObject base class for all effect definitions.
    /// This allows us to create effects as data assets in the Unity Editor.
    /// </summary>
    public abstract class EffectDefinitionSO : ScriptableObject, IEffectDefinition
    {
        /// <summary>
        /// When implemented in a derived class, this method plans the effect,
        /// returning a concrete IPlannedEffect that can be enqueued into the simulation.
        /// </summary>
        public abstract IPlannedEffect Plan(PlanContext ctx);
    }
}
