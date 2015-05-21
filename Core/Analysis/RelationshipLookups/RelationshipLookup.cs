using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.Relationships
{
    /// <summary>
    /// A sample (or test) implementation of the IRelationshipLookup interface.
    /// </summary>
    public class RelationshipLookup<TEntity, TVerbal> : IRelationshipLookup<TEntity, TVerbal> where TEntity : class, IEntity where TVerbal : class, IVerbal
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the RelationshipLookup class over the given domain.
        /// </summary>
        /// <param name="domain">The sequence of TVerbal instances which provide the relevant relationships.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        public RelationshipLookup(IEnumerable<TVerbal> domain, Func<TVerbal, TVerbal, bool> actionComparer, Func<TEntity, TEntity, bool> performerComparer, Func<TEntity, TEntity, bool> receiverComparer)
        {
            verbalRelationshipDomain = domain.WithSubject().WithObject();
            this.
                actionComparer = actionComparer;
            this.performerComparer = performerComparer;
            this.receiverComparer = receiverComparer;
        }
        /// <summary>
        /// Initializes a new instance of the RelationshipLookup class over the given domain.
        /// </summary>
        /// <param name="domain">The sequence of Sentence instances which contain the relevant lexical data set.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>    
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>  
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        public RelationshipLookup(IEnumerable<Sentence> domain, Func<TVerbal, TVerbal, bool> actionComparer, Func<TEntity, TEntity, bool> performerComparer, Func<TEntity, TEntity, bool> receiverComparer)
            : this(domain.Phrases().OfVerbPhrase().OfType<TVerbal>(), actionComparer, performerComparer, receiverComparer)
        {
        }
        /// <summary>
        /// Initializes a new instance of the RelationshipLookup class over the given domain.
        /// </summary>
        /// <param name="domain">The sequence of Paragraph instances which contain the relevant lexical data set.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>    
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>       
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        public RelationshipLookup(IEnumerable<Paragraph> domain, Func<TVerbal, TVerbal, bool> actionComparer, Func<TEntity, TEntity, bool> performerComparer, Func<TEntity, TEntity, bool> receiverComparer)
            : this(domain.SelectMany(paragraph => paragraph.Sentences), actionComparer, performerComparer, receiverComparer)
        {
        }
        /// <summary>
        /// Initializes a new instance of the RelationshipLookup class over the given domain.
        /// </summary>
        /// <param name="domain">The Document instance which contains the relevant lexical data set.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>    
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>       
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        public RelationshipLookup(Document domain, Func<TVerbal, TVerbal, bool> actionComparer, Func<TEntity, TEntity, bool> performerComparer, Func<TEntity, TEntity, bool> receiverComparer)
            : this(domain.Paragraphs, actionComparer, performerComparer, receiverComparer)
        {
        }
        /// <summary>
        /// Initializes a new instance of the RelationshipLookup class from the domain of an exiting RelationshipLookup;
        /// </summary>
        /// <param name="domain">The RelationshipLookup instance which contains the relevant lexical data set.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>       
        public RelationshipLookup(RelationshipLookup<TEntity, TVerbal> domain, Func<TVerbal, TVerbal, bool> actionComparer)
           : this(domain.verbalRelationshipDomain, actionComparer, domain.performerComparer, domain.receiverComparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the RelationshipLookup class from the domain of an exiting RelationshipLookup;
        /// </summary>
        /// <param name="domain">The RelationshipLookup instance which contains the relevant lexical data set.</param>    
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>       
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        public RelationshipLookup(IRelationshipLookup<TEntity, TVerbal> domain, Func<TEntity, TEntity, bool> performerComparer, Func<TEntity, TEntity, bool> receiverComparer)
            : this(domain, EqualityComparer<TVerbal>.Default.Equals, performerComparer, receiverComparer)
        {
        }


        #endregion
        #region Methods

        System.Collections.Generic.IEnumerator<TVerbal> System.Collections.Generic.IEnumerable<TVerbal>.GetEnumerator() => verbalRelationshipDomain.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => verbalRelationshipDomain.GetEnumerator();
        #endregion
        #region Indexers
        /// <summary>
        /// Gets the Verbals which are known to link the given action Performing Entity and action Receiving Entity.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="actionReceiver">The action Receiving Entity</param>
        /// <returns>The Verbals which are known to link the given action Performing Entity and action Receiving Entity.</returns>
        public IEnumerable<TVerbal> this[TEntity actionPerformer, TEntity actionReceiver] => this[actionPerformer, performerComparer, actionReceiver, receiverComparer];

        /// <summary>
        /// Gets the Verbals which are known to link the given action Performing Entity and action Receiving Entity.
        /// The extant performers and receivers within the data set are matched based on the logic of the supplied predicate functions.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        /// <param name="actionReceiver">The action Receiving Entity</param>
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        /// <returns>The Verbals which are known to link the given action Performing Entity and action Receiving Entity.</returns>
        public IEnumerable<TVerbal> this[TEntity actionPerformer, Func<TEntity, TEntity, bool> performerComparer, TEntity actionReceiver, Func<TEntity, TEntity, bool> receiverComparer] => verbalRelationshipDomain
                .WithSubject(s => s.Match((TEntity e) => performerComparer(actionPerformer, e)))
                .WithObject(o => o.Match((TEntity e) => receiverComparer(actionReceiver, e)));
        /// <summary>
        /// Gets the collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.
        /// </summary>
        /// <param name="action">The verbal for which to find relationships over.</param>
        /// <returns>The collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.</returns>
        public IEnumerable<PerformerReceiverPair<TEntity, TEntity>> this[TVerbal action] => this[action, actionComparer];

        /// <summary>
        /// Gets the collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers for all received Actions the given Entity performs.
        /// </summary>
        /// <param name="performer">The verbal for which to find relationships over.</param>
        /// <returns>The collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers for all received Actions the given Entity performs.</returns>
        public IEnumerable<ActionReceiverPair<TVerbal, TEntity>> this[TEntity performer] => this[performer, performerComparer];
        /// <summary>
        /// Gets the collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers which for all received Actions the given Entity performs.
        /// The extant performers within the data set are matched based on the logic of the supplied predicate function.
        /// </summary>
        /// <param name="performer">The verbal for which to find relationships over.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        /// <returns>The collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers which for all received Actions the given Entity performs.</returns>
        public IEnumerable<ActionReceiverPair<TVerbal, TEntity>> this[TEntity performer, Func<TEntity, TEntity, bool> performerComparer] =>
            from action in verbalRelationshipDomain
            from doer in action.Subjects.OfType<TEntity>()
            where performerComparer(doer, performer)
            from receiver in action.DirectObjects.Concat(action.IndirectObjects).OfType<TEntity>()
            select new ActionReceiverPair<TVerbal, TEntity>(action, receiver);

        /// <summary>
        /// Gets the collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.
        /// The extant verbals within the data set are matched based on the logic of the supplied predicate function.
        /// </summary>
        /// <param name="action">The verbal for which to find relationships over.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>
        /// <returns>The collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.</returns>
        public IEnumerable<PerformerReceiverPair<TEntity, TEntity>> this[TVerbal action, Func<TVerbal, TVerbal, bool> actionComparer] =>
            from a in verbalRelationshipDomain
            where actionComparer(a, action)
            from performer in action.Subjects.OfType<TEntity>()
            from receiver in action.DirectObjects.Concat(action.IndirectObjects).OfType<TEntity>()
            select new PerformerReceiverPair<TEntity, TEntity>(performer, receiver);

        /// <summary>
        /// Gets the collection of entities which are the recipients of the given action when performed by the given action Performer.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="action">The Action which the Performing Entity performs.</param>
        /// <returns>The collection of entities which are the recipients of the given action when performed by the given action Performer.</returns>
        public IEnumerable<TEntity> this[TEntity actionPerformer, TVerbal action] => this[actionPerformer, performerComparer, action, actionComparer];

        /// <summary>
        /// Gets the collection of entities which are the recipients of the given action when performed by the given action Performer.
        /// The extant Actions and Action performers within the data set are matched based on the logic of the supplied predicate functions.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        /// <param name="action">The Action which the Performing Entity performs.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>
        /// <returns>The collection of entities which are the recipients of the given action when performed by the given action Performer.</returns>
        public IEnumerable<TEntity> this[
            TEntity actionPerformer,
            Func<TEntity, TEntity, bool> performerComparer,
            TVerbal action,
            Func<TVerbal, TVerbal, bool> actionComparer] => from verbal in verbalRelationshipDomain
                                                            where actionComparer(verbal, action)
                                                            where verbal.HasSubject(s => s.Match().Case((TEntity e) => performerComparer(e, actionPerformer)).Result())
                                                            from receiver in verbal.DirectObjects.Concat(verbal.IndirectObjects).OfType<TEntity>()
                                                            select receiver;

        #endregion

        #region Fields

        private IEnumerable<TVerbal> verbalRelationshipDomain;
        private readonly Func<TEntity, TEntity, bool> receiverComparer;
        private readonly Func<TEntity, TEntity, bool> performerComparer;
        private readonly Func<TVerbal, TVerbal, bool> actionComparer;

        #endregion

    }
}
