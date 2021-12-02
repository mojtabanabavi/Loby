using System;
using System.Linq;
using System.Security.Cryptography;

namespace Loby.Tools
{
    /// <summary>
    /// An implementation of PBKDF2 hash algorithm.
    /// </summary>
    public class PasswordHasher
    {
        public static readonly int SaltSize = 16;
        public static readonly int Iterations = 10000;

        /// <summary>
        /// Returns a hashed representation of the supplied <paramref name="value"/>
        /// using PBKDF2 hash algorithm with 16 bytes salt and 10000 iterations.
        /// </summary>
        /// <param name="value">The value to hash.</param>
        /// <returns>
        /// A hashed representation of the supplied <paramref name="value"/>.
        /// </returns>
        public static string Hash(string value)
        {
            var hasher = new Rfc2898DeriveBytes(value, SaltSize, Iterations);

            var hashString = GenerateHashString(hasher);

            return hashString;
        }

        /// <summary>
        /// Returns a flag that indicating the result of a value hash comparison.
        /// </summary>
        /// <param name="hashedValue">The hash value that is stored.</param>
        /// <param name="providedValue">The value supplied for comparison.</param>
        /// <returns>
        /// A flag that indicating the result of a value hash comparison.
        /// </returns>
        public static bool Verify(string providedValue, string hashedValue)
        {
            var saltBytes = Convert.FromBase64String(hashedValue).Take(SaltSize).ToArray();

            var hasher = new Rfc2898DeriveBytes(providedValue, saltBytes, Iterations);

            var hashString = GenerateHashString(hasher);

            return hashString == hashedValue;
        }

        /// <summary>
        /// Create a base64 string representation of hashed value.
        /// </summary>
        /// <param name="hasher">PBKDF2 key derivation (Rfc2898).</param>
        /// <returns>
        /// A base64 string representation of hashed value.
        /// </returns>
        private static string GenerateHashString(Rfc2898DeriveBytes hasher)
        {
            var hashedValue = hasher.GetBytes(SaltSize);
            var output = hasher.Salt.Concat(hashedValue).ToArray();

            return Convert.ToBase64String(output);
        }
    }
}
