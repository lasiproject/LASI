using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LASI.UserInterface
{
    static class SyntacticBrushMap
    {
        public static Brush ToBrush(this string colorString) {
            return new BrushConverter().ConvertFromString(colorString) as Brush;
        }
        public static Brush GetBrush(this ILexical lexical) { return BrushStore[lexical.Type]; }
        public static Brush GetBrush(this IEntity lexical) {
            try {
                return BrushStore[lexical.Type];
            } catch (KeyNotFoundException) {
                return Brushes.Black;
            }
        }
        //public static Brush GetBrush(this IVerbal lexical) { return BrushStore[lexical.Type]; }
        //public static Brush GetBrush(this IPronoun lexical) { return BrushStore[lexical.Type]; }
        //public static Brush GetBrush(this IDescriptor lexical) { return BrushStore[lexical.Type]; }
        //public static Brush GetBrush(this IAdverbial lexical) { return BrushStore[lexical.Type]; }
        //public static Brush GetBrush(this ProperNoun lexical) { return BrushStore[lexical.Type]; }
        //public static Brush GetBrush(this Determiner lexical) { return BrushStore[lexical.Type]; }
        //public static Brush GetBrush(this IPrepositional lexical) { return BrushStore[lexical.Type]; }

        private static readonly IReadOnlyDictionary<Type, Brush> BrushStore = new Dictionary<Type, Brush> {
            { typeof(Determiner),       Brushes.Black },
            { typeof(IDescriptor),      Brushes.DeepPink },     
            { typeof(IAdverbial),       Brushes.Orange }, 
            { typeof(IEntity),          Brushes. Violet },
            { typeof(ProperNoun),       Brushes.DarkBlue },
            { typeof(IVerbal),          Brushes.DarkSeaGreen },
            { typeof(IPrepositional),   Brushes.Magenta }, 
            { typeof(IPronoun),         Brushes. Aquamarine }, 
        };
    }
}
