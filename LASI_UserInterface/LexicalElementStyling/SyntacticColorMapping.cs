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
                    With<Phrase>(p => p.Match().Yield<Brush>().
                            With<PronounPhrase>(Brushes.HotPink).
                            When<NounPhrase>(n => n.Words.OfProperNoun().Any()).
                            Then(Brushes.DarkBlue).
                            With<NounPhrase>(Brushes.MediumTurquoise).
                            With<InfinitivePhrase>(Brushes.Teal).
                            With<IReferencer>(Brushes.DarkCyan).
                            With<IEntity>(Brushes.DeepSkyBlue).
                            With<IVerbal>(Brushes.Green).
                            With<IPrepositional>(Brushes.DarkOrange).
                            With<IDescriptor>(Brushes.Indigo).
                            With<IAdverbial>(Brushes.Orange).
                        Result(Brushes.Black)).
                    With<Word>(w => w.Match().Yield<Brush>().
                            With<Adjective>(Brushes.Indigo).
                            With<PresentParticipleGerund>(Brushes.DarkGreen).
                            With<Verb>(Brushes.Green).
                        Result(Brushes.Black)).
                    Result();
        }
    }
}
