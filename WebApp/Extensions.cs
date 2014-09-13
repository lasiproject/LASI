using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LASI.WebApp
{
    public static class Extensions
    {
        /// <summary>
        /// Returns a new string in which all characters that are known to be problematic for jQuery's Sizzle Selector 
        /// engine have been replaced with a single distinct character, delimited by an underscore on either side.
        /// </summary>
        /// <param name="value">The string to transform.</param>
        /// <returns>A new string in which all characters that are known to be problematic for jQuery's Sizzle Selector engine are replaced with underscores.
        /// </returns>
        /// <remarks>
        /// The extra underscores which pad the replacement are costly but allow for the name to remain legible.
        /// Note that a space is replaced by a single underscore.
        /// </remarks>
        public static string ToSafeHtmlDomId(this string value) {

            return string.Join("", value.Select(c => tokenLookup[c].DefaultIfEmpty(c.ToString()).First()));
        }
        private static readonly ILookup<char, string> tokenLookup = new Dictionary<char, string>
        {
            { ' ', "_" },
            { '(', "_leftp_" },
            { ')', "_rightp_" },
            { '[', "_leftb_" },
            { ']', "_rightb_" },
            { '{', "_leftc_" },
            { '}', "_rightc_" },
            { '+', "_plus_" },
            { '*', "_asterisk_" },
            { '/', "_fslash_" },
            { '\\',"_bslash_" }
        }.ToLookup(pair => pair.Key, pair => pair.Value);

        public static int CreateNewUniqueId() {
            return System.Threading.Interlocked.Increment(ref idGenerator);
        }
        private static int idGenerator;
    }
}