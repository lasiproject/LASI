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
        public static Brush GetBrush(this ILexical lexical) {
            return lexical.Match().Yield<Brush>()
                    .Case<NounPhrase>(n => n.Words.GetProperNouns().Any() ? Brushes.DarkBlue : Brushes.Brown)
                    .Case<InfinitivePhrase>(Brushes.Teal)
                    .Case<IPronoun>(Brushes.DarkCyan)
                    .Case<ProperNoun>(Brushes.DarkBlue)
                    .Case<IEntity>(Brushes.DeepSkyBlue)
                    .Case<IVerbal>(Brushes.Green)
                    .Case<IPrepositional>(Brushes.Red)
                    .Case<IDescriptor>(Brushes.DarkTurquoise)
                    .Case<IAdverbial>(Brushes.Orange)
                .Result(Brushes.Black);
        }
    }
}
