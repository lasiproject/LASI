using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Heuristics
{
    /// <summary>
    /// Defines the behaviors required of a lookup  table which indexes on various relationships between various combinations of IEntity and IVerbal constructs. 
    /// </summary>
    /// <typeparam name="TEntity">Any type which implements the IEntity interface.</typeparam>
    /// <typeparam name="TVerbal">Any type which implements the IVerbal interface.</typeparam>
    public interface IRelationshipLookup<TEntity, TVerbal>
        where TEntity : IEntity
        where TVerbal : IVerbal
    {
        /// <summary>
        /// Gets the Verbals which are known to link the given action Performing Entity and action Receiving Entity.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="actionReceiver">The action Receiving Entity</param>
        /// <returns>The Verbals which are known to link the given action Performing Entity and action Receiving Entity.</returns>
        IEnumerable<TVerbal> this[TEntity actionPerformer, TEntity actionReceiver] {
            get;
        }
        /// <summary>
        /// Gets the Verbals which are known to link the given action Performing Entity and action Receiving Entity.
        /// The extant performers and recevers within the data set are matched based on the logic of the supplied predicate functions.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        /// <param name="actionReceiver">The action Receiving Entity</param>
        /// <param name="receiverComparer">A predicate function which determines how to find matches for action Receiver.</param>
        /// <returns>The Verbals which are known to link the given action Performing Entity and action Receiving Entity.</returns>
        IEnumerable<TVerbal> this[TEntity actionPerformer, Func<TEntity, TEntity, bool> performerComparer, TEntity actionReceiver, Func<TEntity, TEntity, bool> receiverComparer] {
            get;
        }
        /// <summary>
        /// Gets the collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.
        /// </summary>
        /// <param name="relator">The verbal for which to find relationships over.</param>
        /// <returns>The collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.</returns>
        IEnumerable<PerformerReceiverPair<TEntity, TEntity>> this[TVerbal relator] {
            get;
        }
        /// <summary>
        /// Gets the collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers for all received Actions the given Entity performs.
        /// </summary>
        /// <param name="performer">The verbal for which to find relationships over.</param>
        /// <returns>The collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers for all received Actions the given Entity performs.</returns>
        IEnumerable<ActionReceiverPair<TVerbal, TEntity>> this[TEntity performer] {
            get;
        }
        /// <summary>
        /// Gets the collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers for all received Actions the given Entity performs.
        /// The extant performers within the data set are matched based on the logic of the supplied predicate function.
        /// </summary>
        /// <param name="performer">The verbal for which to find relationships over.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        /// <returns>The collection of Action - Receiver ActionReceiverPairs which consists of all pairings of Actions and Receivers which for all received Actions the given Entity performs.</returns>
        IEnumerable<ActionReceiverPair<TVerbal, TEntity>> this[TEntity performer, Func<TEntity, TEntity, bool> performerComparer] {
            get;
        }
        /// <summary>
        /// Gets the collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.
        /// The extant verbals within the data set are matched based on the logic of the supplied predicate function.
        /// </summary>
        /// <param name="relater">The verbal for which to find relationships over.</param>
        /// <param name="verbalComparer">A predicate function which determines how to find matches for the given Verbal.</param>
        /// <returns>The collection of Performer - Receiver EntityPairs which consists of all pairing of Entities which are related by the given Verbal.</returns>
        IEnumerable<PerformerReceiverPair<TEntity, TEntity>> this[TVerbal relater, Func<TVerbal, TVerbal, bool> verbalComparer] {
            get;
        }
        /// <summary>
        /// Gets the collection of entities which are the recipients of the given action when performed by the given action Performer.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="action">The Action which the Performing Entity performs.</param>
        /// <returns>The collection of entities which are the recipients of the given action when performed by the given action Performer.</returns>
        IEnumerable<TEntity> this[TEntity actionPerformer, TVerbal action] {
            get;
        }
        /// <summary>
        /// Gets the collection of entities which are the recipients of the given action when performed by the given action Performer.
        /// The extant Actions and Action performers within the data set are matched based on the logic of the supplied predicate functions.
        /// </summary>
        /// <param name="actionPerformer">The action Performing Entity.</param>
        /// <param name="performerComparer">A predicate function which determines how to find matches for action Performer.</param>
        /// <param name="action">The Action which the Performing Entity performs.</param>
        /// <param name="actionComparer">A predicate function which determines how to find matches for the given Verbal.</param>
        /// <returns>The collection of entities which are the recipients of the given action when performed by the given action Performer.</returns>
        IEnumerable<TEntity> this[TEntity actionPerformer, Func<TEntity, TEntity, bool> performerComparer, TVerbal action, Func<TVerbal, TVerbal, bool> actionComparer] {
            get;
        }
    }
}
