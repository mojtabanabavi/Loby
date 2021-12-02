using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace Loby.Extensions
{
    public static class EnumExtensions
    {
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
