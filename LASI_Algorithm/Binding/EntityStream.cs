using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.Binding
{
    internal class EntityStream
    {
        private IEnumerable<ILexical> sourceSequence;
        private int resultsFed;


        internal EntityStream(IEnumerable<ILexical> source) {
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
        public IEnumerable<ILexical> SourceSequence {
            get {
                return sourceSequence;
            }
        }
    }
}
