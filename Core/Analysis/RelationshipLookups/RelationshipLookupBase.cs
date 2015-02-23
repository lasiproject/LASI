//using System;
//using System.Collections.Generic;

//namespace LASI.Core.Heuristics
//{
//    public abstract class RelationshipLookupBase<TEntity, TVerbal> : IRelationshipLookup<TEntity, TVerbal> where TEntity : IEntity where TVerbal : IVerbal
//    {
//        public abstract IEnumerable<ActionReceiverPair<TVerbal, TEntity>> this[TEntity performer] { get; }
//        public abstract IEnumerable<PerformerReceiverPair<TEntity, TEntity>> this[TVerbal relator] { get; }
//        public abstract IEnumerable<TEntity> this[TEntity actionPerformer, TVerbal action] { get; }
//        public abstract IEnumerable<PerformerReceiverPair<TEntity, TEntity>> this[TVerbal relater, Func<TVerbal, TVerbal, bool> verbalComparer] { get; }
//        public abstract IEnumerable<ActionReceiverPair<TVerbal, TEntity>> this[TEntity performer, Func<TEntity, TEntity, bool> performerComparer] { get; }
//        public abstract IEnumerable<TVerbal> this[TEntity actionPerformer, TEntity actionReceiver] { get; }
//        public abstract IEnumerable<TEntity> this[TEntity actionPerformer, Func<TEntity, TEntity, bool> performerComparer, TVerbal action, Func<TVerbal, TVerbal, bool> actionComparer] { get; }
//        public abstract IEnumerable<TVerbal> this[TEntity actionPerformer, Func<TEntity, TEntity, bool> performerComparer, TEntity actionReceiver, Func<TEntity, TEntity, bool> receiverComparer] { get; }
//    }
//}