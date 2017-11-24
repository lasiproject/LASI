using System.Collections.Generic;

namespace LASI.Core.Analysis.Binding {
    public partial class ObjectBinder {
        #region Helper Classes
        class PhraseStackWrapper {
            public PhraseStackWrapper(Stack<Phrase> source) {
                stream = new Stack<Phrase>(source);
            }
            public Phrase Get() => stream.Pop();
            public bool Any => stream.Any();
            public bool None => !Any;
            public int Count => stream.Count;
            public List<Phrase> ToList() => stream.ToList();

            readonly Stack<Phrase> stream;
        }
        #endregion

    }
}
