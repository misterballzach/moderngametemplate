using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

namespace Lex.Pres
{
    /// <summary>
    /// MonoBehaviour responsible for visually representing an entity in the scene.
    /// Updates UI elements like health bars.
    /// </summary>
    public class EntityView : MonoBehaviour, IPointerClickHandler
    {
        public event Action<EntityView> OnEntityClicked;

        [Header("UI References")]
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI healthText;

        // We'll add a reference to the simulation entity ID later.
        // public Lex.Sim.EntityId SimId { get; private set; }

        /// <summary>
        /// Updates the health display.
        /// </summary>
        /// <param name="currentHealth">The entity's current health.</param>
        /// <param name="maxHealth">The entity's maximum health.</param>
        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth;
                healthSlider.value = currentHealth;
            }
            if (healthText != null)
            {
                healthText.text = $"{currentHealth} / {maxHealth}";
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnEntityClicked?.Invoke(this);
        }
    }
}
