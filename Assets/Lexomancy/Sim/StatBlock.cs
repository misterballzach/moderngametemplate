namespace Lex.Sim
{
    /// <summary>
    /// A data container for an entity's stats.
    /// </summary>
    public class StatBlock
    {
        public int MaxHealth;
        public int Health;

        public int MaxMana;
        public int Mana;

        public int Armor;

        // Add other stats as needed, e.g., Strength, Dexterity, etc.

        public StatBlock(int maxHealth, int maxMana, int armor = 0)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;

            MaxMana = maxMana;
            Mana = maxMana;

            Armor = armor;
        }

        // Convenience method to apply damage
        public void TakeDamage(int amount)
        {
            // Armor calculation could go here, but for now it's simple.
            int effectiveAmount = amount - Armor;
            if (effectiveAmount < 0) effectiveAmount = 0;

            Health -= effectiveAmount;
            if (Health < 0) Health = 0;
        }
    }
}
