using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm.Thesauri;
using System.Collections.Concurrent;

namespace LASI.Algorithm.RelationshipLookups
{
    public static class ExpressionExtensions
    {


        public static RelatorSet? IsRelatedTo(this IEntity performer, IEntity receiver) {
            Func<IEntity, IEntity, bool> predicate = (L, R) => L.Text == R.Text || L.IsAliasFor(R) || L.IsSimilarTo(R);

            var lookupTable = entityLookupContexts.ContainsKey(performer) ? entityLookupContexts[performer] : entityLookupContexts.ContainsKey(receiver) ? entityLookupContexts[receiver] : null;
            if (lookupTable != null) {

                var result = lookupTable[performer, predicate, receiver, predicate];
                if (result.Any())
                    return new RelatorSet(result);
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
        public static bool On(this RelatorSet? relatorSet, IVerbal relator) {

            return relatorSet.HasValue ? relatorSet.Value.Relators.Contains(relator) : false;
        }
        public static void SetRelationshipLookup(this IEntity entity, IRelationshipLookup<IEntity, IVerbal> relationshipLookup) {
            entityLookupContexts.AddOrUpdate(entity, relationshipLookup, (k, v) => relationshipLookup);
        }



        public struct RelatorSet
        {

            public RelatorSet(IEnumerable<IVerbal> relators) {
                this.relators = relators;
            }


            private IEnumerable<IVerbal> relators;

            internal IEnumerable<IVerbal> Relators {
                get {
                    return relators;
                }
            }
            public static bool operator true(RelatorSet? set) {
                return set != null;
            }
            public static bool operator false(RelatorSet? set) {
                return set == null;
            }


        }
        private static ConcurrentDictionary<IEntity, IRelationshipLookup<IEntity, IVerbal>> entityLookupContexts = new ConcurrentDictionary<IEntity, IRelationshipLookup<IEntity, IVerbal>>();

    }
}
