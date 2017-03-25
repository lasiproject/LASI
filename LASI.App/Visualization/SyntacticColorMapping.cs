using LASI.Core;
using System.Linq;
using System.Windows.Media;

namespace LASI.App.Visualization
{
    using static Brushes;
    class SyntacticColorMapping : Interop.Visualization.IStyleProvider<ILexical, Brush>
    {
        /// <summary>
        /// Maps a Lexical element to a syntax highlighting color. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="element">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public Brush this[ILexical element] =>
             element.Match()
                .Case((PronounPhrase e) => HotPink)
                .Case((NounPhrase n) => n.Words.OfProperNoun().Any() ? RoyalBlue : MediumTurquoise)
                .Case((InfinitivePhrase e) => Teal)
                .Case((PresentParticiple e) => DarkGreen)
                .Case((IReferencer e) => DeepPink)
                .Case((IEntity e) => DeepSkyBlue)
                .Case((IVerbal e) => Green)
                .Case((IPrepositional e) => Red)
                .Case((IDescriptor e) => Indigo)
                .Case((IAdverbial e) => Orange)
                .Result(Black);
    }
}
