using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LASI.UserInterface.LexicalElementStyling
{
    static class SyntacticBrushMap
    {
        public static SolidColorBrush ToBrush(this string colorString) {
            return new BrushConverter().ConvertFromString(colorString) as SolidColorBrush;
        }
        public static SolidColorBrush GetBrush(this ILexical lexical) {
            try { return BrushStore[(lexical as dynamic).Type]; } catch (KeyNotFoundException) { return new SolidColorBrush(Color.FromRgb(0, 0, 0)); }
        }

        static Dictionary<Type, SolidColorBrush> BrushStore = new Dictionary<Type, SolidColorBrush> {
            { typeof(Determiner), "000000".ToBrush() },
            { typeof(IDescriptor), "E36C0A".ToBrush()},//(Post Modifier?)         
            { typeof(IAdverbial), "4BACC6".ToBrush()},// (modifying Idescriptor) 
            { typeof(IEntity), "7030A0".ToBrush()},
            { typeof(ProperNoun), "4F81BD".ToBrush() },
            { typeof(IVerbal), "00B050".ToBrush()},
            { typeof(IPrepositional), "FF0000".ToBrush()},
            { typeof(IAdverbial), "F79646".ToBrush()},
            { typeof(IDescriptor), "00B0F0".ToBrush()},
            //typeof({ Appositive?    )new BrushConverter().ConvertFromString( "984806" )as Brush},
        };
    }
}
