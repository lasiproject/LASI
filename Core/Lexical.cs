//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LASI.Core.PatternMatching;

//namespace LASI.Core
//{
//    public static class Lexical
//    {
//        public static Match<T> Match<T>(T lexical) where T : class, ILexical {

//            return Matcher.Match(lexical);

//        }

//        private static void TestTypeTokenHelpers<T>(T lexical) where T : class, ILexical {
//            var x = from match in lexical
//                    where Entity
//                    where Verbal
//                    select Verbal;
//        }

//        public static readonly TypeHelper<IVerbal> Verbal = new TypeHelper<IVerbal>();
//        public static readonly TypeHelper<IEntity> Entity = new TypeHelper<IEntity>();
//        public static TLexical Where<T, TLexical>(this T lexical, Func<ILexical, TypeHelper<TLexical>> predicate) where T : class, ILexical where TLexical : class, ILexical {
//            var matches = predicate(lexical);
//            return lexical as TLexical;
//        }
//        public static TLexical Select<T, TLexical>(this T lexical, Func<ILexical, TypeHelper<TLexical>> predicate) where T : class, ILexical where TLexical : class, ILexical {
//            var matches = predicate(lexical);
//            return lexical as TLexical;
//        }
//        public static TypeHelper<TLexical> SelectMany<TLexical, TCollection, TResult>(this ILexical lexical, Func<ILexical, TypeHelper<TLexical>> predicate) where TLexical : class, ILexical {
//            return predicate(lexical);
//        }
//        private static void Matches(this ILexical lexical, bool matches) {

//        }
//        public class TypeHelper<TLexical> where TLexical : class, ILexical
//        {

//            private readonly Func<ILexical, TypeHelper<TLexical>> matchesType;

//            internal TypeHelper() {
//                this.matchesType = o => {
//                    o.Matches(o is TLexical);
//                    return this;
//                };
//            }
//        }
//    }
//}
