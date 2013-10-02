using LASI;
using LASI.Algorithm;
using LASI.Algorithm.Patternization;
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
        //   static XElement ToXml(this ILexical lex) {
        //return new XElement(lex.Type.Name,
        //    new XAttribute("ID",
        //        lex.Match().Yield<int>()
        //                .Case((Phrase p) => p.ID)
        //                .Case((Word w) => w.ID)
        //                .Result()),
        //        lex
        //        .MatchMany()
        //        .Yield()
        //            .With<IEntity>(e => e.SerializeAspects())
        //            .With<Clause>(c => new XElement("Phrases", c.Phrases.Select(r => r.ToXml())))
        //            .With<Phrase>(r => new XElement("Words", r.Words.Select(w => w.ToXml())))
        //        .Always(lex.Text).Results()
        //            );
        //      }

        static IEnumerable<XObject> SerializeAspects(this IEntity entity)
        {

            return new XObject[]{ 
                new XAttribute("Weight", entity.Weight),
                new XAttribute("MetaWeight", entity.MetaWeight),
                new XElement("SubjectOf", GetIdentityString(entity.SubjectOf)),
                new XElement("DirectObjectOf", GetIdentityString(entity.DirectObjectOf)),
                new XElement("IndirectObjectOf", GetIdentityString(entity.IndirectObjectOf)),
                new XElement("Bound Pronouns",
                    from e in entity.BoundPronouns
                    let content = e.GetIdentityString()
                    select new XElement("ReferencedBy", content)),
                new XElement("Descriptors",
                    from e in entity.Descriptors
                    let content = e.GetIdentityString()
                    select new XElement("DescribedBy", content)),
                new XElement("Possessions",
                    from e in entity.Possessed
                    let content = e.GetIdentityString()
                    select new XElement("Possesses", content))
            };
        }

        #region Serialization Helpers

        private static string GetIdentityString(this ILexical element)
        {
            if (element == null)
                return string.Empty;
            var result = element.Type.Name + " ";
            var w = element as Word;
            if (w != null) {
                result += w.ID;
            }
            return result;
        }

        #endregion
    }
}
