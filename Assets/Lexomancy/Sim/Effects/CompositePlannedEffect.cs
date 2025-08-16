using System.Collections.Generic;

namespace Lex.Sim.Effects
{
    /// <summary>
    /// An IPlannedEffect that holds a list of other IPlannedEffects.
    /// Used to compose multiple effects from a single definition (e.g., for multi-target abilities).
    /// </summary>
    public readonly struct CompositePlannedEffect : IPlannedEffect
    {
        private readonly List<IPlannedEffect> _effects;

        public CompositePlannedEffect(List<IPlannedEffect> effects)
        {
            _effects = effects;
        }

        public void Enqueue(SimContext ctx)
        {
            if (_effects == null) return;

            foreach (var effect in _effects)
            {
                effect.Enqueue(ctx);
            }
        }
    }
}
