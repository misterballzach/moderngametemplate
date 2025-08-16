using System;

namespace Lex.Sim
{
    /// <summary>
    /// A deterministic random number generator using the xoshiro128** algorithm.
    /// This is NOT thread-safe. A separate instance should be used per thread/simulation.
    /// </summary>
    public sealed class RngStream
    {
        private uint s0, s1, s2, s3;

        /// <summary>
        /// Initializes a new instance of the RngStream with a 64-bit seed.
        /// </summary>
        public RngStream(ulong seed)
        {
            // Use a SplitMix64 generator to initialize the state from the seed.
            // This is more robust than directly using parts of the seed.
            ulong state = seed;
            s0 = (uint)SplitMix64(ref state);
            s1 = (uint)SplitMix64(ref state);
            s2 = (uint)SplitMix64(ref state);
            s3 = (uint)SplitMix64(ref state);
        }

        private static ulong SplitMix64(ref ulong state)
        {
            ulong z = (state += 0x9e3779b97f4a7c15);
            z = (z ^ (z >> 30)) * 0xbf58476d1ce4e5b9;
            z = (z ^ (z >> 27)) * 0x94d049bb133111eb;
            return z ^ (z >> 31);
        }

        private static uint Rotl(uint x, int k)
        {
            return (x << k) | (x >> (32 - k));
        }

        /// <summary>
        /// Returns a random 32-bit unsigned integer from the stream.
        /// </summary>
        public uint Next()
        {
            // xoshiro128** algorithm
            uint result = Rotl(s1 * 5, 7) * 9;
            uint t = s1 << 9;

            s2 ^= s0;
            s3 ^= s1;
            s1 ^= s2;
            s0 ^= s3;

            s2 ^= t;
            s3 = Rotl(s3, 11);

            return result;
        }

        /// <summary>
        /// Returns a random integer within the specified range [min, max] (inclusive).
        /// </summary>
        public int Next(int min, int max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException(nameof(min), "min cannot be greater than max");
            }

            long range = (long)max - min + 1;
            if (range == 0) return min; // Should only happen if min == max

            // This is a common way to map a random uint to a range.
            // It has a slight bias, but is acceptable for most game development purposes.
            return (int)(min + (Next() % (ulong)range));
        }

        /// <summary>
        /// Returns a random float within [0.0f, 1.0f] (inclusive).
        /// </summary>
        public float NextFloat()
        {
            // 0x3f800000 is the bit representation of 1.0f.
            // We shift the top 9 random bits into the mantissa of a float.
            return (Next() >> 8) * (1.0f / (1 << 24));
        }
    }
}
