namespace Loby.Core
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
    }
}
