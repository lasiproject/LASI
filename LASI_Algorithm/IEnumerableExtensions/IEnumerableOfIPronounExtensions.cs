using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IPronoun interface.
    /// </summary>
    /// <see cref="IPronoun"/>
    public static class IEnumerableOfIPronounExtensions
    { /// <summary>
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        ///<typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="refererring"></param>
        /// <returns>All Pronouns in the collection that are bound as references of some entity.</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> refererring) where T : IPronoun {
            return from ER in refererring
                   where ER.BoundEntity != null
                   select ER;
        }
        /// <summary>
        /// Returns all IPronouns constructs in the collection that refer to the given entity.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="refererring"></param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> refererring, IEntity referenced) where T : IPronoun {
            return from ER in refererring
                   where ER.BoundEntity == referenced
                   select ER;
        }
        /// <summary>
        /// Returns all IPronoun constructs in the collection that refer to any entity matching the given test entity selector.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="condition">The function which tests the entity setPnt deteriming if its refererring IProunoun should be selected.</param>
        /// <param name="refererring">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All IPronoun constructs in the collection that refer to the given entity</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> refererring, Func<IEntity, bool> condition) where T : IPronoun {
            return from ER in refererring
                   where ER.BoundEntity != null && condition(ER.BoundEntity)
                   select ER;
        }
    }
}
