using LASI;
using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LASI.ContentSystem.Serialization.XML.ILexicalExtensions
{
    static class SerializationExtensions
    {
        static XElement ToXml(this ILexical element) {
            try {
                return (element as dynamic).ToXElement();
            } catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) {
                return null;
            }
        }

        static XElement ToXElement(this Noun noun) {
            return new XElement(noun.Type.Name,
                new XAttribute("ID", noun.ID),
                new XAttribute("Weight", noun.Weight),
                new XAttribute("MetaWeight", noun.MetaWeight),
                new XElement("SubjectOf", GetIdentityString(noun.SubjectOf)),
                new XElement("DirectObjectOf", GetIdentityString(noun.DirectObjectOf)),
                new XElement("IndirectObjectOf", GetIdentityString(noun.IndirectObjectOf)),
                new XElement("Bound Pronouns",
                    from e in noun.BoundPronouns
                    let content = e.GetIdentityString()
                    select new XElement("ReferencedBy", content)),
                new XElement("Descriptors",
                    from e in noun.Descriptors
                    let content = e.GetIdentityString()
                    select new XElement("DescribedBy", content)),
                new XElement("Possessions",
                    from e in noun.Possessed
                    let content = e.GetIdentityString()
                    select new XElement("Possesses", content)),
                noun.Text
            );
        }
        static XElement ToXElement(this NounPhrase noun) {
            return new XElement(noun.Type.Name,
                new XAttribute("ID", noun.ID),
                new XAttribute("Weight", noun.Weight),
                new XAttribute("MetaWeight", noun.MetaWeight),
                new XElement("SubjectOf", GetIdentityString(noun.SubjectOf)),
                new XElement("DirectObjectOf", GetIdentityString(noun.DirectObjectOf)),
                new XElement("IndirectObjectOf", GetIdentityString(noun.IndirectObjectOf)),
                new XElement("Bound Pronouns",
                    from e in noun.BoundPronouns
                    let content = e.GetIdentityString()
                    select new XElement("ReferencedBy", content)),
                new XElement("Descriptors",
                    from e in noun.Descriptors
                    let content = e.GetIdentityString()
                    select new XElement("DescribedBy", content)),
                new XElement("Possessions",
                    from e in noun.Possessed
                    let content = e.GetIdentityString()
                    select new XElement("Possesses", content)),
                new XElement("Words",
                    from e in noun.Words
                    select e.ToXml()),
                noun.Text
            );
        }


        #region Serialization Helpers

        static string GetIdentityString(this ILexical element) {
            if (element == null)
                return string.Empty;
            var result = element.Type.Name + " ";
            var w = element as Word;
            if (w != null) {
                result += w.ID;
            } else {
                var p = element as Phrase;
                if (p != null) {
                    result += p.ID;
                }
            }
            return result;
        }

        #endregion
    }
}
