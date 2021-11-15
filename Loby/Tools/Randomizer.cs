using System;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Loby
{
    /// <summary>
    /// Represents a pseudo-random data generator, which is an algorithm that produces
    /// a sequence of data that meet certain statistical requirements for randomness.
    /// </summary>
    public static class Randomizer
    {
        #region Bytes

        /// <summary>
        /// Create a cryptographically strong random sequence of values 
        /// for a specified number of bytes.
        /// </summary>
        /// <param name="size">The number of bytes to generate.</param>
        /// <returns>
        /// A sequence of random bytes for the specified size.
        /// </returns>
        public static byte[] RandomBytes(int size)
        {
            var randomBytes = new byte[size];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        /// <summary>
        /// Returns a random 8-bit unsigned integer.
        /// </summary>
        /// <returns>
        /// A 8-bit unsigned integer that is greater than or equal to 0 and less than 255.
        /// </returns>
        public static byte RandomByte()
        {
            var randomBytes = RandomBytes(1);

            byte generatedByte = randomBytes.Single();

            return generatedByte;
        }

        #endregion;

        #region Int

        /// <summary>
        /// Returns a non-negative random 32-bit integer.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0 and less than System.Int32.MaxValue.
        /// </returns>
        public static int RandomInt()
        {
            var randomBytes = RandomBytes(4);

            int generatedInt = BitConverter.ToInt32(randomBytes);

            return Math.Abs(generatedInt);
        }

        /// <summary>
        /// Returns a non-negative random 32-bit integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. maxValue must
        /// be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0, and less than maxValue;
        /// the range of return values ordinarily includes 0 but not maxValue. However,
        /// if maxValue equals 0, maxValue is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// maxValue is less than 0.
        /// </exception>
        public static int RandomInt(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue is less than 0.");
            }

            return Math.Abs(RandomInt(0, maxValue));
        }

        /// <summary>
        /// Returns a random 32-bit integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">
        /// The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. maxValue must be greater
        /// than or equal to minValue.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// the range of return values includes minValue but not maxValue. If minValue
        /// equals maxValue, minValue is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// minValue is greater than maxValue.
        /// </exception>
        public static int RandomInt(int minValue, int maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }

            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue is greater than maxValue.");
            }

            int generatedInt = RandomInt();

            return (generatedInt % (maxValue - minValue)) + minValue;
        }

        #endregion;

        #region Long

        /// <summary>
        /// Returns a non-negative random 64-bit integer.
        /// </summary>
        /// <returns>
        /// A 64-bit signed integer that is greater than or equal to 0 and less than System.Int64.MaxValue.
        /// </returns>
        public static long RandomLong()
        {
            var randomBytes = RandomBytes(8);

            long generatedLong = BitConverter.ToInt64(randomBytes);

            return Math.Abs(generatedLong);
        }

        /// <summary>
        /// Returns a non-negative 64-bit random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. maxValue must
        /// be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A 64-bit signed integer that is greater than or equal to 0, and less than maxValue;
        /// the range of return values ordinarily includes 0 but not maxValue. However,
        /// if maxValue equals 0, maxValue is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// maxValue is less than 0.
        /// </exception>
        public static long RandomLong(long maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("maxValue is less than 0.");
            }

            return Math.Abs(RandomLong(0, maxValue));
        }

        /// <summary>
        /// Returns a random 64-bit integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">
        /// The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. maxValue must be greater
        /// than or equal to minValue.</param>
        /// <returns>
        /// A 64-bit signed integer greater than or equal to minValue and less than maxValue;
        /// the range of return values includes minValue but not maxValue. If minValue
        /// equals maxValue, minValue is returned.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// minValue is greater than maxValue.
        /// </exception>
        public static long RandomLong(long minValue, long maxValue)
        {
            if (minValue == maxValue)
            {
                return minValue;
            }

            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("minValue is greater than maxValue.");
            }

            long generatedLong = RandomLong();

            return (generatedLong % (maxValue - minValue)) + minValue;
        }

        #endregion;

        #region Bool

        /// <summary>
        /// Returns a random boolean.
        /// </summary>
        /// <returns>
        /// A random boolean.
        /// </returns>
        public static bool RandomBool()
        {
            return RandomInt(0, 2) == 1;
        }

        #endregion;

        #region Color

        /// <summary>
        /// Returns a random <see cref="Color"/>.
        /// </summary>
        /// <returns>
        /// A random color.
        /// </returns>
        public static Color RandomColor()
        {
            return Color.FromArgb(RandomInt(255), RandomInt(255), RandomInt(255));
        }

        #endregion;

        #region Float

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0,
        /// and less than 1.0.
        /// </summary>
        /// <returns>
        /// A float-precision floating point number that is greater than or equal to 0.0,
        /// and less than 1.0.
        /// </returns>
        public static float RandomFloat()
        {
            return RandomInt() * (float)1.0 / int.MaxValue;
        }

        #endregion;

        #region Double

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0,
        /// and less than 1.0.
        /// </summary>
        /// <returns>
        /// A double-precision floating point number that is greater than or equal to 0.0,
        /// and less than 1.0.
        /// </returns>
        public static double RandomDouble()
        {
            return RandomInt() * 1.0 / int.MaxValue;
        }

        #endregion;

        #region DateTime

        /// <summary>
        /// Returns a date-time instance that is greater than or equal to 01/01/1900 and less than now.
        /// </summary>
        /// <returns>
        /// A date-time instance greater than or equal to 01/01/1900 and less than now.
        /// </returns>
        public static DateTime RandomDateTime()
        {
            return RandomDateTime(new DateTime(1900, 1, 1), DateTime.Now);
        }

        /// <summary>
        /// Returns a date-time instance that is within a specified range.
        /// </summary>
        /// <param name="fromDate">
        /// The inclusive lower bound of the random date-time returned.
        /// </param>
        /// <param name="toDate">
        /// The exclusive upper bound of the random date-time returned. toDate must be greater
        /// than or equal to fromDate.
        /// </param>
        /// <returns>
        /// A date-time instance greater than or equal to fromDate and less than toDate.
        /// If fromDate equals toDate, fromDate is returned.
        /// </returns>
        public static DateTime RandomDateTime(DateTime fromDate, DateTime toDate)
        {
            var rangeTicks = toDate.Ticks - fromDate.Ticks;

            return fromDate.AddTicks((long)(RandomDouble() * rangeTicks));
        }

        #endregion;

        #region GUID

        /// <summary>
        ///  Returns a string representation of the globally unique identifier (GUID).
        /// </summary>
        /// <returns>
        /// A string representation of the globally unique identifier (GUID), formatted by using the "D" format specifier as
        /// follows: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx where the value of the GUID is
        /// represented as a series of lowercase hexadecimal digits in groups of 8, 4, 4,
        /// 4, and 12 digits and separated by hyphens. An example of a return value is "382c74c3-721d-4f34-80e5-57657b6cbc27".
        /// To convert the hexadecimal digits from a through f to uppercase, call the <see cref="String.ToUpper"/>
        /// method on the returned string.
        /// </returns>
        public static string RandomGuid()
        {
            return Guid.NewGuid().ToString();
        }

        #endregion;

        #region AlphaNmeric

        private const string AlphaNumerics = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// Returns a random string of specified length.
        /// </summary>
        /// <param name="length">
        /// The random string length to be generated. length must
        /// be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A random string represented as a series of lowercase and uppercase alphabets and digits.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// length is less than 0.
        /// </exception>
        public static string RandomAlphaNmeric(int length)
        {
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length is less than 0.");
            }

            var rand = new Random();

            var result = Enumerable
                .Repeat(AlphaNumerics, length)
                .Select(chars => chars[rand.Next(chars.Length)])
                .ToArray();

            return new string(result);
        }

        #endregion;

        #region Select

        /// <summary>
        /// Select and return a random element from the sequence.
        /// </summary>
        /// <typeparam name="Type">
        /// Type of sequence elements.
        /// </typeparam>
        /// <param name="values">
        /// A sequence of elements in which a random element is to be selected.
        /// </param>
        /// <returns>
        /// A random element from the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is null.
        /// </exception>
        public static Type SelectRandom<Type>(params Type[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values), "is null");
            }

            return values[RandomInt(values.Length)];
        }

        /// <summary>
        /// Select and return a random element from the sequence.
        /// </summary>
        /// <typeparam name="Type">
        /// Type of sequence elements.
        /// </typeparam>
        /// <param name="values">
        /// A sequence of elements in which a random element is to be selected.
        /// </param>
        /// <returns>
        /// A random element from the sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is null.
        /// </exception>
        public static Type SelectRandom<Type>(IEnumerable<Type> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values), "is null");
            }

            return values.ElementAt(RandomInt(values.Count()));
        }

        /// <summary>
        /// Select and return a sequence of random elements from the input sequence.
        /// </summary>
        /// <typeparam name="Type">
        /// Type of sequence elements.
        /// </typeparam>
        /// <param name="values">
        /// A sequence of elements in which some random elements is to be selected.
        /// </param>
        /// <returns>
        /// A sequence of elements that is randomly selected from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is null.
        /// </exception>     
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> is less than zero (0) or more than <paramref name="values"/> length.
        /// </exception>
        public static IEnumerable<Type> SelectRandom<Type>(IEnumerable<Type> values, int count)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values), "is null");
            }

            if (count < 0 || count > values.Count())
            {
                throw new ArgumentOutOfRangeException(nameof(count), "is less than 0 or more than values length.");
            }

            return values.OrderBy(x => RandomInt()).Take(count);
        }

        #endregion;
    }
}
