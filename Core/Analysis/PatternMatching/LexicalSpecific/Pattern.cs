using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.PatternMatching;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific
{
    public class Pattern<TResult>
    {
        internal Pattern() { }
        public Func<IReferencer, TResult> Referencer { get; set; }
        public Func<IEntity, TResult> Entity { get; set; }
        public Func<IVerbal, TResult> Verbal { get; set; }
        public Func<IDescriptor, TResult> Descriptor { get; set; }
        public Func<IAdverbial, TResult> Adverbial { get; set; }
        public Func<Clause, TResult> Clause { get; set; }
        public Func<Phrase, TResult> Phrase { get; set; }

        internal TResult Apply<T>(Match<T, TResult> expression) where T : class, ILexical {
            return ApplyPartial(expression).Result();
        }
        internal Match<T, TResult> ApplyPartial<T>(Match<T, TResult> expression) where T : class, ILexical {
            return expression
                        .Case(Referencer)
                        .Case(Entity)
                        .Case(Verbal)
                        .Case(Descriptor)
                        .Case(Adverbial)
                        .Case(Clause)
                        .Case(Phrase);
        }
    }
}
