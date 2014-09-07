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
                        .With((IReferencer r) => "referencer")
                        .With((NounPhrase n) => "entity" + (n.Words.OfProperNoun().Any() ? " proper" : ""))
                        .With((InfinitivePhrase i) => "infinitive")
                        .With((IEntity e) => "entity")
                        .With((IVerbal v) => "verbal")
                        .With((IPrepositional p) => "prepositional")
                        .With((IDescriptor p) => "descriptor")
                        .With((IAdverbial a) => "adverbial")
                        .With((IConjunctive c) => "conjunctive")
                        .With((Adjective w) => "descriptor")
                        .With((PresentParticipleGerund w) => "present-participle-gerund")
                        .With((Verb w) => "verbal")
                        .With((IConjunctive w) => "conjunctive")
                        .Result(() => "lexical-default-style")
                };
            }
        }
    }
}