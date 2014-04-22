using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;
using LASI.Core.DocumentStructures;



namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{

    /// <summary>
    /// Encapsulates a method that has 17 parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.This
    /// type parameter is contravariant. That is, you can use either the type you specified
    /// or any type that is less derived. For more information about covariance and contravariance,
    /// see Covariance and Contravariance in Generics.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the seventeenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg15">The fifteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg16">The sixteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg17">The seventeenth parameter of the method that this delegate encapsulates.</param>
    public delegate void Action<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17);
    /// <summary>
    /// Encapsulates a method that has 18 parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.This
    /// type parameter is contravariant. That is, you can use either the type you specified
    /// or any type that is less derived. For more information about covariance and contravariance,
    /// see Covariance and Contravariance in Generics.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the seventeenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T18">The type of the eighteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg15">The fifteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg16">The sixteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg17">The seventeenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg18">The eighteenth parameter of the method that this delegate encapsulates.</param>
    public delegate void Action<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, in T18>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18);
    /// <summary>
    /// Encapsulates a method that has 19 parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.This
    /// type parameter is contravariant. That is, you can use either the type you specified
    /// or any type that is less derived. For more information about covariance and contravariance,
    /// see Covariance and Contravariance in Generics.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the seventeenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T18">The type of the eighteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T19">The type of the nineteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg15">The fifteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg16">The sixteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg17">The seventeenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg18">The eighteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg19">The nineteenth parameter of the method that this delegate encapsulates.</param>
    public delegate void Action<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, in T18, in T19>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19);
    /// <summary>
    /// Encapsulates a method that has 20 parameters and does not return a value.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.This
    /// type parameter is contravariant. That is, you can use either the type you specified
    /// or any type that is less derived. For more information about covariance and contravariance,
    /// see Covariance and Contravariance in Generics.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T6">The type of the sixth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T7">The type of the seventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T8">The type of the eighth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T9">The type of the ninth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T10">The type of the tenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T11">The type of the eleventh parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T12">The type of the twelfth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T13">The type of the thirteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T14">The type of the fourteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T15">The type of the fifteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T16">The type of the sixteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T17">The type of the seventeenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T18">The type of the eighteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T19">The type of the nineteenth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T20">The type of the twentieth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg4">The fourth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg5">The fifth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg6">The sixth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg7">The seventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg8">The eighth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg9">The ninth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg10">The tenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg11">The eleventh parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg12">The twelfth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg13">The thirteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg14">The fourteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg15">The fifteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg16">The sixteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg17">The seventeenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg18">The eighteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg19">The nineteenth parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg20">The twentieth parameter of the method that this delegate encapsulates.</param>
    public delegate void Action<in T1, in T2, in T3, in T4, in T5, in T6, in T7, in T8, in T9, in T10, in T11, in T12, in T13, in T14, in T15, in T16, in T17, in T18, in T19, in T20>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19, T20 arg20);
    static class BindingHelper
    {


        internal static bool Applicable<T1, T2, T3, TLexical>(this Action<T1, T2, T3> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Action<T1, T2, T3, T4> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            return elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Action<T1, T2, T3, T4, T5> pattern, IReadOnlyList<TLexical> elements)
                        where T1 : class, ILexical
                        where T2 : class, ILexical
                        where T3 : class, ILexical
                        where T4 : class, ILexical
                        where T5 : class, ILexical
                        where TLexical : class, ILexical {
            return elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;

        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Action<T1, T2, T3, T4, T5, T6> pattern, IReadOnlyList<TLexical> elements)
                        where T1 : class, ILexical
                        where T2 : class, ILexical
                        where T3 : class, ILexical
                        where T4 : class, ILexical
                        where T5 : class, ILexical
                        where T6 : class, ILexical
                        where TLexical : class, ILexical {
            return elements.Count >= 6 &&
               elements[0] is T1 &&
               elements[1] is T2 &&
               elements[2] is T3 &&
               elements[3] is T4 &&
               elements[4] is T5 &&
               elements[5] is T6;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7> pattern, IReadOnlyList<TLexical> elements)
                       where T1 : class, ILexical
                       where T2 : class, ILexical
                       where T3 : class, ILexical
                       where T4 : class, ILexical
                       where T5 : class, ILexical
                       where T6 : class, ILexical
                       where T7 : class, ILexical
                       where TLexical : class, ILexical {
            return elements.Count >= 7 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where T6 : class, ILexical
                      where T7 : class, ILexical
                      where T8 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 8 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern, IReadOnlyList<TLexical> elements)
                     where T1 : class, ILexical
                     where T2 : class, ILexical
                     where T3 : class, ILexical
                     where T4 : class, ILexical
                     where T5 : class, ILexical
                     where T6 : class, ILexical
                     where T7 : class, ILexical
                     where T8 : class, ILexical
                     where T9 : class, ILexical
                     where TLexical : class, ILexical {
            return elements.Count >= 9 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern, IReadOnlyList<TLexical> elements) where T1 : class, ILexical
                     where T2 : class, ILexical
                     where T3 : class, ILexical
                     where T4 : class, ILexical
                     where T5 : class, ILexical
                     where T6 : class, ILexical
                     where T7 : class, ILexical
                     where T8 : class, ILexical
                     where T9 : class, ILexical
                     where T10 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 10 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern, IReadOnlyList<TLexical> elements)
                     where T1 : class, ILexical
                     where T2 : class, ILexical
                     where T3 : class, ILexical
                     where T4 : class, ILexical
                     where T5 : class, ILexical
                     where T6 : class, ILexical
                     where T7 : class, ILexical
                     where T8 : class, ILexical
                     where T9 : class, ILexical
                     where T10 : class, ILexical
                     where T11 : class, ILexical
                     where TLexical : class, ILexical {
            return elements.Count >= 11 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where T7 : class, ILexical
                   where T8 : class, ILexical
                   where T9 : class, ILexical
                   where T10 : class, ILexical
                   where T11 : class, ILexical
                   where T12 : class, ILexical
                   where TLexical : class, ILexical {
            return elements.Count >= 12 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12;
        }


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern, IReadOnlyList<TLexical> elements)
               where T1 : class, ILexical
               where T2 : class, ILexical
               where T3 : class, ILexical
               where T4 : class, ILexical
               where T5 : class, ILexical
               where T6 : class, ILexical
               where T7 : class, ILexical
               where T8 : class, ILexical
               where T9 : class, ILexical
               where T10 : class, ILexical
               where T11 : class, ILexical
               where T12 : class, ILexical
               where T13 : class, ILexical
               where TLexical : class, ILexical {
            return elements.Count >= 13 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern, IReadOnlyList<TLexical> elements)
           where T1 : class, ILexical
           where T2 : class, ILexical
           where T3 : class, ILexical
           where T4 : class, ILexical
           where T5 : class, ILexical
           where T6 : class, ILexical
           where T7 : class, ILexical
           where T8 : class, ILexical
           where T9 : class, ILexical
           where T10 : class, ILexical
           where T11 : class, ILexical
           where T12 : class, ILexical
           where T13 : class, ILexical
           where T14 : class, ILexical
           where TLexical : class, ILexical {
            return elements.Count >= 14 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern, IReadOnlyList<TLexical> elements)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical
        where T11 : class, ILexical
        where T12 : class, ILexical
        where T13 : class, ILexical
        where T14 : class, ILexical
        where T15 : class, ILexical
        where TLexical : class, ILexical {
            return elements.Count >= 15 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15;
        }


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern, IReadOnlyList<TLexical> elements)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where TLexical : class, ILexical {
            return elements.Count >= 16 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> pattern, IReadOnlyList<TLexical> elements)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where TLexical : class, ILexical {
            return elements.Count >= 17 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16 &&
              elements[16] is T17;
        }



        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> pattern, IReadOnlyList<TLexical> elements)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where T18 : class, ILexical
