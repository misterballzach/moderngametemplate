using UnityEngine;
using Lex.Sim;

namespace Lex.Data.Targeting
{
    [CreateAssetMenu(menuName = "Lexomancy/Targeting/Self")]
    public class SelfTargetingSO : TargetingDefinitionSO
    {
        public override TargetSet FindTargets(SimContext ctx, Entity source)
        {
            return new TargetSet(source.Id);
        }
    }
}
