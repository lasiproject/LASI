using System;
using System.Collections.Concurrent;
using System.Linq;

namespace LASI.Core.Analysis.Relationships
{
    /// <summary>
    /// Provides convenient extension methods for working with IEntity and IVerbal constructs in the context of an applicable IRelationshipLookup.
    /// </summary>
    public static class RelationShipInferenceExtensions
    {
        /// <summary>
        /// Returns an object containing all of the IVerbals on which the two IEntity constructs are related or null if they have no established IVerbal relationships.
        /// </summary>
        /// <param name="performer">The first IEntity, the performer of the action.</param>
        /// <param name="receiver">The second IEntity, the receiver of the action.</param>
        /// <returns>An object containing all of the IVerbals on which the two IEntity constructs are related or null if they have no established IVerbal relationships.</returns>
        public static ActionsRelatedOn? IsRelatedTo(this IEntity performer, IEntity receiver)
        {
            var mapping = entityLookupContexts.ContainsKey(performer)
                ? entityLookupContexts[performer]
                : entityLookupContexts.ContainsKey(receiver)
                    ? entityLookupContexts[receiver]
                    : null;

            if (mapping != null)
            {
                var actions = mapping[performer, receiver].Concat(mapping[receiver, performer]);
                return actions.Any()
                    ? new ActionsRelatedOn(actions)
                    : default(ActionsRelatedOn?);
            }
            throw new InvalidOperationException(BuildAssociationContextMessage(performer, receiver));
        }

        /// <summary>
        /// Determines if the Given ActionsRelatedOn object contains the provided IVerbal.
        /// </summary>
        /// <param name="relatorSet">The object whose contents are to be searched. This parameter can be null. If it is null, false is returned.</param>
        /// <param name="relator">The IVerbal for which to search.</param>
        /// <returns> <c>true</c> if the given ActionsRelatedOn set contains the provided IVerbal, false if theActionsRelatedOn set does not contain the provided IVerbal or is null.</returns>
        public static bool On(this ActionsRelatedOn? relatorSet, IVerbal relator) => relatorSet?.RelatedOn.Contains(relator, (l, r) => l.Text == r.Text) == true;

        /// <summary>
        /// Associates the given IEntity to the given IRelationshipLookup. All future searches involving the provided entity will be done in the context of the provided lookup.
        /// </summary>
        /// <param name="entity">The IEntity to associate to a lookup context.</param>
        /// <param name="relationshipLookup">The IRelationshipLookup instance providing a lookup context for the entity.</param>
        public static void SetRelationshipLookup(this IEntity entity, IRelationshipLookup<IEntity, IVerbal> relationshipLookup)
        {
            entityLookupContexts.AddOrUpdate(entity, relationshipLookup, (k, v) => relationshipLookup);
        }

        private static string BuildAssociationContextMessage(IEntity performer, IEntity receiver) =>
            $@"There is no relationship lookup context associated with {performer} or {receiver}.
            Please associate a context by calling {performer}.SetRelationshipLookup or {receiver}.SetRelationshipLookup appropriately.";

        private static ConcurrentDictionary<IEntity, IRelationshipLookup<IEntity, IVerbal>> entityLookupContexts = new ConcurrentDictionary<IEntity, IRelationshipLookup<IEntity, IVerbal>>();
    }
}
