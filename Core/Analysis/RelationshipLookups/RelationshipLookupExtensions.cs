using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;

namespace LASI.Core.Analysis.Relationships
{
    public static class RelationshipLookupExtensions
    {
        public static IRelationshipLookup<TEntity, TVerbal> UsingEntityComparer<TEntity, TVerbal>(this IRelationshipLookup<TEntity, TVerbal> lookup, Func<TEntity, TEntity, bool> entityEquality)
            where TEntity : class, IEntity
            where TVerbal : class, IVerbal => new RelationshipLookup<TEntity, TVerbal>(lookup, entityEquality, entityEquality);

    }
}
