using System;
using System.Diagnostics;

namespace Lex.Sim
{
    /// <summary>
    /// A struct that efficiently stores a set of tags using a bitmask.
    /// Assumes a maximum of 64 unique tags.
    /// </summary>
    [DebuggerDisplay("{Mask}")]
    public readonly struct TagSet : IEquatable<TagSet>
    {
        public readonly ulong Mask;

        public TagSet(ulong mask)
        {
            Mask = mask;
        }

        /// <summary>
        /// Checks if this set contains all the tags from another set.
        /// </summary>
        public bool Has(TagSet other) => (Mask & other.Mask) == other.Mask;

        /// <summary>
        /// Checks if this set contains any of the tags from another set.
        /// </summary>
        public bool HasAny(TagSet other) => (Mask & other.Mask) != 0;

        /// <summary>
        /// Returns a new TagSet with the specified tags added.
        /// </summary>
        public TagSet With(TagSet other) => new TagSet(Mask | other.Mask);

        /// <summary>
        /// Returns a new TagSet with the specified tags removed.
        /// </summary>
        public TagSet Without(TagSet other) => new TagSet(Mask & ~other.Mask);

        public bool Equals(TagSet other) => Mask == other.Mask;
        public override bool Equals(object obj) => obj is TagSet other && Equals(other);
        public override int GetHashCode() => Mask.GetHashCode();
        public static bool operator ==(TagSet left, TagSet right) => left.Equals(right);
        public static bool operator !=(TagSet left, TagSet right) => !left.Equals(right);
        public static TagSet operator |(TagSet left, TagSet right) => left.With(right);
        public static TagSet operator &(TagSet left, TagSet right) => new TagSet(left.Mask & right.Mask);
        public static TagSet operator ~(TagSet set) => new TagSet(~set.Mask);
    }
}
