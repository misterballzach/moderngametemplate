using NUnit.Framework;
using Lex.Sim;
using Lex.Data;
using Lex.Data.Effects;
using System.Collections.Generic;
using UnityEngine;

namespace Lex.Tests
{
    [TestFixture]
    public class BattleControllerTests
    {
        private SimContext _ctx;
        private Entity _player;
        private Entity _enemy;
        private BattleController _battle;
        private Card _testCard;
        private Deck _deck;

        [SetUp]
        public void SetUp()
        {
            _ctx = new SimContext(seed: 100);
            _player = _ctx.CreateEntity(new StatBlock(maxHealth: 100, maxMana: 100));
            _enemy = _ctx.CreateEntity(new StatBlock(maxHealth: 100, maxMana: 100));

            _testCard = new Card();
            _deck = new Deck(new List<Card> { _testCard });

            _battle = new BattleController(_ctx, _player, _enemy, _deck);
        }

        [Test]
        public void PlayCard_WithEnemyTargeting_DealsDamageToEnemy()
        {
            // Arrange
            _battle.StartBattle(); // Draws the test card

            // Create a mock CardTemplate with a DamageEffectSO and EnemyTargetingSO
            var cardTemplate = ScriptableObject.CreateInstance<CardTemplate>();
            cardTemplate.manaCost = 5;

            var damageEffect = ScriptableObject.CreateInstance<DamageEffectSO>();
            damageEffect.baseAmount = 25;
            cardTemplate.effects = new List<EffectDefinitionSO> { damageEffect };

            var enemyTargeting = ScriptableObject.CreateInstance<EnemyTargetingSO>();
            cardTemplate.targeting = enemyTargeting;

            int initialPlayerHealth = _player.Stats.Health;
            int initialEnemyHealth = _enemy.Stats.Health;

            // Act
            _battle.PlayCard(_testCard, cardTemplate);

            // Assert
            Assert.AreEqual(initialEnemyHealth - damageEffect.baseAmount, _enemy.Stats.Health, "Enemy's health should be reduced.");
            Assert.AreEqual(initialPlayerHealth, _player.Stats.Health, "Player's health should remain unchanged.");
        }

        [Test]
        public void StartBattle_DrawsInitialHand()
        {
            // This test requires a deck with more cards
            var cards = new List<Card> { new Card(), new Card(), new Card(), new Card(), new Card(), new Card() };
            var freshDeck = new Deck(cards);
            var battle = new BattleController(_ctx, _player, _enemy, freshDeck);

            // Act
            battle.StartBattle();

            // Assert
            // Can't access hand directly, but we can infer from the deck count.
            // StartBattle draws 5 for initial hand, then 1 for first turn.
            Assert.AreEqual(0, freshDeck.Count, "Deck should be empty after drawing initial hand and first card.");
        }
    }
}
