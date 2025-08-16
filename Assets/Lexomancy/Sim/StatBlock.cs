namespace Lex.Sim
{
    /// <summary>
    /// A data container for an entity's stats.
    /// </summary>
    [System.Serializable]
    public class StatBlock
    {
        public int MaxHealth;
        public int MaxMana;
        public int Armor;

        [System.NonSerialized]
        public int Health;
        [System.NonSerialized]
        public int Mana;


        // Constructor for simulation logic
        public StatBlock(int maxHealth, int maxMana, int armor = 0)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            MaxMana = maxMana;
            Mana = maxMana;
            Armor = armor;
        }

        // Copy constructor for creating instances from inspector data
        public StatBlock(StatBlock source)
        {
            MaxHealth = source.MaxHealth;
            MaxMana = source.MaxMana;
            Armor = source.Armor;
            Health = source.MaxHealth;
            Mana = source.MaxMana;
        }

        // Convenience method to apply damage
        public int TakeDamage(int amount)
        {
            // Armor calculation could go here, but for now it's simple.
            int effectiveAmount = amount - Armor;
            if (effectiveAmount < 1) effectiveAmount = 1; // Always do at least 1 damage if amount > 0

            Health -= effectiveAmount;
            if (Health < 0) Health = 0;
            return Health;
        }
    }
}
