using UnityEngine;
using System.Collections.Generic;
using System;

namespace Lex.Pres
{
    public class HandView : MonoBehaviour
    {
        public event Action<CardView> OnCardSelected;

        public GameObject cardViewPrefab;
        public Transform handContainer;

        private Dictionary<int, CardView> _cardViews = new Dictionary<int, CardView>();

        public void AddCard(Lex.Sim.Card simCard, Lex.Data.CardTemplate template)
        {
            if (_cardViews.ContainsKey(simCard.InstanceId)) return;

            var cardGO = Instantiate(cardViewPrefab, handContainer);
            var cardView = cardGO.GetComponent<CardView>();
            cardView.Populate(simCard, template);
            cardView.OnCardClicked += HandleCardClicked;
            _cardViews[simCard.InstanceId] = cardView;

            // In a real game, we'd have a layout group or code to arrange the cards nicely.
        }

        public void RemoveCard(Lex.Sim.Card simCard)
        {
            if (_cardViews.TryGetValue(simCard.InstanceId, out var cardView))
            {
                cardView.OnCardClicked -= HandleCardClicked;
                Destroy(cardView.gameObject);
                _cardViews.Remove(simCard.InstanceId);
            }
        }

        private void HandleCardClicked(CardView cardView)
        {
            OnCardSelected?.Invoke(cardView);
        }
    }
}
