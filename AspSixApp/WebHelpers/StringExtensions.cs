using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Utilities;

namespace AspSixApp.WebHelpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which all characters that are known to be problematic when used as Ids for DOM Query Selector engines,
        /// e.g. jQuery's Sizzle Selector engine, are replaced with appropriate aliases.
        /// </summary>
        /// <param name="value">The string to transform.</param>
        /// <returns>A new string in which all characters that are known to be problematic when used as Ids for DOM Query Selector engines,
        /// e.g. jQuery's Sizzle Selector, engine are replaced with appropriate aliases.
        /// </returns>
        /// <remarks>
        /// The extra underscores which pad the replacement are costly but allow for the name to remain legible.
        /// Note that a space is replaced by a single underscore.
        /// </remarks>
        /// <example>
        /// <code>
        /// var arbitrary = "A Treatise on [Name Mangling Schemes] (ex C++) from http://isocpp.org";
        /// var safeIdString = arbitrary.ToSafeHtmlDomId();
        /// Assert.AreEqual(
        /// safeIdString, 
        /// "A_Treatise_on__leftb_Name_Mangling_Schemes_rightb___leftp_ex_C_plus__plus__rightp__from_http_colon__fslash__fslash_isocpp_period_org");
        /// </code>
        /// </example>
        public static string ToSafeHtmlDomId(this string value) {

            return string.Join("", value.Select(c => tokenMap.GetValueOrDefault(c, c.ToString())));
        }
        private static IDictionary<char, string> tokenMap = new Dictionary<char, string>
        {
            [' '] = "_",
            ['.'] = "_period_",
            [':'] = "_colon_",
            ['('] = "_leftp_",
            [')'] = "_rightp_",
            ['['] = "_leftb_",
            [']'] = "_rightb_",
            ['{'] = "_leftc_",
            ['}'] = "_rightc_",
            ['+'] = "_plus_",
            ['-'] = "_minus_",
            ['*'] = "_asterisk_",
            ['/'] = "_fslash_",
            ['\\'] = "_bslash_"
        };

        public static int CreateUniqueId() {
            return idGenerator = System.Threading.Interlocked.Increment(ref idGenerator);
        }
        private static int idGenerator;
    }
}