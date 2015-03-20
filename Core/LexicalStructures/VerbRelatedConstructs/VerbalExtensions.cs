using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for classes that implement the IVerbal interface.
    /// </summary>
    public static class VerbalExtensions
    {
        /// <summary>
        /// Return a value indicating if the Verbal has any subjects bound to it.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <returns><c>true</c> if the Verbal has any Subjects bound to it; otherwise, <c>false</c>.</returns>
        public static bool HasSubject(this IVerbal verbal) => verbal.Subjects.Any();

        /// <summary>
        /// Return a value indicating if the Verbal has any subjects bound to it which match the given predicate function.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <param name="predicate">The predicate to match subjects. </param>
        /// <returns><c>true</c> if the Verbal has any subjects bound to it which match the given predicate function; otherwise, <c>false</c>.</returns>
        public static bool HasSubject(this IVerbal verbal, Func<IEntity, bool> predicate) => HasBoundEntity(verbal.Subjects, predicate);

        /// <summary>
        /// Return a value indicating if the Verbal has any direct objects bound to it.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <returns><c>true</c> if the Verbal has any direct objects bound to it; otherwise, <c>false</c>.</returns>
        public static bool HasDirectObject(this IVerbal verbal) => verbal.DirectObjects.Any();

        /// <summary>
        /// Return a value indicating if the Verbal has any direct objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns><c>true</c> if the Verbal has any direct objects bound to it which match the given predicate function; otherwise, <c>false</c>.</returns>
        public static bool HasDirectObject(this IVerbal verbal, Func<IEntity, bool> predicate) => HasBoundEntity(verbal.DirectObjects, predicate);

        /// <summary>
        /// Return a value indicating if the Verbal has any indirect objects bound to it.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <returns><c>true</c> if the Verbal has any direct objects bound to it; otherwise, <c>false</c>.</returns>
        public static bool HasIndirectObject(this IVerbal verbal) => verbal.IndirectObjects.Any();

        /// <summary>
        /// Return a value indicating if the Verbal has any indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <param name="predicate">The predicate to match indirect objects. </param>
        /// <returns><c>true</c> if the Verbal has any indirect objects bound to it which match the given predicate function; otherwise, <c>false</c>.</returns>
        public static bool HasIndirectObject(this IVerbal verbal, Func<IEntity, bool> predicate) => HasBoundEntity(verbal.IndirectObjects, predicate);

        /// <summary>
        /// Return a value indicating if the Verbal has any direct OR indirect objects bound to it.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <returns><c>true</c> if the Verbal has any direct OR indirect objects bound to it; otherwise, <c>false</c>.</returns>
        public static bool HasObject(this IVerbal verbal) => verbal.HasDirectObject() || verbal.HasIndirectObject();

        /// <summary>
        /// Return a value indicating if the Verbal has any direct OR indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <param name="predicate">The predicate to test match objects.</param>
        /// <returns><c>true</c> if the Verbal has any direct OR indirect objects bound to it which match the given predicate function; otherwise, <c>false</c>.</returns>
        public static bool HasObject(this IVerbal verbal, Func<IEntity, bool> predicate) => verbal.HasDirectObject(predicate) || verbal.HasIndirectObject(predicate);

        /// <summary>
        /// Gets a value indicating if the Verbal has at least one subject, direct object, or indirect object.
        /// </summary>
        /// <param name="verbal">The verbal to test.</param>
        /// <returns><c>true</c> if the Verbal has at least one subject, direct object, or indirect object; otherwise, <c>false</c>.</returns>
        public static bool HasSubjectOrObject(this IVerbal verbal) => verbal.HasObject() || verbal.HasSubject();

        /// <summary>
        /// Gets a value indicating if the Verbal has at least one subject, direct object, or indirect object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each associated subject, direct object, or indirect object.</param>
        /// <param name="verbal">The Verbal to test.</param>
        /// <returns><c>true</c> if the Verbal has at least one subject, direct object, or indirect object  matching the provided predicate; otherwise, <c>false</c>.</returns>
        public static bool HasSubjectOrObject(this IVerbal verbal, Func<IEntity, bool> predicate) =>
             verbal.HasObject(predicate) || verbal.HasSubject(predicate);


        private static bool HasBoundEntity(IEnumerable<IEntity> entities, Func<IEntity, bool> predicate) =>
            entities.Any(predicate) || entities.OfReferencer().Any(r => r.RefersTo != null && predicate(r.RefersTo));
    }
}