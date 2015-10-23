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
            sentence.Match()
                .Case((IVerbal v, IDescriptor a) =>
                {
                    v.SubjectComplement = a;
                });
        }
    }
}