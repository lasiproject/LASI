using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Analysis.BinderImplementations.Experimental.SequentialPatterns;

namespace LASI.Core.Analysis.Binding
{
    public class IntraSentenceIDescriptorToVerbalSubjectBinder : IIntraSentenceBinder
    {
        public void Bind(Sentence sentence)
        {
            sentence.Match().Case((IVerbal v, IDescriptor a) => v.SubjectComplement = a);
        }
    }
    public static class SentenceDeconstructionExtensions
    {
        public static void Deconstruct<T1, T2>(this Sentence sentence, out T1 a, out T2 b) where T1 : ILexical
            where T2 : ILexical
        {
            var phrases = sentence.Phrases.Select(l => l as ILexical).ToArray();

            (a, b) = phrases[0] is T1 x && phrases[1] is T2 y ? (x, y) : (default, default);
        }
    }
}
