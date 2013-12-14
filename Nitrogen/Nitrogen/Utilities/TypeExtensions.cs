using System;
using System.Diagnostics.Contracts;

namespace Nitrogen.Utilities
{
    /// <summary>
    /// Provides extension methods for various types.
    /// </summary>
    public static class TypeExtensions
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Checks whether the specified <paramref name="value"/> is defined in its enum type.
        /// </summary>
        /// <param name="value">An enum value.</param>
        /// <returns>
        /// <c>true</c> if the <paramref name="value"/> is defined in the enum; otherwise, <c>false</c>.
        /// </returns>
        [Pure]
        public static bool IsDefined(this Enum value)
        {
            Contract.Requires(value != null);
            return Enum.IsDefined(value.GetType(), value);
        }

        /// <summary>
        /// Gets an <see cref="Int64"/> value representing the given <paramref name="value"/> as a
        /// Unix timestamp.
        /// </summary>
        /// <param name="value">A <see cref="DateTime"/> value.</param>
        /// <returns>
        /// An <see cref="Int64"/> value representing the total number of seconds since the epoch.
        /// </returns>
        public static double ToUnixTime(this DateTime value)
        {
            return value.Subtract(UnixEpoch).TotalSeconds;
        }

        /// <summary>
        /// Gets a <see cref="DateTime"/> value representing the given Unix timestamp.
        /// </summary>
        /// <param name="value">A Unix timestamp.</param>
        /// <returns>An <see cref="DateTime"/> value equivalent to the Unix timestamp.</returns>
        public static DateTime ToDateTime(this long value)
        {
            return UnixEpoch.AddSeconds(value);
        }
    }
}