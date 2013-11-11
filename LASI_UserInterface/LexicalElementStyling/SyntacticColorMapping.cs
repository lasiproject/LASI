using LASI.Core;
using LASI.Core.Patternization;
using System.Linq;
using System.Windows.Media;

namespace LASI.App
{
    static class SyntacticStylization
    {
        /// <summary>
        /// Maps a Lexical element to a syntax highlighting color. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="syntactic">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public static Brush GetSyntacticColorization(this ILexical syntactic) {
            return syntactic.Match().Yield<Brush>().
                    _<Phrase>(p => p.Match().Yield<Brush>().
                            _<PronounPhrase>(Brushes.HotPink).
                            When<NounPhrase>(n => n.Words.OfProperNoun().Any()).
                            Then(Brushes.DarkBlue).
                            _<NounPhrase>(Brushes.MediumTurquoise).
                            _<InfinitivePhrase>(Brushes.Teal).
                            _<IReferencer>(Brushes.DarkCyan).
                            _<IEntity>(Brushes.DeepSkyBlue).
                            _<IVerbal>(Brushes.Green).
                            _<IPrepositional>(Brushes.DarkOrange).
                            _<IDescriptor>(Brushes.Indigo).
                            _<IAdverbial>(Brushes.Orange).
                        Result(Brushes.Black)).
                    _<Word>(w => w.Match().Yield<Brush>().
                            _<Adjective>(Brushes.Indigo).
                            _<PresentParticipleGerund>(Brushes.DarkGreen).
                            _<Verb>(Brushes.Green).
                        Result(Brushes.Black)).
                    Result();
        }
    }
}
