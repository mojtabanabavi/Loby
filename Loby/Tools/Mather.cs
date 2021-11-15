using System;

namespace Loby
{
    public static class Mather
    {
        /// <summary>
        /// Rounds a <see cref="float"/> value to a specified number of fractional
        /// digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="number">
        /// A float number to be rounded.
        /// </param>
        /// <param name="digits">
        /// The number of fractional digits in the return value.
        /// </param>
        /// <returns>
        /// The number nearest to value that contains a number of fractional digits equal
        /// to digits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// digits is less than 0 or greater than 15.
        /// </exception>
        public static float Round(this float number, int fractionalDigits = 2)
        {
            return (float)Math.Round(number, fractionalDigits);
        }

        /// <summary>
        /// Rounds a <see cref="double"/> value to a specified number of fractional
        /// digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="number">
        /// A double number to be rounded.
        /// </param>
        /// <param name="digits">
        /// The number of fractional digits in the return value.
        /// </param>
        /// <returns>
        /// The number nearest to value that contains a number of fractional digits equal
        /// to digits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// digits is less than 0 or greater than 15.
        /// </exception>
        public static double Round(this double number, int digits = 2)
        {
            return Math.Round(number, digits);
        }

        /// <summary>
        /// Rounds a <see cref="decimal"/> value to a specified number of fractional
        /// digits, and rounds midpoint values to the nearest even number.
        /// </summary>
        /// <param name="number">
        /// A decimal number to be rounded.
        /// </param>
        /// <param name="digits">
        /// The number of fractional digits in the return value.
        /// </param>
        /// <returns>
        /// The number nearest to value that contains a number of fractional digits equal
        /// to digits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// digits is less than 0 or greater than 28.
        /// </exception>
        /// <exception cref="OverflowException">
        /// The result is outside the range of a <see cref="decimal"/>.
        /// </exception>
        public static decimal Round(this decimal number, int digits = 2)
        {
            return Math.Round(number, digits);
        }
    }
}
