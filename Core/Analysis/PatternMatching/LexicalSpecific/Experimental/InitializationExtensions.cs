//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental
//{
//    static class InitializationExtensions
//    {
//        public static void Add<TLexical, TResult>(this Match<TResult> builder,
//            TypePattern<TLexical> pattern,
//            Func<TLexical, object> item) where TLexical : class, ILexical {
//            builder.Add(pattern, item);
//        }
//    }
//    static class TypePattern
//    {
//        public static ReferencerP Referencer = ReferencerP.Referencer;
//        public static VerbalP Verbal = VerbalP.Vebral;
//        public static EntityP Entity = EntityP.Entity;
//        public class EntityP : TypePattern<IEntity>
//        {
//            private EntityP() { }
//            public static EntityP Entity => new EntityP();


//        }
//        public class VerbalP : TypePattern<IVerbal>
//        {
//            private VerbalP() { }
//            public static VerbalP Vebral => new VerbalP();


//        }

//        public class ReferencerP : TypePattern<IReferencer>
//        {
//            private ReferencerP() { }
//            public static ReferencerP Referencer => new ReferencerP();

//        }
//    }

//    public class TypePattern<T> where T : class, ILexical
//    {
//        private Func<T, object> f;


//    }

//}
