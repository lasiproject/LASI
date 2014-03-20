using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IPronoun interface.
    /// </summary>
    /// <see cref="IReferencer"/>
    /// <seealso cref="PersonalPronoun"/>
    /// <seealso cref="RelativePronoun"/>
    public static partial class LexicalEnumerable
    {
        #region Sequential Implementation
        /// <summary>
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        ///<typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="source">The sequence of IPronoun elements to filter.</param>
        /// <returns>All Pronouns in the collection that are bound as references of some entity.</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> source) where T : IReferencer {
            return from pro in source
                   where pro.ReferredTo != null
                   select pro;
        }
        /// <summary>
        /// Returns all IPronouns constructs in the collection that refer to the given entity.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="source">The sequence of IPronoun elements to filter.</param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> source, IEntity referenced) where T : IReferencer {
            return from pro in source
                   where pro.ReferredTo == referenced
                   select pro;
        }
        /// <summary>
        /// Returns all IPronoun constructs in the collection that refer to any entity matching the given test entity selector.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="source">The sequence of IPronoun elements to filter.</param>
        /// <param name="condition">The function which tests the referenced entity of each IPronoun to determine if the IPronoun should be selected.</param>
        /// <returns>All IPronoun constructs in the collection that refer to the given entity</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> source, Func<IEntity, bool> condition) where T : IReferencer {
            return from pro in source
                   where pro.ReferredTo != null && condition(pro.ReferredTo)
                   select pro;
        }
        #endregion

        #region Parallel Implementation
        /// <summary>
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        ///<typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="source">The sequence of IPronoun elements to filter.</param>
        /// <returns>All Pronouns in the collection that are bound as references of some entity.</returns>
        public static ParallelQuery<T> Referencing<T>(this ParallelQuery<T> source) where T : IReferencer {
            return from pro in source
                   where pro.ReferredTo != null
                   select pro;
        }
        /// <summary>
        /// Returns all IPronouns constructs in the collection that refer to the given entity.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="source">The sequence of IPronoun elements to filter.</param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static ParallelQuery<T> Referencing<T>(this ParallelQuery<T> source, IEntity referenced) where T : IReferencer {
            return from ER in source
                   where ER.ReferredTo == referenced
                   select ER;
        }
        /// <summary>
        /// Returns all IPronoun constructs in the collection that refer to any entity matching the given test entity selector.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="source">The sequence of IPronoun elements to filter.</param>
        /// <param name="condition">The function which tests the referenced entity of each IPronoun to determine if the IPronoun should be selected.</param>
        /// <returns>All IPronoun constructs in the collection that refer to the given entity</returns>
        public static ParallelQuery<T> Referencing<T>(this ParallelQuery<T> source, Func<IEntity, bool> condition) where T : IReferencer {
            return
                from pro in source
                where pro.ReferredTo != null && condition(pro.ReferredTo)
                select pro;
        }
        #endregion

    }
}
