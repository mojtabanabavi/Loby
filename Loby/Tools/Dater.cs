using System;
using System.Linq;
using Loby.Extensions;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Loby.Tools
{
    /// <summary>
    /// A set of methods for date conversions along with other practical methods.
    /// </summary>
    public class Dater
    {
        #region Convert

        /// <summary>
        /// Converts the value of the current <see cref="DateTime"/> object to its equivalent string
        /// representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="dateTime">
        /// </param>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <param name="culture">
        /// A string that supplies culture-specific formatting name.
        /// </param>
        /// <returns>
        /// A string representation of value of the current <see cref="DateTime"/> object as specified
        /// by <paramref name="format"/> and <paramref name="culture"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported.
        /// </exception>
        /// <exception cref="FormatException">
        /// The format parameter is not recognized or not supported.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// culture is null.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        /// Culture is not supported -or- not found.
        /// </exception>
        public static string ToSolar(DateTime dateTime, string format = "yyyy/MM/dd", string culture = "en-us")
        {
            return dateTime.ToString(format, new CultureInfo(culture));
        }

        /// <summary>
        /// Converts the string representation of a solar date and time to its 
        /// <see cref="DateTime"/> equivalent by using culture-specific format information.
        /// </summary>
        /// <param name="dateTime">
        /// A string that contains a solar date and time to convert.
        /// </param>
        /// <param name="culture">
        /// A string that supplies culture-specific formatting name.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> object that is represent gregorian date and time.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// dateTime -or- culture is null.
        /// </exception>
        /// <exception cref="CultureNotFoundException">
        /// Culture is not supported -or- not found.
        /// </exception>
        public static DateTime FromSolar(string dateTime, string culture = "en-us")
        {
            dateTime = dateTime.ToNativeDigits(culture, "en-us");

            return DateTime.Parse(dateTime, new CultureInfo(culture));
        }

        /// <summary>
        /// Convert a <see cref="DateTime"/> object to iranian solar date and time 
        /// according to its equivalent string representation by using the specified format. 
        /// </summary>
        /// <param name="dateTime">
        /// A string that contains a iranian solar date and time to convert.
        /// </param>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <returns>
        /// The string representation of the iranian solar date and time in the format
        /// specified by the <paramref name="format"/> parameter.
        /// </returns>
        /// <exception cref="FormatException">
        /// The format parameter is not recognized or not supported.
        /// </exception>
        public static string ToIranSolar(DateTime dateTime, string format = "yyyy/MM/dd")
        {
            return ToSolar(dateTime, format, "fa-ir");
        }

        /// <summary>
        /// Convert a <see cref="DateTime"/> object to afghanistanian solar date and time 
        /// according to its equivalent string representation by using the specified format. 
        /// </summary>
        /// <param name="dateTime">
        /// A string that contains a afghanistanian solar date and time to convert.
        /// </param>
        /// <param name="format">
        /// A standard or custom date and time format string.
        /// </param>
        /// <returns>
        /// The string representation of the afghanistanian solar date and time in the format
        /// specified by the <paramref name="format"/> parameter.
        /// </returns>
        /// <exception cref="FormatException">
        /// The format parameter is not recognized or not supported.
        /// </exception>
        public static string ToAfghanistanSolar(DateTime dateTime, string format = "yyyy/MM/dd")
        {
            return ToSolar(dateTime, format, "fa-af");
        }

        /// <summary>
        /// Converts the string representation of an iranian solar date and time to its 
        /// <see cref="DateTime"/> equivalent.
        /// </summary>
        /// <param name="dateTime">
        /// A string that contains an iranian solar date and time to convert.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> object that is represent gregorian date and time.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// dateTime is null.
        /// </exception>
        public static DateTime FromIranSolar(string dateTime)
        {
            return FromSolar(dateTime, "fa-ir");
        }

        /// <summary>
        /// Converts the string representation of an afghanistanian solar date and time to its 
        /// <see cref="DateTime"/> equivalent.
        /// </summary>
        /// <param name="dateTime">
        /// A string that contains an afghanistanian solar date and time to convert.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> object that is represent gregorian date and time.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// dateTime is null.
        /// </exception>
        public static DateTime FromAfghanistanSolar(string dateTime)
        {
            return FromSolar(dateTime, "fa-af");
        }

        /// <summary>
        /// Converts the value of the current <see cref="TimeSpan"/> object to its equivalent string
        /// representation by using the specified format.
        /// </summary>
        /// <param name="time">
        /// </param>
        /// <param name="format">
        /// A standard or custom TimeSpan format string.
        /// </param>
        /// <returns>
        /// The string representation of the current <see cref="TimeSpan"/> value in the format
        /// specified by the <paramref name="format"/> parameter.
        /// </returns>
        /// <exception cref="FormatException">
        /// The format parameter is not recognized or not supported.
        /// </exception>
        public static string Format(TimeSpan time, string format = "hh:mm:ss")
        {
            return time.ToString(format?.Replace(":", "\\:"));
        }

        /// <summary>
        /// Converts the value of the current <see cref="DateTime"/> object to Unix TimeStamp.
        /// </summary>
        /// <param name="dateTime">
        /// </param>
        /// <returns>
        /// The number of seconds since Jan 1, 1970.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported.
        /// </exception>
        public static double ToUnixTimeStamp(DateTime dateTime)
        {
            return dateTime.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }

        /// <summary>
        /// Converts the value of the current <see cref="long"/> object that 
        /// represent Unix TimeStamp to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="unixTimeSeconds">
        /// The number of seconds from Jan 1, 1970.
        /// </param>
        /// <returns>
        /// A <see cref="DateTime"/> object that It consists of Jan 1, 1970 + <paramref name="unixTimeSeconds"/>.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported.
        /// </exception>
        public static DateTime ToDateTime(long unixTimeSeconds)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeSeconds);
        }

        /// <summary>
        /// Converts the value of the current System.String object to <see cref="TimeSpan"/>.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// A <see cref="TimeSpan"/> object that is extracted from <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The value of System.String object is null.
        /// </exception>
        /// <exception cref="FormatException">
        /// The value of System.String object is not in correct format (00:00:00).
        /// </exception>
        public static TimeSpan ToTimeSpan(string value)
        {
            value = NormalizeDateTime(value, ':');

            if (!Regex.Match(value, @"\d{2}:\d{2}:\d{2}").Success)
                throw new FormatException("Value must be in 00:00:00 format.");

            var splited = value.Split(':');

            return new TimeSpan(Convert.ToInt32(splited[0]), Convert.ToInt32(splited[1]), Convert.ToInt32(splited[2]));
        }

        #endregion;

        #region Utils

        /// <summary>
        /// Returns a new string whose textual value is the same as this string, but whose 
        /// separators like dash replaced with <paramref name="separator"/>.
        /// </summary>
        /// <param name="value">
        /// A <see cref="string"/> that represent a <see cref="DateTime"/>.
        /// </param>
        /// <param name="separator">
        /// A character that is going to be replaced with separators.
        /// </param>
        /// <returns>
        /// A new, normalized string, that separators like dash is replaced with <paramref name="separator"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The value of System.String objects is null.
        /// </exception>
        private static string NormalizeDateTime(string value, char separator = '/')
        {
            return value
                .Trim()
                .Replace('\\', separator)
                .Replace('-', separator)
                .Replace('_', separator)
                .Replace(',', separator)
                .Replace('.', separator)
                .Replace(' ', separator)
                .Replace(':', separator);
        }

        /// <summary>
        /// Returns the first day of month based on <paramref name="date"/>.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>
        /// A new instance of System.DateTime that represent first day of month.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported.
        /// </exception>
        public static DateTime FirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Returns the last day of month based on <paramref name="date"/>.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>
        /// A new instance of System.DateTime that represent last day of month.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The date and time is outside the range of dates supported.
        /// </exception>
        public static DateTime LastDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        /// <summary>
        /// Returns a list of System.DateTime that are missing between start and end date.
        /// </summary>
        /// <param name="availableDates">
        /// All dates that we are going to find missing dates among them.
        /// </param>
        /// <param name="startingDate">
        /// A System.DateTime that is the beging of range.
        /// </param>
        /// <param name="endingDate">
        /// A System.DateTime that is the ending of range.</param>
        /// <returns>
        /// a list of System.DateTime that are missing between start and end date.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The list is null.
        /// </exception>
        public static List<DateTime> FindMissingDates(IEnumerable<DateTime> availableDates, DateTime startingDate, DateTime endingDate)
        {
            availableDates = availableDates.Distinct();

            var totalDates = Enumerable
                .Range(0, (int)(endingDate - startingDate).TotalDays + 1)
                .Select(i => startingDate.AddDays(i))
                .Except(availableDates);

            var missingDates = totalDates.Where(i => !availableDates.Contains(i.Date)).ToList();

            return missingDates;
        }

        #endregion;
    }
}
