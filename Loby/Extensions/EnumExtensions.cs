using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

namespace Loby.Extensions
{
    public static class EnumExtensions
    {
        #region Convert

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

        #region Utils

        /// <summary>
        /// Returns description value of <see cref="DescriptionAttribute"/> 
        /// that is used on current object.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// The value of <see cref="DescriptionAttribute.Description"/> that is used on current object.
        /// Returns null, if current object doest have any <see cref="DescriptionAttribute"/>.
        /// </returns>
        public static string GetDescription(this Enum value)
        {
            var attribute = value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes<DescriptionAttribute>(false)
                .FirstOrDefault();

            return attribute?.Description;
        }

        /// <summary>
        /// Gets the total number of elements in an enum.
        /// </summary>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// The total number of elements in an enum; zero (0)
        /// if there are no elements.
        /// </returns>
        public static int Count(this Enum value)
        {
            return Enum.GetNames(value.GetType()).Length;
        }

        #endregion;
    }
}
