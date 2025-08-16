using System;
using System.Diagnostics;

namespace Lex.Sim
{
    /// <summary>
    /// A type-safe wrapper for an integer to represent a unique entity in the simulation.
    /// </summary>
    [DebuggerDisplay("EntityId({Value})")]
    public readonly struct EntityId : IEquatable<EntityId>
    {
        public readonly int Value;

        public EntityId(int value)
        {
            Value = value;
        }

        public bool Equals(EntityId other) => Value == other.Value;
        public override bool Equals(object obj) => obj is EntityId other && Equals(other);
        public override int GetHashCode() => Value;
        public static bool operator ==(EntityId left, EntityId right) => left.Equals(right);
        public static bool operator !=(EntityId left, EntityId right) => !left.Equals(right);
        public override string ToString() => Value.ToString();
    }
}
