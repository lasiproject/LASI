using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Algorithm.RelationshipLookups
{
    public struct ActionReceiverPair<TAction, TEntity>
        where TAction : IVerbal
        where TEntity : IEntity
    {
        public ActionReceiverPair(TAction action, TEntity receiver)
            : this() {
            this.action = action;
            this.receiver = receiver;
        }

        public TAction Action {
            get {
                return action;
            }
        }
        public TEntity Receiver {
            get {
                return receiver;
            }
        }
        private TAction action;
        private TEntity receiver;
    }
}
