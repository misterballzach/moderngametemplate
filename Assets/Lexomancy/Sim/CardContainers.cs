using System.Collections.Generic;

namespace Lex.Sim
{
    public class Deck
    {
        private readonly List<Card> _cards = new List<Card>();

        public int Count => _cards.Count;

        public Deck(IEnumerable<Card> cards)
        {
            _cards.AddRange(cards);
        }

        public void Shuffle(RngStream rng)
        {
            // Fisher-Yates shuffle
            int n = _cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(0, n);
                Card value = _cards[k];
                _cards[k] = _cards[n];
                _cards[n] = value;
            }
        }

        public Card Draw()
        {
            if (_cards.Count == 0)
            {
                return null;
            }

            var card = _cards[0];
            _cards.RemoveAt(0);
            return card;
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }
    }

    public class Hand
    {
        public readonly List<Card> Cards = new List<Card>();
        public int MaxSize { get; set; } = 10;

        public bool IsFull => Cards.Count >= MaxSize;

        public bool TryAdd(Card card)
        {
            if (IsFull) return false;
            Cards.Add(card);
            return true;
        }

        public void Remove(Card card)
        {
            Cards.Remove(card);
        }
    }

    public class Pile
    {
        public readonly List<Card> Cards = new List<Card>();

        public void Add(Card card)
        {
            Cards.Add(card);
        }

        public void AddRange(IEnumerable<Card> cards)
        {
            Cards.AddRange(cards);
        }

        public void Clear()
        {
            Cards.Clear();
        }
    }
}
