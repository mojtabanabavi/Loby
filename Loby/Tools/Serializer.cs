using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Loby
{
    /// <summary>
    /// Provides functionality to serialize objects or value types to JSON, XML and to deserialize
    /// JSON, XML into objects or value types.
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// Converts the specified value into a JSON string.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// A JSON string representation of the value.
        /// </returns>
        public static string ToJson(object value)
        {
            var serializedValue = JsonSerializer.Serialize(value);

            return serializedValue;
        }

        /// <summary>
        /// Converts the text representing a single JSON value into an instance of the type
        /// specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="Type">
        /// The target type of the JSON value.
        /// </typeparam>
        /// <param name="value">
        /// The JSON string to parse.
        /// </param>
        /// <returns>
        /// A <typeparamref name="Type"/> representation of the JSON value.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// json is null.
        /// </exception>
        /// <exception cref="JsonException">
        /// The JSON is invalid. -or- Type is not compatible with the JSON. -or- There
        /// is remaining data in the string beyond a single JSON value.
        /// </exception>
        public static Type FromJson<Type>(string json)
        {
            var deserializedValue = JsonSerializer.Deserialize<Type>(json);

            return deserializedValue;
        }

        /// <summary>
        /// Converts the specified value into a XML string.
        /// </summary>
        /// <param name="value">
        /// The value to convert.
        /// </param>
        /// <returns>
        /// A XML string representation of the value.
        /// </returns>
        public static string ToXml(object value)
        {
            using var stream = new StringWriter();

            var serializer = new XmlSerializer(value.GetType());

            serializer.Serialize(stream, value);

            return stream.ToString();
        }

        /// <summary>
        /// Converts the text representing a single XML value into an instance of the type
        /// specified by a generic type parameter.
        /// </summary>
        /// <typeparam name="Type">
        /// The target type of the XML value.
        /// </typeparam>
        /// <param name="value">
        /// The XML string to parse.
        /// </param>
        /// <returns>
        /// A <typeparamref name="Type"/> representation of the XML value.
        /// </returns>
        public static Type FromXml<Type>(string value)
        {
            using var stream = new StringReader(value);

            var serializer = new XmlSerializer(typeof(Type));

            return serializer.Deserialize(stream).ChangeType<Type>();
        }
    }
}
