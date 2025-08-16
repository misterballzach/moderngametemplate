namespace Lex.Sim
{
    public class BattleController
    {
        private readonly SimContext _ctx;
        private readonly Entity _player;
        private readonly Entity _enemy;
        private readonly Deck _playerDeck;
        private readonly Hand _playerHand;
        private readonly Pile _playerDiscard;

        public BattleController(SimContext ctx, Entity player, Entity enemy, Deck playerDeck)
        {
            _ctx = ctx;
            _player = player;
            _enemy = enemy;
            _playerDeck = playerDeck;
            _playerHand = new Hand();
            _playerDiscard = new Pile();

            _ctx.SetPlayer(player);
            _ctx.SetEnemy(enemy);
        }

        public void StartBattle()
        {
            _playerDeck.Shuffle(_ctx.Rng);
            DrawInitialHand();
            StartPlayerTurn();
        }

        private void DrawInitialHand(int count = 5)
        {
            for (int i = 0; i < count; i++)
            {
                DrawCard();
            }
        }

        private void DrawCard()
        {
            if (_playerDeck.Count == 0)
            {
                // Reshuffle discard pile into deck if deck is empty
                _playerDiscard.Cards.ForEach(_playerDeck.Add);
                _playerDiscard.Clear();
                _playerDeck.Shuffle(_ctx.Rng);
            }

            if (_playerDeck.Count > 0)
            {
                var card = _playerDeck.Draw();
                _playerHand.TryAdd(card);
            }
        }

        public void StartPlayerTurn()
        {
            // Placeholder for start-of-turn logic (e.g., mana refresh)
            DrawCard();
        }

        public void PlayCard(Card card, CardTemplate template)
        {
            if (!_playerHand.Cards.Contains(card) || _player.Stats.Mana < template.manaCost)
            {
                return; // Can't play this card
            }

            _player.Stats.Mana -= template.manaCost;
            _playerHand.Remove(card);
            _playerDiscard.Add(card);

            // Find targets using the card's targeting definition
            var targets = template.targeting != null ? template.targeting.FindTargets(_ctx, _player) : TargetSet.Empty();

            var planCtx = new PlanContext(_ctx, _player, card, targets);
            foreach (var effect in template.effects)
            {
                if (effect != null)
                {
                    var plannedEffect = effect.Plan(planCtx);
                    plannedEffect.Enqueue(_ctx);
                }
            }

            // The core loop: resolve all queued commands until none are left.
            _ctx.Resolve();
        }

        public void EndPlayerTurn()
        {
            // Placeholder for end-of-turn logic and triggering the enemy's turn.
        }
    }
}
