namespace Lex.Sim
{
    /// <summary>
    /// A static class holding predefined TagSet constants for use in the simulation.
    /// </summary>
    public static class Tags
    {
        // Elemental Damage Types
        public static readonly TagSet Fire = new TagSet(1UL << 0);
        public static readonly TagSet Ice = new TagSet(1UL << 1);
        public static readonly TagSet Lightning = new TagSet(1UL << 2);
        public static readonly TagSet Poison = new TagSet(1UL << 3);
        public static readonly TagSet Arcane = new TagSet(1UL << 4);

        // Physical Damage Types
        public static readonly TagSet Slashing = new TagSet(1UL << 5);
        public static readonly TagSet Piercing = new TagSet(1UL << 6);
        public static readonly TagSet Bludgeoning = new TagSet(1UL << 7);

        // Effect Types
        public static readonly TagSet Damage = new TagSet(1UL << 10);
        public static readonly TagSet Healing = new TagSet(1UL << 11);
        public static readonly TagSet Shield = new TagSet(1UL << 12);
        public static readonly TagSet DrawCard = new TagSet(1UL << 13);

        // Status Effects
        public static readonly TagSet Stun = new TagSet(1UL << 20);
        public static readonly TagSet Bleed = new TagSet(1UL << 21);
        public static readonly TagSet Burn = new TagSet(1UL << 22);

        // Immunities & Resistances
        public static readonly TagSet StunImmune = new TagSet(1UL << 30);
        public static readonly TagSet FireResistant = new TagSet(1UL << 31);
    }
}
