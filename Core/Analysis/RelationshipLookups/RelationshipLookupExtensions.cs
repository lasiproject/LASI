using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;

namespace LASI.Core.Analysis.Relationships
{
    /// <summary>
    /// Provides extension methods for convenient use of <see cref="IRelationshipLookup{TEntity, TVerbal}"/>s.
    /// </summary>
    public static class RelationshipLookupExtensions
    {
        /// <summary>
        /// Creates a new relationship lookup with the same domain as the first but uses the given comparison function to compare <typeparamref name="TEntity"/>s.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities in the lookup.</typeparam>
        /// <typeparam name="TVerbal">The type of the verbals in the lookup.</typeparam>
        /// <param name="lookup">The source lookup.</param>
        /// <param name="entityEquator">The comparison function the resulting lookup will use to compare <typeparamref name="TEntity"/>s.</param>
        /// <returns>A new relationship lookup with the same domain as the first but uses the given comparison function to compare <typeparamref name="TEntity"/>s.</returns>
        public static IRelationshipLookup<TEntity, TVerbal> UsingEntityComparer<TEntity, TVerbal>(this IRelationshipLookup<TEntity, TVerbal> lookup, Func<TEntity, TEntity, bool> entityEquator)
            where TEntity : class, IEntity
            where TVerbal : class, IVerbal => new RelationshipLookup<TEntity, TVerbal>(lookup, entityEquator, entityEquator);

    }
}
