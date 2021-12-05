using System;
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Loby.Extensions
{
    public static class StringExtensions 
    {
        #region Convert

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

            return Convert.ToBase64String(ToBytes(value, encoding));
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

        #region Value

        /// <summary>
        /// Gets a value indicating whether the current string object has a valid value.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// true if the current string object has a value; false if has no value.
        /// </returns>
        public static bool HasValue(this string value)
        {
            return !IsNullOrEmptyOrWhiteSpace(value);
        }

        /// <summary>
        /// Indicates whether the specified string is null or an empty or white-space string.
        /// </summary>
        /// <param name="value">
        /// The string to test.
        /// </param>
        /// <returns>
        /// true if the value parameter is null or empty or white-space string; otherwise, false.
        /// </returns>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Indicates whether the specified string is categorized as a numeric.
        /// digit.
        /// </summary>
        /// <param name="value">
        /// The string to test.
        /// </param>
        /// <returns>
        /// true if the current string object is numeric; otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value is null.
        /// </exception>
        public static bool IsNumeric(this string value)
        {
            if (Regex.IsMatch(value, @"([-+]?[0-9]*\.?[0-9]+)"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Return all digits from specified string.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// All digits that extracted from specified string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value is null.
        /// </exception>
        public static string GetDigits(this string value)
        {
            return Regex.Replace(value, "[^0-9]", string.Empty);
        }

        /// <summary>
        /// Return all letters from specified string.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// All letters that extracted from specified string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value is null.
        /// </exception>
        public static string GetLetters(this string value)
        {
            return Regex.Replace(value, "[^a-zA-Z]", string.Empty);
        }

        /// <summary>
        /// Remove all leading and trailing white spaces and convert multiple white spaces 
        /// to one single white space inside this string instance.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// The string that remains after converting multiple white spaces to one and removing all 
        /// instances of the trimChar character from the start and end of the current string.
        /// If no characters can be trimmed from the current instance, the method returns the 
        /// current instance unchanged.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value is null.
        /// </exception>
        public static string TrimAndReduce(this string value)
        {
            return Regex.Replace(value, @"\s+", " ").Trim();
        }

        /// <summary>
        /// Returns a new instance of <see cref="string"/> that is consist of characters from right of specified length.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="length">
        /// The number of characters to return.
        /// </param>
        /// <returns>
        /// Returns a new instance of <see cref="string"/> that is consist of characters from right of specified length.
        /// if lenght is greater than this string instance, returns the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// length indicates a position not within this instance. -or- length is less than zero.
        /// </exception>
        public static string Right(this string value, int length)
        {
            return value.Length > length ? value.Substring(value.Length - length) : value;
        }

        /// <summary>
        /// Returns a new instance of System.String that is consist of characters from left of specified length.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="length">
        /// The number of characters to return.
        /// </param>
        /// <returns>
        /// Returns a new instance of System.String that is consist of characters from left of specified length.
        /// if lenght is greater than this string instance, returns the string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// length indicates a position not within this instance. -or- length is less than zero.
        /// </exception>
        public static string Left(this string value, int length)
        {
            return value.Length > length ? value.Substring(0, length) : value;
        }

        #endregion;

        #region Format

        /// <summary>
        /// Formats the current string according to the specified mask.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="mask">
        /// The format string. Like "####-##-##-T-##:##:##".
        /// </param>
        /// <returns>
        /// A string representation of value of the current System.String object as specified
        /// by format.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- mask is null.
        /// </exception>
        public static string Mask(this string value, string mask, char maskCharacter = '#')
        {
            if (value.IsNull())
            {
                throw new ArgumentNullException("value can not be null, empty or white-space.");
            }

            if (mask.IsNull())
            {
                throw new ArgumentNullException("mask can not be null, empty or white-space.");
            }

            var index = 0;
            var output = string.Empty;

            foreach (var ch in mask)
            {
                if (ch == maskCharacter && index++ < value.Length)
                {
                    output += value[index];
                }
                else
                {
                    output += ch;
                }
            }

            return output;
        }

        #endregion;

        #region Seek

        /// <summary>
        /// Returns a value indicating whether a specified string array occurs within this string as substring.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="values">
        /// The string array to seek.
        /// </param>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies the rules to 
        /// use in the comparison.
        /// </param>
        /// <returns>
        /// true if the string array parameter occurs as substring within this string. otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- values is null.
        /// </exception>
        public static bool Contains(this string value, string[] values, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            foreach (var item in values)
            {
                if (value.IndexOf(item, comparisonType) >= 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a value indicating whether a specified char array occurs within this string as substring.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="values">
        /// The char array to seek.
        /// </param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules to 
        /// use in the comparison.
        /// </param>
        /// <returns>
        /// true if the char array parameter occurs as substring within this string. otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- values is null.
        /// </exception>
        public static bool Contains(this string value, char[] values, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            foreach (var item in values)
            {
                if (value.IndexOf(item.ToString(), comparisonType) >= 0)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the end of this string instance matches at least one of the specified string array element
        /// when compared using the specified comparison option.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="values">
        /// The string array to compare to the substring at the end of this instance.
        /// </param>
        /// <param name="comparisonType">
        /// One of the enumeration values that determines how this string and value are compared.
        /// </param>
        /// <returns>
        /// true if the values parameter matches the end of this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- values is null.
        /// </exception>
        public static bool EndsWith(this string value, string[] values, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            foreach (var item in values)
            {
                if (value.EndsWith(item, comparisonType))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the end of this string instance matches at least one of the 
        /// specified char array element when compared using the specified comparison option.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="values">
        /// The char array to compare to the substring at the end of this instance.
        /// </param>
        /// <param name="comparisonType">
        /// One of the enumeration values that determines how this string and value are compared.
        /// </param>
        /// <returns>
        /// true if the values parameter matches the end of this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- values is null.
        /// </exception>
        public static bool EndsWith(this string value, char[] values, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            foreach (var item in values)
            {
                if (value.EndsWith(item.ToString(), comparisonType))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the start of this string instance matches at least one of 
        /// the specified string array element when compared using the specified comparison option.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="values">
        /// The string array to compare to the substring at the start of this instance.
        /// </param>
        /// <param name="comparisonType">
        /// One of the enumeration values that determines how this string and value are compared.
        /// </param>
        /// <returns>
        /// true if the values parameter matches the start of this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- values is null.
        /// </exception>
        public static bool StartsWith(this string value, string[] values, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            foreach (var item in values)
            {
                if (value.StartsWith(item, comparisonType))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the start of this string instance matches at least one of 
        /// the specified char array element when compared using the specified comparison option.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="values">
        /// The char array to compare to the substring at the start of this instance.
        /// </param>
        /// <param name="comparisonType">
        /// One of the enumeration values that determines how this string and value are compared.
        /// </param>
        /// <returns>
        /// true if the values parameter matches the start of this string; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- values is null.
        /// </exception>
        public static bool StartsWith(this string value, char[] values, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            foreach (var item in values)
            {
                if (value.StartsWith(item.ToString()))
                    return true;
            }

            return false;
        }

        #endregion;

        #region Utils

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string array in the current
        /// instance are replaced with ("").
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="withouts">
        /// The string array to replace all occurrences of it with ("").
        /// </param>
        /// <returns>
        /// A string that is equivalent to the current string except that all instances of
        /// string array are replaced with (""). If string array elements are not found in the current
        /// instance, the method returns the current instance unchanged.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- withouts is null or empty.
        /// </exception>
        public static string Without(this string value, params string[] withouts)
        {
            foreach (var item in withouts)
            {
                value = value.Replace(item, string.Empty);
            }

            return value;
        }

        /// <summary>
        /// Returns a string in which all occurrences of a specified keys in the pairs, 
        /// has been replaced with its key value.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <param name="pairs">
        /// A key-value pairs that are going to be replaced in current string.
        /// </param>
        /// <returns>
        /// A System.String object that all pairs has been replaced in current string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// value -or- withouts is null.
        /// </exception>
        public static string Replace(this string value, Dictionary<string, string> pairs)
        {
            foreach (var item in pairs)
            {
                value = value.Replace(item.Key, item.Value);
            }

            return value;
        }

        /// <summary>
        /// Returns the extension (including the period ".") of the specified string.
        /// </summary>
        /// <param name="value">
        /// The string from which to get the extension.
        /// </param>
        /// <returns>
        /// The extension of the specified string (including the period ".").
        /// If string is null, returns null and If string does not have any extension, returns System.String.Empty.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// string contains one or more of the invalid characters.
        /// </exception>
        public static string GetExtension(this string value)
        {
            return Path.GetExtension(value);
        }

        #endregion;
    }
}
