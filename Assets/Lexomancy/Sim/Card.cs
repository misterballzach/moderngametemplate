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

        public Card()
        {
            InstanceId = _nextId++;
        }
    }
}
