using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Lex.Pres
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<CardView> OnCardClicked;

        [Header("UI References")]
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        public Image artImage;
        public TextMeshProUGUI manaCostText;

        public Lex.Sim.Card SimCard { get; private set; }
        public Lex.Data.CardTemplate Template { get; private set; }

        public void Populate(Lex.Sim.Card simCard, Lex.Data.CardTemplate template)
        {
            SimCard = simCard;
            Template = template;

            nameText.text = template.cardName;
            descriptionText.text = template.description;
            artImage.sprite = template.art;
            manaCostText.text = template.manaCost.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnCardClicked?.Invoke(this);
        }
    }
}
