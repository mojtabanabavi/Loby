using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Collections.Generic;

namespace Loby
{
    /// <summary>
    /// Extensions to get the user claim values from identity.
    /// </summary>
    public static class IdentityExtensions
    {
        #region Base

        /// <summary>
        /// Return the first claim value with the specified <paramref name="claimType"/> 
        /// if it exists, null otherwise.
        /// </summary>
        /// <param name="identity">
        /// An implementation of <see cref="IIdentity"/>.
        /// </param>
        /// <param name="claimType">
        /// A string that indicates the type of claim.
        /// </param>
        /// <returns>
        /// The string that represents the first claim value with the specified 
        /// <paramref name="claimType"/> if it exists, null otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// identity is null.
        /// </exception>
        public static string GetClaimValue(this IIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                throw new ArgumentNullException(nameof(claimsIdentity));
            }

            return claimsIdentity.FindFirst(claimType)?.Value;
        }

        /// <summary>
        /// Return the first claim value with the specified <paramref name="claimType"/> as 
        /// (<typeparamref name="T"/>) if it exists, default otherwise.
        /// </summary>
        /// <typeparam name="T">
        /// The type of output object.
        /// </typeparam>
        /// <param name="identity">
        /// An implementation of <see cref="IIdentity"/>.
        /// </param>
        /// <param name="claimType">
        /// A string that indicates the type of claim.
        /// </param>
        /// <returns>
        /// An object of type (<typeparamref name="T"/>) that represents the first claim value 
        /// with the specified <paramref name="claimType"/>.
        /// if it exists, default otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// identity is null.
        /// </exception>
        public static T GetClaimValue<T>(this IIdentity identity, string claimType)
        {
            var claimValue = GetClaimValue(identity, claimType);

            if (claimValue != null)
            {
                return claimValue.As<T>();
            }

            return default(T);
        }

        /// <summary>
        /// Return the all claim values with the specified <paramref name="claimType"/>
        /// if it exists, null otherwise.
        /// </summary>
        /// <param name="identity">
        /// An implementation of <see cref="IIdentity"/>.
        /// </param>
        /// <param name="claimType">
        /// A string that indicates the type of claim.
        /// </param>
        /// <returns>
        /// A collection of all claim values with the specified <paramref name="claimType"/>
        /// if it exists, null otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// identity is null.
        /// </exception>
        public static IEnumerable<string> GetClaimValues(this IIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            var claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null)
            {
                throw new ArgumentNullException(nameof(claimsIdentity));
            }

            return claimsIdentity.FindAll(claimType)?.Select(c => c.Value);
        }

        #endregion;

        #region User Id

        /// <summary>
        /// Return the user id using the <see cref="ClaimTypes.NameIdentifier"/> 
        /// type if it exists, null otherwise.
        /// </summary>
        /// <param name="identity">
        /// An implementation of <see cref="IIdentity"/>.
        /// </param>
        /// <returns>
        /// A string representing user id if it exists, null otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// identity is null.
        /// </exception>
        public static string GetUserId(this IIdentity identity)
        {
            return identity.GetClaimValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Return the user id using the <see cref="ClaimTypes.NameIdentifier"/> 
        /// type as (<typeparamref name="T"/>) if it exists, default otherwise.
        /// </summary>
        /// <param name="identity">
        /// An implementation of <see cref="IIdentity"/>.
        /// </param>
        /// <returns>
        /// An object of type (<typeparamref name="T"/>) representing user id if it exists, 
        /// default otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// identity is null.
        /// </exception>
        public static T GetUserId<T>(this IIdentity identity)
        {
            return identity.GetClaimValue<T>(ClaimTypes.NameIdentifier);
        }

        #endregion;
    }
}
