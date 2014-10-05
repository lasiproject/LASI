using System.Linq;
using LASI.Core;
using System.Web.UI.WebControls;

namespace LASI.WebApp
{
    public class SyntacticStyleMap : Interop.IStyleProvider<ILexical, Style>
    {
        /// <summary>
        /// Maps a Lexical element to a CSS Style specification based on its syntactic nature. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="element">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public Style this[ILexical element] {
            get {
                return new Style
                {
                    CssClass = element.Match().Yield<string>()
                        .Case((IReferencer r) => "referencer")
                        .Case((NounPhrase n) => "entity" + (n.Words.OfProperNoun().Any() ? " proper" : ""))
                        .Case((InfinitivePhrase i) => "infinitive")
                        .Case((IEntity e) => "entity")
                        .Case((IVerbal v) => "verbal")
                        .Case((IPrepositional p) => "prepositional")
                        .Case((IDescriptor p) => "descriptor")
                        .Case((IAdverbial a) => "adverbial")
                        .Case((IConjunctive c) => "conjunctive")
                        .Case((Adjective w) => "descriptor")
                        .Case((PresentParticipleVerb w) => "present-participle-gerund")
                        .Case((Verb w) => "verbal")
                        .Case((IConjunctive w) => "conjunctive")
                        .Result(() => "lexical-default-style")
                };
            }
        }
    }
}