where TLexical : class, ILexical {
            return elements.Count >= 18 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16 &&
              elements[16] is T17 &&
              elements[17] is T18;
        }


        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> pattern, IReadOnlyList<TLexical> elements)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where T18 : class, ILexical
where T19 : class, ILexical
where TLexical : class, ILexical {
            return elements.Count >= 19 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16 &&
              elements[16] is T17 &&
              elements[17] is T18 &&
              elements[18] is T19;
        }

        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, TLexical>(this Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> pattern, IReadOnlyList<TLexical> elements)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where T18 : class, ILexical
where T19 : class, ILexical
where T20 : class, ILexical
where TLexical : class, ILexical {
            return elements.Count >= 19 &&
              elements[0] is T1 &&
              elements[1] is T2 &&
              elements[2] is T3 &&
              elements[3] is T4 &&
              elements[4] is T5 &&
              elements[5] is T6 &&
              elements[6] is T7 &&
              elements[7] is T8 &&
              elements[8] is T9 &&
              elements[9] is T10 &&
              elements[10] is T11 &&
              elements[11] is T12 &&
              elements[12] is T13 &&
              elements[13] is T14 &&
              elements[14] is T15 &&
              elements[15] is T16 &&
              elements[16] is T17 &&
              elements[17] is T18 &&
              elements[18] is T19 &&
              elements[18] is T20;
        }

        internal static bool Applicable<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 3 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3;
        }
        internal static bool Applicable<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            return elements.Count >= 4 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical {
            return elements.Count >= 5 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical {
            return elements.Count == 6 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
                  where TLexical : class, ILexical {
            return elements.Count == 7 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
                where TLexical : class, ILexical {
            return elements.Count == 8 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical
              where TLexical : class, ILexical {
            return elements.Count == 9 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9;
        }
        internal static bool Applicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where TLexical : class, ILexical {
            return elements.Count == 10 &&
                elements[0] is T1 &&
                elements[1] is T2 &&
                elements[2] is T3 &&
                elements[3] is T4 &&
                elements[4] is T5 &&
                elements[5] is T6 &&
                elements[6] is T7 &&
                elements[7] is T8 &&
                elements[8] is T9 &&
                elements[9] is T10;
        }



        internal static void Apply<T1, T2, T3, TLexical>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<TLexical> elements)
                                  where T1 : class, ILexical
                                  where T2 : class, ILexical
                                  where T3 : class, ILexical
                                  where TLexical : class, ILexical {
            pattern(elements[0] as T1)(elements[1] as T2)(elements[2] as T3);
        }
        internal static void Apply<T1, T2, T3, T4, TLexical>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<TLexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical
                         where TLexical : class, ILexical {
            pattern(
                    elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4);
        }
        internal static void Apply<T1, T2, T3, T4, T5, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<TLexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical
                      where TLexical : class, ILexical {
            pattern(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<TLexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical
                   where TLexical : class, ILexical {
            pattern(
               elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical
                  where TLexical : class, ILexical {
            pattern(
                elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical
                where TLexical : class, ILexical {
            pattern(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical
              where TLexical : class, ILexical {
            pattern(elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8)(
                elements[8] as T9);
        }
        internal static void Apply<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TLexical>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<TLexical> elements)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical {
            pattern(
                   elements[0] as T1)(
                elements[1] as T2)(
                elements[2] as T3)(
                elements[3] as T4)(
                elements[4] as T5)(
                elements[5] as T6)(
                elements[6] as T7)(
                elements[7] as T8)(
                elements[8] as T9)(
                elements[9] as T10);
        }

        internal static bool ApplyIfApplicable<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<ILexical> elements)
                                where T1 : class, ILexical
                                where T2 : class, ILexical
                                where T3 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<ILexical> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<ILexical> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<ILexical> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<ILexical> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<ILexical> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<ILexical> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<ILexical> elements)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3>(this Func<T1, Func<T2, Action<T3>>> pattern, IReadOnlyList<Phrase> elements)
                               where T1 : class, ILexical
                               where T2 : class, ILexical
                               where T3 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;
        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4>(this Func<T1, Func<T2, Func<T3, Action<T4>>>> pattern, IReadOnlyList<Phrase> elements)
                         where T1 : class, ILexical
                         where T2 : class, ILexical
                         where T3 : class, ILexical
                         where T4 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5>(this Func<T1, Func<T2, Func<T3, Func<T4, Action<T5>>>>> pattern, IReadOnlyList<Phrase> elements)
                      where T1 : class, ILexical
                      where T2 : class, ILexical
                      where T3 : class, ILexical
                      where T4 : class, ILexical
                      where T5 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Action<T6>>>>>> pattern, IReadOnlyList<Phrase> elements)
                   where T1 : class, ILexical
                   where T2 : class, ILexical
                   where T3 : class, ILexical
                   where T4 : class, ILexical
                   where T5 : class, ILexical
                   where T6 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Action<T7>>>>>>> pattern, IReadOnlyList<Phrase> elements)
                  where T1 : class, ILexical
                  where T2 : class, ILexical
                  where T3 : class, ILexical
                  where T4 : class, ILexical
                  where T5 : class, ILexical
                  where T6 : class, ILexical
                  where T7 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Action<T8>>>>>>>> pattern, IReadOnlyList<Phrase> elements)
                where T1 : class, ILexical
                where T2 : class, ILexical
                where T3 : class, ILexical
                where T4 : class, ILexical
                where T5 : class, ILexical
                where T6 : class, ILexical
                where T7 : class, ILexical
                where T8 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Action<T9>>>>>>>>> pattern, IReadOnlyList<Phrase> elements)
              where T1 : class, ILexical
              where T2 : class, ILexical
              where T3 : class, ILexical
              where T4 : class, ILexical
              where T5 : class, ILexical
              where T6 : class, ILexical
              where T7 : class, ILexical
              where T8 : class, ILexical
              where T9 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }
        internal static bool ApplyIfApplicable<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern, IReadOnlyList<Phrase> elements)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical {
            var r = pattern.Applicable(elements);
            if (r)
                pattern.Apply(elements);
            return r;

        }

    }
    internal static class Matcher
    {
        internal static SentenceMatch Match(this Sentence sentence) {
            return new SentenceMatch(sentence);
        }
    }
    public partial class SentenceMatch(private Sentence value)
    {
        private bool predicateSucceded;
        private bool guarded;
        List<Func<ILexical, bool>> predicates = new List<Func<ILexical, bool>>();
        Func<Sentence, IEnumerable<ILexical>> test {
            get {

                return val => from v in val.Phrases where predicates.All(f => f(v)) select v;
            }
        }
        protected IReadOnlyList<ILexical> Values { get { return test(value).ToList(); } }
        protected bool Accepted { get; set; }

        public SentenceMatch TryPath<T1, T2, T3>(Action<T1, T2, T3> pattern) where T1 : class, ILexical where T2 : class, ILexical where T3 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4>(Action<T1, T2, T3, T4> pattern)
            where T1 : class, ILexical where T2 : class, ILexical
            where T3 : class, ILexical where T4 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6>(Action<T1, T2, T3, T4, T5, T6> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7>(Action<T1, T2, T3, T4, T5, T6, T7> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8>(Action<T1, T2, T3, T4, T5, T6, T7, T8> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical {

            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> pattern)
        where T1 : class, ILexical
        where T2 : class, ILexical
        where T3 : class, ILexical
        where T4 : class, ILexical
        where T5 : class, ILexical
        where T6 : class, ILexical
        where T7 : class, ILexical
        where T8 : class, ILexical
        where T9 : class, ILexical
        where T10 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15, Values[15] as T16);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15, Values[15] as T16, Values[16] as T17);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where T18 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15, Values[15] as T16, Values[16] as T17, Values[17] as T18);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where T18 : class, ILexical
where T19 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15, Values[15] as T16, Values[16] as T17, Values[17] as T18, Values[18] as T19);
            });
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> pattern)
where T1 : class, ILexical
where T2 : class, ILexical
where T3 : class, ILexical
where T4 : class, ILexical
where T5 : class, ILexical
where T6 : class, ILexical
where T7 : class, ILexical
where T8 : class, ILexical
where T9 : class, ILexical
where T10 : class, ILexical
where T11 : class, ILexical
where T12 : class, ILexical
where T13 : class, ILexical
where T14 : class, ILexical
where T15 : class, ILexical
where T16 : class, ILexical
where T17 : class, ILexical
where T18 : class, ILexical
where T19 : class, ILexical
where T20 : class, ILexical {
            return CheckGuard(() => {
                Accepted = pattern.Applicable(Values);
                if (Accepted)
                    pattern(Values[0] as T1, Values[1] as T2, Values[2] as T3, Values[3] as T4, Values[4] as T5, Values[5] as T6, Values[6] as T7, Values[7] as T8, Values[8] as T9, Values[9] as T10, Values[10] as T11, Values[11] as T12, Values[12] as T13, Values[13] as T14, Values[14] as T15, Values[15] as T16, Values[16] as T17, Values[17] as T18, Values[18] as T19, Values[19] as T20);
            });
        }



        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern)
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical
            where T6 : class, ILexical
            where T7 : class, ILexical
            where T8 : class, ILexical
            where T9 : class, ILexical
            where T10 : class, ILexical
            where T11 : class, ILexical
            where T12 : class, ILexical
            where T13 : class, ILexical
            where T14 : class, ILexical
            where T15 : class, ILexical
            where T16 : class, ILexical
            where T17 : class, ILexical
            where T18 : class, ILexical {
            Accepted = pattern.ApplyIfApplicable(Values.ToList());
            return this;
        }
        public SentenceMatch TryPath<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Func<T1, Func<T2, Func<T3, Func<T4, Func<T5, Func<T6, Func<T7, Func<T8, Func<T9, Action<T10>>>>>>>>>> pattern)
          where T1 : class, ILexical
          where T2 : class, ILexical
          where T3 : class, ILexical
          where T4 : class, ILexical
          where T5 : class, ILexical
          where T6 : class, ILexical
          where T7 : class, ILexical
          where T8 : class, ILexical
          where T9 : class, ILexical
          where T10 : class, ILexical
          where T11 : class, ILexical
          where T12 : class, ILexical
          where T13 : class, ILexical
          where T14 : class, ILexical
          where T15 : class, ILexical
          where T16 : class, ILexical
          where T17 : class, ILexical
          where T18 : class, ILexical
          where T19 : class, ILexical {
            Accepted = pattern.ApplyIfApplicable(Values.ToList());
            return this;
        }


        public SentenceMatch When(bool condition) {
            predicateSucceded = condition;
            return this;
        }
        public SentenceMatch When(Func<bool> condition) {
            predicateSucceded = condition();
            guarded = true;
            return this;
        }
        private SentenceMatch CheckGuard(Action onSuccess) {
            if (!Accepted && guarded && predicateSucceded) { onSuccess(); guarded = false; }
            return this;
        }

        public SentenceMatch FilterAll<T1>()
            where T1 : class, ILexical {
            predicates.Add(v => !(v is T1));
            return this;
        }
        public SentenceMatch FilterAll<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2));
            return this;
        }
        public SentenceMatch FilterAll<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }
        public SentenceMatch FilterAll<T1, T2, T3, T4>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4));
            return this;
        }
        public SentenceMatch FilterAll<T1, T2, T3, T4, T5>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4 || v is T5));
            return this;
        }
        public SentenceMatch FilterAll(Func<ILexical, bool> predicate) {
            predicates.Add(predicate);
            return this;
        }
        public SentenceMatch FilterOnce<T1>()
       where T1 : class, ILexical {
            predicates.Add(v => !(v is T1));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2>()
            where T1 : class, ILexical
            where T2 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2, T3>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2, T3, T4>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4));
            return this;
        }
        public SentenceMatch FilterOnce<T1, T2, T3, T4, T5>()
            where T1 : class, ILexical
            where T2 : class, ILexical
            where T3 : class, ILexical
            where T4 : class, ILexical
            where T5 : class, ILexical {
            predicates.Add(v => !(v is T1 || v is T2 || v is T3 || v is T4 || v is T5));
            return this;
        }
        public SentenceMatch FilterOnce(Func<ILexical, bool> predicate) {
            predicates.Add(predicate);
            return this;
        }
    }



}






namespace LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns
{
    static class ListExtensions
    {
        public static List<T> Take<T>(this List<T> source, int count) {
            return source.GetRange(0, count);
        }
        public static List<T> Skip<T>(this List<T> source, int count) {
            try {
                return source.GetRange(count, source.Count);
            }
            catch (ArgumentException) {
                return new List<T>();
            }
        }

    }

}