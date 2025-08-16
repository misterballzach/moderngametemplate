using UnityEngine;
using Lex.Sim;
using Lex.Data;
using System.Collections.Generic;

namespace Lex.Pres
{
    public class BattlePresenter : MonoBehaviour
    {
        [Header("Configuration")]
        [Tooltip("Stat block defining the player's initial stats.")]
        public StatBlock playerStatConfig;
        [Tooltip("Stat block defining the enemy's initial stats.")]
        public StatBlock enemyStatConfig;
        [Tooltip("The list of cards in the player's deck.")]
        public List<CardTemplate> playerDeckConfig;

        [Header("Scene References")]
        public EntityView playerView;
        public EntityView enemyView;
        public HandView handView;

        private SimContext _ctx;
        private BattleController _battle;
        private Dictionary<EntityId, EntityView> _entityViews = new Dictionary<EntityId, EntityView>();
        private Dictionary<string, CardTemplate> _cardTemplates = new Dictionary<string, CardTemplate>();

        private CardView _selectedCard;

        void Start()
        {
            // 0. Index templates for easy lookup
            foreach (var template in playerDeckConfig)
            {
                _cardTemplates[template.name] = template;
            }

            // 1. Create the simulation from config
            _ctx = new SimContext(seed: (ulong)Random.Range(0, int.MaxValue));

            // Create runtime instances from the config assets
            var player = _ctx.CreateEntity(new StatBlock(playerStatConfig));
            var enemy = _ctx.CreateEntity(new StatBlock(enemyStatConfig));

            // Map sim IDs to scene views
            _entityViews[player.Id] = playerView;
            _entityViews[enemy.Id] = enemyView;

            var deckCards = new List<Card>();
            foreach (var template in playerDeckConfig)
            {
                // The simulation Card needs to know its template's ID
                deckCards.Add(new Card(template.name));
            }
            var deck = new Deck(deckCards);

            _battle = new BattleController(_ctx, player, enemy, deck);

            // 2. Subscribe to events
            _ctx.HealthChanged += OnHealthChanged;
            _ctx.CardDrawn += OnCardDrawn;
            _ctx.CardDiscarded += OnCardDiscarded;
            handView.OnCardSelected += OnCardSelected;
            playerView.OnEntityClicked += OnEntityClicked;
            enemyView.OnEntityClicked += OnEntityClicked;

            // 3. Start the battle
            _battle.StartBattle();
        }

        private void OnHealthChanged(EntityId entityId, int newHealth)
        {
            if (_entityViews.TryGetValue(entityId, out var view))
            {
                // We also need the max health. Let's get it from the sim entity.
                if (_ctx.Entities.TryGetValue(entityId, out var simEntity))
                {
                    view.UpdateHealth(newHealth, simEntity.Stats.MaxHealth);
                }
            }
        }

        private void OnCardDrawn(Card simCard)
        {
            if (_cardTemplates.TryGetValue(simCard.TemplateId, out var template))
            {
                handView.AddCard(simCard, template);
            }
        }

        private void OnCardDiscarded(Card simCard)
        {
            handView.RemoveCard(simCard);
        }

        private void OnCardSelected(CardView cardView)
        {
            // In a real game, you might highlight the card, show targeting arrows, etc.
            Debug.Log($"Selected card: {cardView.Template.cardName}");
            _selectedCard = cardView;
        }

        private void OnEntityClicked(EntityView entityView)
        {
            if (_selectedCard == null) return;

            Debug.Log($"Targeted entity. Playing card {_selectedCard.Template.cardName}");
            _battle.PlayCard(_selectedCard.SimCard, _selectedCard.Template);
            _selectedCard = null;
        }

        void OnDestroy()
        {
            // Unsubscribe from events to prevent memory leaks
            if (_ctx != null)
            {
                _ctx.HealthChanged -= OnHealthChanged;
                _ctx.CardDrawn -= OnCardDrawn;
                _ctx.CardDiscarded -= OnCardDiscarded;
            }
            if (handView != null)
            {
                handView.OnCardSelected -= OnCardSelected;
            }
            if(playerView != null) playerView.OnEntityClicked -= OnEntityClicked;
            if(enemyView != null) enemyView.OnEntityClicked -= OnEntityClicked;
        }
    }
}
