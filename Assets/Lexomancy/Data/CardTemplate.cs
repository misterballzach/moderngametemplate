using UnityEngine;
using System.Collections.Generic;
using Lex.Data.Effects;

namespace Lex.Data
{
    [CreateAssetMenu(menuName = "Lexomancy/Card Template")]
    public class CardTemplate : ScriptableObject
    {
        [Header("Card Info")]
        public string cardName;
        [TextArea]
        public string description;
        public Sprite art;

        [Header("Gameplay")]
        public int manaCost;
        public TargetingDefinitionSO targeting;

        // This is the core of the data-driven design. A card is just a sequence of effects.
        [SerializeReference]
        public List<EffectDefinitionSO> effects;
    }
}
