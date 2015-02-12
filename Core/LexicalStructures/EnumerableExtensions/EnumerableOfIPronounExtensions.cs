using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Utilities.Validation;

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
        /// <param name="referencers">The sequence of IPronoun elements to filter.</param>
        /// <returns>All Pronouns in the collection that are bound as references of some entity.</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> referencers) where T : IReferencer {
            Validate.NotNull(referencers, "referencers");
            return referencers
                .Where(referencer => referencer.RefersTo != null);
        }
        /// <summary>
        /// Returns all IPronouns constructs in the collection that refer to the given entity.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="referencers">The sequence of IPronoun elements to filter.</param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> referencers, IEntity referenced) where T : IReferencer {
            Validate.NotNull(referencers, "referencers", referenced, "referenced");
            return referencers.Referencing()
                .Where(referencer => referencer.RefersTo == referenced || referencer.RefersTo.Any(entity => entity == referenced));
        }
        /// <summary>
        /// Returns all IPronoun constructs in the collection that refer to any entity matching the given test entity selector.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="referencers">The sequence of IPronoun elements to filter.</param>
        /// <param name="predicate">The function which tests the referenced entity of each IPronoun to determine if the IPronoun should be selected.</param>
        /// <returns>All IPronoun constructs in the collection that refer to the given entity</returns>
        public static IEnumerable<T> Referencing<T>(this IEnumerable<T> referencers, Func<IEntity, bool> predicate) where T : IReferencer {
            Validate.NotNull(referencers, "referencers", predicate, "predicate");
            return referencers.Referencing().Where(referencer => predicate(referencer.RefersTo) || referencer.RefersTo.Any(predicate));
        }
        #endregion

        #region Parallel Implementation
        /// <summary>
        /// Returns all Pronouns in the collection that are bound to some entity
        /// </summary>
        ///<typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="referencers">The sequence of IPronoun elements to filter.</param>
        /// <returns>All Pronouns in the collection that are bound as references of some entity.</returns>
        public static ParallelQuery<T> Referencing<T>(this ParallelQuery<T> referencers) where T : IReferencer {
            Validate.NotNull(referencers, "referencers");
            return referencers.Where(referencer => referencer.RefersTo != null);
        }
        /// <summary>
        /// Returns all IPronouns constructs in the collection that refer to the given entity.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="referencers">The sequence of IPronoun elements to filter.</param>
        /// <param name="referenced">The entity whose referencing pronouns will be returned.</param>
        /// <returns>All Pronouns in the collection that refer to the given entity</returns>
        public static ParallelQuery<T> Referencing<T>(this ParallelQuery<T> referencers, IEntity referenced) where T : IReferencer {
            Validate.NotNull(referencers, "referencers", referenced, "referenced");
            return referencers.Referencing()
                .Where(referencer => referencer.RefersTo == referenced || referencer.RefersTo.Any(entity => entity == referenced));
        }
        /// <summary>
        /// Returns all IPronoun constructs in the collection that refer to any entity matching the given test entity selector.
        /// </summary>
        /// <typeparam name="T">Any Type which implements the IPronoun interface.</typeparam>
        /// <param name="referencers">The sequence of IPronoun elements to filter.</param>
        /// <param name="predicate">The function which tests the referenced entity of each IPronoun to determine if the IPronoun should be selected.</param>
        /// <returns>All IPronoun constructs in the collection that refer to the given entity</returns>
        public static ParallelQuery<T> Referencing<T>(this ParallelQuery<T> referencers, Func<IEntity, bool> predicate) where T : IReferencer {
            Validate.NotNull(referencers, "referencers", predicate, "predicate");
            return referencers.Referencing()
                .Where(referencer => predicate(referencer.RefersTo) || referencer.RefersTo.Any(predicate));
        }
        #endregion
    }
}
