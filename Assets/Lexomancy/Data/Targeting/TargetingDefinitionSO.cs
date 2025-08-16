using UnityEngine;
using Lex.Sim;
using System.Collections.Generic;

namespace Lex.Data.Targeting
{
    /// <summary>
    /// Abstract ScriptableObject base class for all targeting logic.
    /// </summary>
    public abstract class TargetingDefinitionSO : ScriptableObject
    {
        /// <summary>
        /// Finds and returns a set of targets based on the current simulation context.
        /// </summary>
        /// <param name="ctx">The current simulation context.</param>
        /// <param name="source">The entity initiating the targeting.</param>
        /// <returns>A TargetSet containing the IDs of the found targets.</returns>
        public abstract TargetSet FindTargets(SimContext ctx, Entity source);
    }
}
