using System;
using System.Collections.Generic;
using System.Text;

namespace Loby.Extensions
{
    internal class ByteExtensions
    {
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
        public static string ToString(byte[] bytes, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return encoding.GetString(bytes);
        }
    }
}
