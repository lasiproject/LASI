using LASI.Core;
using System.Linq;
using System.Windows.Media;

namespace LASI.App.Visualization
{
    using static Brushes;
    class SyntacticColorMap : Interop.Visualization.IStyleProvider<ILexical, Brush>
    {
        /// <summary>
        /// Maps a Lexical element to a syntax highlighting color. The returned value is a System.Windows.Media.Brush enumeration member.
        /// </summary>
        /// <param name="element">The Lexical for which to get a color based on its syntactic role.</param>
        /// <returns>A System.Windows.Media.Brush enumeration value mapped to the syntactic role of the element.</returns>
        public Brush this[ILexical element] =>
            (from value in element.Match()
                .Case((Phrase p) =>
                    from color in p.Match()
                        .Case((PronounPhrase e) => HotPink)
                        .Case((NounPhrase n) => n.Words.OfProperNoun().Any() ? DarkBlue : MediumTurquoise)
                        .Case((InfinitivePhrase e) => Teal)
                        .Case((IReferencer e) => DarkCyan)
                        .Case((IEntity e) => DeepSkyBlue)
                        .Case((IVerbal e) => Green)
                        .Case((IPrepositional e) => DarkOrange)
                        .Case((IDescriptor e) => Indigo)
                        .Case((IAdverbial e) => Orange)
                    select color)
                .Case((Word w) =>
                    from color in w.Match()
                        .Case((Adjective e) => Indigo)
                        .Case((PresentParticiple e) => DarkGreen)
                        .Case((Verb e) => Green)
                    select color)
             from color in value
             select color).DefaultIfEmpty(Black).First();
    }
}
