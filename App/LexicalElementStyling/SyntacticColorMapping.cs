using LASI.Core;
using LASI.Core.PatternMatching;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace LASI.App
{
    class SyntacticColorMap : LASI.Interop.IStyleProvider<ILexical, Brush>
    {
        /// <summary>
        /// Maps a Lexical element to a syntax highlighting color. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="syntactic">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public Brush this[ILexical syntactic] {
            get {
                return syntactic.Match().Yield<Brush>()
                    .Case((Phrase p) => p.Match().Yield<Brush>()
                          .Case((PronounPhrase e) => Brushes.HotPink)
                          .When((NounPhrase n) => n.Words.OfProperNoun().Any())
                          .Then(Brushes.DarkBlue)
                          .Case((NounPhrase e) => Brushes.MediumTurquoise)
                          .Case((InfinitivePhrase e) => Brushes.Teal)
                          .Case((IReferencer e) => Brushes.DarkCyan)
                          .Case((IEntity e) => Brushes.DeepSkyBlue)
                          .Case((IVerbal e) => Brushes.Green)
                          .Case((IPrepositional e) => Brushes.DarkOrange)
                          .Case((IDescriptor e) => Brushes.Indigo)
                          .Case((IAdverbial e) => Brushes.Orange)
                          .Result(Brushes.Black))
                    .Case((Word w) => w.Match().Yield<Brush>()
                          .Case((Adjective e) => Brushes.Indigo)
                          .Case((PresentParticipleVerb e) => Brushes.DarkGreen)
                          .Case((Verb e) => Brushes.Green)
                          .Result(Brushes.Black))
                    .Result();
            }
        }
    }


}
