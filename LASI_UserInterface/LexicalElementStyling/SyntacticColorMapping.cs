using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LASI.Algorithm.Patternization;

namespace LASI.UserInterface
{
    static class SyntacticStylization
    {
        public static Brush GetBrush(this ILexical lexical)
        {
            return lexical.Match().Yield<Brush>()
                .Case<Phrase>(p =>
                    p.Match().Yield<Brush>()
                        .Case<PronounPhrase>(Brushes.HotPink)
                        .When<NounPhrase>(n => n.Words.OfProperNoun().Any())
                        .Then(Brushes.DarkBlue)
                        .Case<NounPhrase>(Brushes.MediumTurquoise)
                        .Case<InfinitivePhrase>(Brushes.Teal)
                        .Case<IPronoun>(Brushes.DarkCyan)
                        .Case<IEntity>(Brushes.DeepSkyBlue)
                        .Case<IVerbal>(Brushes.Green)
                        .Case<IPrepositional>(Brushes.DarkOrange)
                        .Case<IDescriptor>(Brushes.Indigo)
                        .Case<IAdverbial>(Brushes.Orange)
                    .Result(Brushes.Black))
                .Case<Word>(w =>
                    w.Match().Yield<Brush>()
                        .Case<Adjective>(Brushes.Indigo)
                        .Case<PresentParticipleGerund>(Brushes.DarkGreen)
                        .Case<Verb>(Brushes.Green)
                    .Result(Brushes.Black))
                .Result();
        }
    }
}
