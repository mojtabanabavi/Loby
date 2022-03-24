using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace Loby.Extensions
{
    public static class ReflectionExtensions
    {
        #region check

        /// <summary>
        /// Determines whether any attribute of type <paramref name="attributeType"/>
        /// is applied to a member of a type.
        /// </summary>
        /// <param name="element">
        /// An object derived from the <see cref="MemberInfo"/> class that describes
        /// a constructor, event, field, method, type, or property member of a class.
        /// </param>
        /// <param name="attributeType">
        /// The type, or a base type, of the custom attribute to search for.
        /// </param>
        /// <param name="inherit">
        /// If true, specifies to also search the ancestors of element for custom attributes.
        /// </param>
        /// <returns>
        /// true if an attribute of type <paramref name="attributeType"/> is applied to 
        /// <paramref name="element"/>; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// element -or- attributeType is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// attributeType is not derived from <see cref="Attribute"/>.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// element is not a constructor, method, property, event, type, or field.
        /// </exception>
        public static bool HasAttribute(this MemberInfo element, Type attributeType, bool inherit = false)
        {
            return Attribute.IsDefined(element, attributeType, inherit);
        }

        /// <summary>
        /// Determines whether any attribute of type <typeparamref name="T"/>
        /// is applied to a member of a type.
        /// </summary>
        /// <param name="element">
        /// An object derived from the <see cref="MemberInfo"/> class that describes
        /// a constructor, event, field, method, type, or property member of a class.
        /// </param>
        /// <typeparam name="T">
        /// The type, or a base type, of the custom attribute to search for.
        /// </typeparam>
        /// <param name="inherit">
        /// If true, specifies to also search the ancestors of element for custom 
        /// attributes.
        /// </param>
        /// <returns>
        /// true if an attribute of type <typeparamref name="T"/> is applied to 
        /// <paramref name="element"/>; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// element is null.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// element is not a constructor, method, property, event, type, or field.
        /// </exception>
        public static bool HasAttribute<T>(this MemberInfo element, bool inherit = false) where T : Attribute
        {
            return HasAttribute(element, typeof(T), inherit);
        }

        /// <summary>
        /// Determines whether an instance of a specified type can be assigned
        /// to an instance of the current type.
        /// </summary>
        /// <param name="element">
        /// The type to check inheritance.
        /// </param>
        /// <param name="type">
        /// The type to compare with the current type.
        /// </param>
        /// <returns>
        /// true if any of the following conditions is true: 
        /// type and the current instance represent the same type. 
        /// type is derived either directly or indirectly from the current instance.
        /// type is derived directly from the current instance if it inherits from 
        /// the current instance; 
        /// type is derived indirectly from the current instance if it inherits from 
        /// a succession of one or more classes that inherit from the current instance. 
        /// The current instance is an interface that type implements. 
        /// type is a generic type parameter, and the current instance represents one 
        /// of the constraints of type.
        /// </returns>
        public static bool IsInheritFrom(this Type element, Type type)
        {
            return type.IsAssignableFrom(element);
        }

        /// <summary>
        /// Determines whether an instance of a specified type can be assigned
        /// to an instance of the current type.
        /// </summary>
        /// <param name="element">
        /// The type to check inheritance.
        /// </param>
        /// <typeparam name="T">
        /// The type to compare with the current type.
        /// </typeparam>
        /// <returns>
        /// true if any of the following conditions is true: 
        /// type and the current instance represent the same type. 
        /// type is derived either directly or indirectly from the current instance.
        /// type is derived directly from the current instance if it inherits from 
        /// the current instance; 
        /// type is derived indirectly from the current instance if it inherits from 
        /// a succession of one or more classes that inherit from the current instance. 
        /// The current instance is an interface that type implements. 
        /// type is a generic type parameter, and the current instance represents one 
        /// of the constraints of type.
        /// </returns>
        public static bool IsInheritFrom<T>(this Type element)
        {
            return IsInheritFrom(element, typeof(T));
        }

        #endregion;
    }
}