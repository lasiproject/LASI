using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LASI.Utilities;
using Interlocked = System.Threading.Interlocked;
namespace LASI.WebApp.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns a new string in which all characters that are known to be problematic when used
        /// as Ids for DOM Query Selector engines, e.g. jQuery's Sizzle Selector engine, are
        /// replaced with appropriate aliases.
        /// </summary>
        /// <param name="value">The string to transform.</param>
        /// <returns>
        /// A new string in which all characters that are known to be problematic when used as Ids
        /// for DOM Query Selector engines, e.g. jQuery's Sizzle Selector, engine are replaced with
        /// appropriate aliases.
        /// </returns>
        /// <remarks>
        /// The extra underscores which pad the replacement are costly but allow for the name to
        /// remain legible. Note that a space is replaced by a single underscore.
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
        public static string ToSafeHtmlDomId(this string value) =>
            value.Aggregate(string.Empty, (a, c) => a + mapping.GetValueOrDefault(c, c.ToString()));


        public static int CreateUniqueId() =>
            idGenerator = Interlocked.Increment(ref idGenerator);


        private static IDictionary<char, string> mapping = new Dictionary<char, string>
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

        private static int idGenerator;
    }
}