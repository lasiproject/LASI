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
                    .With<NounPhrase>(n => n.Words.GetProperNouns().Any() ? Brushes.DarkBlue : Brushes.Brown)
                    .With<InfinitivePhrase>(Brushes.Teal)
                    .With<IPronoun>(Brushes.DarkCyan)
                    .With<ProperNoun>(Brushes.DarkBlue)
                    .With<IEntity>(Brushes.DeepSkyBlue)
                    .With<IVerbal>(Brushes.Green)
                    .With<IPrepositional>(Brushes.Red)
                    .With<IDescriptor>(Brushes.DarkTurquoise)
                    .With<IAdverbial>(Brushes.Orange)
                .Result(Brushes.Black);
        }
    }
}
