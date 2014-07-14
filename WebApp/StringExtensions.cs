using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LASI.WebApp
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which all characters which are known to be problematic for jQuery's Sizzle Selector 
        /// engine have been replaced with a single distinct character, delimited by an underscore on either side.
        /// </summary>
        /// <param name="value">The string to transform.</param>
        /// <returns>A new string in which all characters which are known to be problematic for jQuery's Sizzle Selector engine are replaced with underscores.
        /// </returns>
        /// <remarks>
        /// The extra underscores which pad the replacement are costly but allow for the name to remain legible.
        /// Note that a space is replaced by a single underscore.
        /// </remarks>
        public static string ToSizzleSafeString(this string value) {
            return value.Replace(' ', '_')
                        .Replace("(", "_a_")
                        .Replace(")", "_b_")
                        .Replace("[", "_c_")
                        .Replace("]", "_d_")
                        .Replace("{", "_e_")
                        .Replace("}", "_f_")
                        .Replace("+", "_g_");
        }
    }
}