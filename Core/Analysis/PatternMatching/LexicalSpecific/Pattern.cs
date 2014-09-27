using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.PatternMatching;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific
{
    //public class Pattern
    //{
    //    public Type PatternType { get; private set; }

    //    private Pattern(Type type) {
    //        PatternType = type;
    //    }
    //    public static readonly Pattern Referencer = new Pattern(typeof(IReferencer));
    //    public static readonly Pattern Entity = new Pattern(typeof(IEntity));
    //    public static readonly Pattern Verbal = new Pattern(typeof(IVerbal));
    //    public static readonly Pattern Descriptor = new Pattern(typeof(IDescriptor));
    //    public static readonly Pattern Adverbial = new Pattern(typeof(IAdverbial));
    //}
    public class Pattern<TResult>
    {
        public Func<IReferencer, TResult> Referencer { get; set; }
        public Func<IEntity, TResult> Entity { get; set; }
        public Func<IVerbal, TResult> Verbal { get; set; }
        public Func<IDescriptor, TResult> Descriptor { get; set; }
        public Func<IAdverbial, TResult> Adverbial { get; set; }
        public Func<Clause, TResult> Clause { get; set; }
        public Func<Phrase, TResult> Phrase { get; set; }

        internal TResult Apply<T>(Match<T, TResult> pattern) where T : class, ILexical {
            return pattern
                        .Case(Referencer)
                        .Case(Entity)
                        .Case(Verbal)
                        .Case(Descriptor)
                        .Case(Adverbial)
                    .Result();
        }
        internal Match<T, TResult> ApplyPartial<T>(Match<T, TResult> pattern) where T : class, ILexical {
            return pattern
                        .Case(Referencer)
                        .Case(Entity)
                        .Case(Verbal)
                        .Case(Descriptor)
                        .Case(Adverbial);
        }
    }
}
