using System;

namespace Loby.Extensions
{
    public static class ObjectExtensions
    {
        #region Value

        /// <summary>
        /// Indicates whether the specified object is null.
        /// </summary>
        /// <param name="value">
        /// The object to test.
        /// </param>
        /// <returns>
        /// true if the object parameter is null; otherwise, false.
        /// </returns>
        public static bool IsNull(this object value)
        {
            return value == null;
        }

        #endregion;

        #region Convert

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
        public static T As<T>(this object value)
        {
            Type conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);

            return (T)Convert.ChangeType(value, conversionType);
        }

        #endregion;
    }
}
