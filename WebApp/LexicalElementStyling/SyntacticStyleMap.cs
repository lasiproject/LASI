using System.Linq;
using LASI.Core;
using System.Web.UI.WebControls;
using LASI.Core.PatternMatching;

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
                return new Style {
                    CssClass = element.Match().Yield<string>()
                        | ((IReferencer r) => "referencer")
                        | ((NounPhrase n) => "entity" + (n.Words.OfProperNoun().Any() ? " proper" : ""))
                        | ((InfinitivePhrase i) => "infinitive")
                        | ((IEntity e) => "entity")
                        | ((IVerbal v) => "verbal")
                        | ((IPrepositional p) => "prepositional")
                        | ((IDescriptor p) => "descriptor")
                        | ((IAdverbial a) => "adverbial")
                        | ((IConjunctive c) => "conjunctive")
                        | ((Adjective w) => "descriptor")
                        | ((PresentParticipleGerund w) => "present-participle-gerund")
                        | ((Verb w) => "verbal")
                        | ((IConjunctive w) => "conjunctive")
                        | (() => "lexical-default-style")
                };
            }
        }
    }
}