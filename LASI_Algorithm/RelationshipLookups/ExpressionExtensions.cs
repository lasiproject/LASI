using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Lookup;
using System.Collections.Concurrent;

namespace LASI.Algorithm.RelationshipLookups
{
    public static class ExpressionExtensions
    {


        public static ActionsRelatedOn? IsRelatedTo(this IEntity performer, IEntity receiver) {
            Func<IEntity, IEntity, bool> predicate = (L, R) => L.IsAliasFor(R) || L.IsSimilarTo(R);

            var lookupTable = entityLookupContexts.ContainsKey(performer) ? entityLookupContexts[performer] : entityLookupContexts.ContainsKey(receiver) ? entityLookupContexts[receiver] : null;
            if (lookupTable != null) {

                var result = lookupTable[performer, predicate, receiver, predicate];
                if (result.Any())
                    return new ActionsRelatedOn(result);
                else
                    return null;
            }
            else {
                throw new InvalidOperationException(string.Format(@"There is no relationship lookup Context associated with {0} or {1}.\n
                    Please associate a context by calling {2}.SetRelationshipLookup or {3}.SetRelationshipLookup appropriately.",
                    performer, receiver,
                    performer, receiver));
            }

        }
        public static bool On(this ActionsRelatedOn? relatorSet, IVerbal relator) {

            return relatorSet.HasValue ? relatorSet.Value.RelatedOn.Contains(relator) : false;
        }

        public static void SetRelationshipLookup(this IEntity entity, IRelationshipLookup<IEntity, IVerbal> relationshipLookup) {
            entityLookupContexts.AddOrUpdate(entity, relationshipLookup, (k, v) => relationshipLookup);
        }




        private static ConcurrentDictionary<IEntity, IRelationshipLookup<IEntity, IVerbal>> entityLookupContexts = new ConcurrentDictionary<IEntity, IRelationshipLookup<IEntity, IVerbal>>();

        public struct ActionsRelatedOn
        {

            internal ActionsRelatedOn(IEnumerable<IVerbal> actionsRelatedOn)
                : this() {
                RelatedOn = actionsRelatedOn;
            }

            internal IEnumerable<IVerbal> RelatedOn {
                get;
                private set;
            }
            public static bool operator true(ActionsRelatedOn? set) {
                return set != null;
            }
            public static bool operator false(ActionsRelatedOn? set) {
                return set == null;
            }
        }
    }
}
