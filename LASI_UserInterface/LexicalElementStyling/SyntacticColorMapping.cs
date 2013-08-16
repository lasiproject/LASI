using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using LASI.Utilities.PatternMatching;

namespace LASI.UserInterface
{
    static class SyntacticStylization
    {
        public static Brush GetBrush(this ILexical lexical) {
            return Match.From(lexical).To<Brush>()
                .With<Determiner>(() => Brushes.Black)
                .With<NounPhrase>(n => n.Words.Any(w => w is ProperNoun) ? Brushes.DarkBlue : Brushes.Brown)
                .With<InfinitivePhrase>(() => Brushes.Teal)
                .With<IPronoun>(() => Brushes.DarkCyan)
                .With<ProperNoun>(() => Brushes.DarkBlue)
                .With<IEntity>(() => Brushes.DeepSkyBlue)
                .With<IVerbal>(() => Brushes.Green)
                .With<IPrepositional>(() => Brushes.Red)
                .With<IDescriptor>(() => Brushes.DarkTurquoise)
                .With<IAdverbial>(() => Brushes.Orange)
                .Default(() => Brushes.Black)
                .Result();
        }
    }
}
