namespace Lex.Sim
{
    /// <summary>
    /// A placeholder for a card instance. This will later be linked to a CardTemplate (ScriptableObject).
    /// </summary>
    public class Card
    {
        private static int _nextId = 0;

        /// <summary>
        /// Unique identifier for this specific instance of a card.
        /// </summary>
        public int InstanceId { get; }

        /// <summary>
        /// The ID of the CardTemplate asset this card was created from.
        /// </summary>
        public string TemplateId { get; }

        public Card(string templateId)
        {
            InstanceId = _nextId++;
            TemplateId = templateId;
        }
    }
}
