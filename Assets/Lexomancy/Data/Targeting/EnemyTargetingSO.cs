using UnityEngine;
using Lex.Sim;

namespace Lex.Data.Targeting
{
    [CreateAssetMenu(menuName = "Lexomancy/Targeting/Enemy")]
    public class EnemyTargetingSO : TargetingDefinitionSO
    {
        public override TargetSet FindTargets(SimContext ctx, Entity source)
        {
            var opponent = ctx.GetOpponent(source);
            if (opponent != null)
            {
                return new TargetSet(opponent.Id);
            }
            return TargetSet.Empty();
        }
    }
}
