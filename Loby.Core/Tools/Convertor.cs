using System;
using System.Text;
using System.Linq;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;

namespace Loby.Core
{
    public static class Convertor
    {
        #region String

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 32-bit
        /// signed integer.
        /// </summary>
        /// <param name="value">
        /// A string that contains the number to convert.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that is equivalent to the number in value, or 0 (zero)
        /// if value is null.
        /// </returns>
        /// <exception cref="FormatException">
        /// value is not a number in a valid format.
        /// </exception>
        /// <exception cref="OverflowException">
        /// value represents a number that is less than <see cref="Int32.MinValue"/> or greater
        /// than System.Int32.MaxValue.
        /// </exception>
        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 32-bit
        /// signed integer.
        /// return value will be 0 (zero) if conversion is not succeeded.
        /// </summary>
        /// <param name="value">A string that contains the number to convert.</param>
        /// <returns>
        /// A 32-bit signed integer that is equivalent to the number in value, or 0 (zero)
        /// if value is null -or- not convertable.
        /// </returns>
        public static int ToIntOrDefault(this string value)
        {
            int.TryParse(value, out int result);

            return result;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 64-bit
        /// signed integer.
        /// </summary>
        /// <param name="value">
        /// A string that contains the number to convert.
        /// </param>
        /// <returns>
        /// A 64-bit signed integer that is equivalent to the number in value, or 0 (zero)
        /// if value is null.
        /// </returns>
        /// <exception cref="FormatException">
        /// value is not a number in a valid format.
        /// </exception>
        /// <exception cref="OverflowException">
        /// value represents a number that is less than <see cref="Int64.MinValue"/> or greater
        /// than <see cref="Int64.MaxValue"/>.
        /// </exception>
        public static long ToLong(this string value)
        {
            return Convert.ToInt64(value);
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent 64-bit
        /// signed integer.
        /// return value will be 0 (zero) if conversion is not succeeded.
        /// </summary>
        /// <param name="value">
        /// A string that contains the number to convert.
        /// </param>
        /// <returns>
        /// A 64-bit signed integer that is equivalent to the number in value, or 0 (zero)
        /// if value is null -or- not convertable.
        /// </returns>
        public static long ToLongOrDefault(this string value)
        {
            long.TryParse(value, out long result);

            return result;
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent decimal number.
        /// </summary>
        /// <param name="value">
        /// A string that contains the number to convert.
        /// </param>
        /// <returns>
        /// A decimal number that is equivalent to the number in value, or 0 (zero) if value
        /// is null.
        /// </returns>
        /// <exception cref="FormatException">
        /// value is not a number in a valid format.
        /// </exception>
        /// <exception cref="OverflowException">
        /// value represents a number that is less than System.Decimal.MinValue or greater
        /// than System.Decimal.MaxValue.
        /// </exception>
        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }

        /// <summary>
        /// Converts the specified string representation of a number to an equivalent decimal.
        /// return value will be 0 (zero) if conversion is not succeeded.
        /// </summary>
        /// <param name="value">
        /// A string that contains the number to convert.
        /// </param>
        /// <returns>
        /// A decimal that is equivalent to the number in value, or 0 (zero)
        /// if value is null -or- not convertable.
        /// </returns>
        public static decimal ToDecimalOrDefault(this string value)
        {
            decimal.TryParse(value, out decimal result);

            return result;
        }

        /// <summary>
        /// Create a new instance of the <see cref="Uri"/> with the specified <paramref name="uri"/>.
        /// </summary>
        /// <param name="uri">
        /// A string that identifies the resource to be represented by the System.Uri instance.
        /// Note that an IPv6 address in string form must be enclosed within brackets. For
        /// example, "http://[2607:f8b0:400d:c06::69]".
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// uriString is null.
        /// </exception>
        /// <exception cref="UriFormatException">
        /// uriString is empty. -or- 
        /// The scheme specified in uriString is not correctly formed. see <see cref="Uri.CheckSchemeName(string?)"/>. -or- 
        /// uriString contains too many slashes. -or- 
        /// The password specified in uriString is not valid. -or- 
        /// The host name specified in uriString is not valid. -or- 
        /// The file name specified in uriString is not valid. -or- 
        /// The user name specified in uriString is not valid. -or- 
        /// The host or authority name specified in uriString cannot be terminated by backslashes. -or- 
        /// The port number specified in uriString is not valid or cannot be parsed. -or- 
        /// The length of uriString exceeds 65519 characters. -or- 
        /// The length of the scheme specified in uriString exceeds 1023
        /// characters. -or- There is an invalid character sequence in uriString. -or- 
        /// The MS-DOS path specified in uriString must start with c:\\.
        /// </exception>
        /// <returns>
        /// A new instance of the <see cref="Uri"/> based on the specified <paramref name="uri"/>.
        /// </returns>
        public static Uri ToUri(this string uri)
        {
            return new Uri(uri);
        }

        /// <summary>
        /// encodes all the characters in the specified string into a sequence of bytes base on encoding.
        /// if encoding is null, the default value is <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="value">
        /// The string containing characters to encode.
        /// </param>
        /// <param name="encoding">
        /// The target encoding format. default value is <see cref="Encoding.UTF8"/>.
        /// </param>
        /// <returns>
        /// A byte array containing the results of encoding the specified set of characters.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The value of System.Text.Encoding is null.
        /// </exception>
        public static byte[] ToBytes(this string value, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding.GetBytes(value);
        }

        /// <summary>
        /// decodes all the bytes in the specified byte array into a string.
        /// if encoding is null, the default value is <see cref="Encoding.UTF8"/>.
        /// </summary>
        /// <param name="bytes">
        /// The byte array containing the sequence of bytes to decode.
        /// </param>
        /// <param name="encoding">
        /// The target encoding format. default value is <see cref="Encoding.UTF8"/>.
        /// </param>
        /// <returns>
        /// A string that contains the results of decoding the specified sequence of bytes.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The byte array contains invalid Unicode code points.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The bytes is null.
        /// </exception>
        public static string ToString(this byte[] bytes, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding.GetString(bytes);
        }

        /// <summary>
        /// Converts an array of 8-bit unsigned integers to its equivalent string representation
        /// that is encoded with base-64 digits.
        /// </summary>
        /// <param name="value">
        /// The string containing characters to encode.
        /// </param>
        /// <param name="encoding">
        /// The target encoding format. default value is <see cref="Encoding.UTF8"/>.
        /// </param>
        /// <returns>
        /// The string representation, in base-64 of the contents of inArray.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The value is null.
        /// </exception>
        public static string ToBase64(this string value, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return Convert.ToBase64String(value.ToBytes(encoding));
        }

        /// <summary>
        /// Converts the specified string, which encodes binary data as base-64 digits, to
        /// an equivalent 8-bit unsigned integer array.
        /// </summary>
        /// <param name="value">
        /// The string to convert.
        /// </param>
        /// <returns>
        /// An array of 8-bit unsigned integers that is equivalent to <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The value is null.
        /// </exception>
        /// <exception cref="FormatException">
        /// The length of <paramref name="value"/>, ignoring white-space characters, is not zero or a multiple of
        /// 4. -or- The format of s is invalid. <paramref name="value"/> contains a non-base-64 character, more
        /// than two padding characters, or a non-white space-character among the padding
        /// characters.
        /// </exception>
        public static byte[] FromBase64(this string value)
        {
            return Convert.FromBase64String(value);
        }

        /// <summary>
        /// Converts the digits of input to its equivalent string
        /// representation using the culture-specific format informations.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="sourceCultureName">
        /// A string that supplies culture-specific formatting name. It is not case-sensitive.
        /// </param>
        /// <param name="destinationCultureName">
        /// A string that supplies culture-specific formatting name. It is not case-sensitive.
        /// </param>
        /// <returns>
        /// A string representing numbers in the destination culture.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The name of source or destination culture is null.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        /// Culture is not supported -or- not found.
        /// </exception>
        public static string ToNativeDigits(this string value, string sourceCultureName, string destinationCultureName)
        {
            if (value is null)
            {
                return null;
            }

            var sourceCulture = CultureInfo.GetCultureInfo(sourceCultureName);
            var destinationCulture = CultureInfo.GetCultureInfo(destinationCultureName);

            for (int i = 0; i <= 9; i++)
            {
                value = value.Replace(sourceCulture.NumberFormat.NativeDigits[i], destinationCulture.NumberFormat.NativeDigits[i]);
            }

            return value;
        }

        #endregion;

        #region Object

        /// <summary>
        /// Returns an object of the specified type (<typeparamref name="T"/>) and 
        /// whose value is equivalent to the specified object.
        /// </summary>
        /// <typeparam name="T">
        /// Destination type.
        /// </typeparam>
        /// <param name="value">
        /// An object that implements the <see cref="IConvertible"/> interface.
        /// </param>
        /// <returns>
        /// An object whose type is conversionType and whose value is equivalent to value.
        /// -or- A null reference, if value is null and conversionType
        /// is not a value type.
        /// </returns>
        /// <exception cref="FormatException">
        /// value is not in a format recognized by conversionType.
        /// </exception>
        /// <exception cref="OverflowException">
        /// value represents a number that is out of the range of conversionType.
        /// </exception>
        /// <exception cref=" InvalidCastException">
        /// This conversion is not supported. -or- value is null and conversionType is a
        /// value type. -or- value does not implement the <see cref="IConvertible"/> interface.
        /// </exception>
        public static T ChangeType<T>(this object value)
        {
            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            return (T)Convert.ChangeType(value, conversionType);
        }

        #endregion;

        #region Numeric

        /// <summary>
        /// Convert a number to a string representation a currency amount using 
        /// the culture-specific format information.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="value">
        /// </param>
        /// <param name="culture">
        /// A string that supplies culture-specific formatting name.
        /// </param>
        /// <returns>
        /// A string representation of input number as specified by currency format and culture.
        /// </returns>
        /// <exception cref="CultureNotFoundException">
        /// Culture is not supported -or- not found.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// value -or- culture is null.
        /// </exception>
        public static string ToCurrency<T>(this T value, string culture = "en-us") where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            return value.ToString("C0", new CultureInfo(culture));
        }

        /// <summary>
        /// Convert a number to a string representation a numeric using the 
        /// culture-specific format information.
        /// Like 5,603,245.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="value">
        /// </param>
        /// <param name="culture">
        /// A string that supplies culture-specific formatting name.
        /// </param>
        /// <returns>
        /// A string representation of input number as specified by numeric format and culture.
        /// </returns>
        /// <exception cref="CultureNotFoundException">
        /// Culture is not supported -or- not found.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Value -or- culture is null.
        /// </exception>
        public static string ToNumeric<T>(this T value, string culture = "en-us") where T : struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable
        {
            return value.ToString("N0", new CultureInfo(culture));
        }

        #endregion;

        #region Enum

        /// <summary>
        /// Creates a dictionary from an enum, which the key is the number 
        /// assigned to each item and value is <see cref="DescriptionAttribute.Description"/>,
        /// if its defined; otherwise its the name of item.
        /// </summary>
        /// <param name="value">
        /// An enum to convert.
        /// </param>
        /// <returns>
        /// A dictionary created from an enum which the key is the number assigned to each item and value 
        /// is <see cref="DescriptionAttribute.Description"/>, if its defined; otherwise its the name of item.
        /// </returns>
        public static Dictionary<int, string> ToDictionary(this Enum value)
        {
            return Enum
                .GetValues(value.GetType())
                .Cast<Enum>()
                .ToDictionary(e => Convert.ToInt32(e), e => e.GetDescription());
        }

        #endregion;
    }
}
