using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Analysis.Binding
{
    internal sealed partial class ObjectBinder
    {
        public class PhraseStackWrapper
        {
            public PhraseStackWrapper(Stack<Phrase> source)
            {
                stream = new Stack<Phrase>(source);
            }

            public Phrase Get() => stream.Pop();
            public bool Any => stream.Any();
            public bool None => !Any;
            public int Count => stream.Count;
            public List<Phrase> ToList() => stream.ToList();

            readonly Stack<Phrase> stream;
        }
    }
}
