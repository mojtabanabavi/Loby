using System.Linq;
using System.Text.RegularExpressions;

namespace Loby.Tools
{
    /// <summary>
    /// Includes a set of practical methods for validation.
    /// </summary>
    public class Validator
    {
        #region Emails

        /// <summary>
        /// Indicates whether the specified string represent a valid 
        /// email address or not.
        /// </summary>
        /// <param name="value">
        /// An string to test.
        /// </param>
        /// <returns>
        /// true if the value parameter is a valid email address; otherwise, false.
        /// </returns>
        public static bool IsValidEmail(string value)
        {
            var pattern = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";

            return Regex.IsMatch(value, pattern);
        }

        #endregion;

        #region Urls

        /// <summary>
        /// Indicates whether the specified string represent a valid url or not.
        /// </summary>
        /// <param name="value">
        /// An string to test.
        /// </param>
        /// <returns>
        /// true if the value parameter is a valid url; otherwise, false.
        /// </returns>
        public static bool IsValidUrl(string value)
        {
            var pattern = @"^(https?|ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$";

            return Regex.IsMatch(value, pattern);
        }

        /// <summary>
        /// Indicates whether the specified string represent a valid 
        /// credit card number or not.
        /// </summary>
        /// <param name="value">
        /// An string to test. every four digits can be separated by " " or "-".
        /// </param>
        /// <returns>
        /// true if the value parameter is a valid credit card number; 
        /// otherwise, false.
        /// </returns>
        public static bool IsValidCreditCard(string value)
        {
            value = value
                .Replace("-", "")
                .Replace(" ", "");

            int checksum = 0;
            bool evenDigit = false;

            foreach (char digit in value.Reverse())
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);

                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;

                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }

        /// <summary>
        /// Indicates whether the specified string represent a valid number or not.
        /// </summary>
        /// <param name="value">
        /// An string to test.
        /// </param>
        /// <returns>
        /// true if the value parameter is a valid number; otherwise, false.
        /// </returns>
        public static bool IsValidNumber(string value)
        {
            var pattern = @"^[-+]?[0-9]*\.?[0-9]+$";

            return Regex.IsMatch(value, pattern);
        }

        #endregion;
    }
}
