using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.RelationshipLookups
{
    public struct EntityPair<TEntity> where TEntity : IEntity
    {
        public EntityPair(TEntity performer, TEntity receiver)
            : this() {
            this.performer = performer;
            this.receiver = receiver;
        }

        public TEntity Performer {
            get {
                return performer;
            }
        }
        public TEntity Receiver {
            get {
                return receiver;
            }
        }
        private TEntity receiver;
        private TEntity performer;
    }
}
