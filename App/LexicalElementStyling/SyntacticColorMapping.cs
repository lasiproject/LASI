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
                    .With((Phrase p) => p.Match().Yield<Brush>()
                          .With((PronounPhrase e) => Brushes.HotPink)
                          .When((NounPhrase n) => n.Words.OfProperNoun().Any())
                          .Then(Brushes.DarkBlue)
                          .With((NounPhrase e) => Brushes.MediumTurquoise)
                          .With((InfinitivePhrase e) => Brushes.Teal)
                          .With((IReferencer e) => Brushes.DarkCyan)
                          .With((IEntity e) => Brushes.DeepSkyBlue)
                          .With((IVerbal e) => Brushes.Green)
                          .With((IPrepositional e) => Brushes.DarkOrange)
                          .With((IDescriptor e) => Brushes.Indigo)
                          .With((IAdverbial e) => Brushes.Orange)
                          .Result(Brushes.Black))
                    .With((Word w) => w.Match().Yield<Brush>()
                          .With((Adjective e) => Brushes.Indigo)
                          .With((PresentParticipleGerund e) => Brushes.DarkGreen)
                          .With((Verb e) => Brushes.Green)
                          .Result(Brushes.Black))
                    .Result();
            }
        }
    }


}
