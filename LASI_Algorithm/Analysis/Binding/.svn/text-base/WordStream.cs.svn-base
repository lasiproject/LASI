using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LASI.Algorithm.Binding
{
    internal class WordStream
    {
        private IEnumerable<Word> sourceSequence;
        private int resultsFed;


        internal WordStream(IEnumerable<Word> source) {
            sourceSequence = source;

        }

        protected dynamic NextEntity() {
            resultsFed++;
            return new Lazy<IEntity>(() => {
                try {
                    return sourceSequence.Skip(resultsFed).OfType<IEntity>().First() as IEntity;
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            });
        }
        public IReadOnlyCollection<ILexical> SourceSequence {
            get {
                return sourceSequence.ToList().AsReadOnly();
            }
        }
    }
}
