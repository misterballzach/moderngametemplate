using UnityEngine;
using Lex.Sim;
using Lex.Sim.Effects;

namespace Lex.Data.Effects
{
    [CreateAssetMenu(menuName = "Lexomancy/Effects/Damage")]
    public class DamageEffectSO : EffectDefinitionSO
    {
        public int baseAmount;
        // In the future, this would reference a TargetingDefinitionSO.
        // For now, we'll just have a simple placeholder.

        public override IPlannedEffect Plan(PlanContext ctx)
        {
            // This effect will apply to all targets found by the card's targeting.
            var plannedEffects = new List<IPlannedEffect>();
            foreach (var targetId in ctx.Targets.Entities)
            {
                int finalAmount = baseAmount;
                // Future calculations could go here.
                plannedEffects.Add(new PlannedDamage(targetId, finalAmount));
            }
            // We need a way to return multiple planned effects. Let's create a composite planned effect.
            return new CompositePlannedEffect(plannedEffects);
        }
    }
}
