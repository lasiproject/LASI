using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>True if the Verbal has any Subjects bound to it; otherwise, false.</returns>
        public static bool HasSubject(this IVerbal verbal) {
            return verbal.Subjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any subjects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verbal has any subjects bound to it which match the given predicate function; otherwise, false.</returns>
        public static bool HasSubject(this IVerbal verbal, Func<IEntity, bool> predicate) {
            return HasBoundEntitiy(verbal.Subjects, predicate);
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any direct objects bound to it.
        /// </summary>
        /// <returns>True if the Verbal has any direct objects bound to it; otherwise, false.</returns>
        public static bool HasDirectObject(this IVerbal verbal) {
            return verbal.DirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any direct objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verbal has any direct objects bound to it which match the given predicate function; otherwise, false.</returns>
        public static bool HasDirectObject(this IVerbal verbal, Func<IEntity, bool> predicate) {
            return HasBoundEntitiy(verbal.DirectObjects, predicate);
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verbal has any direct objects bound to it; otherwise, false.</returns>
        public static bool HasIndirectObject(this IVerbal verbal) {
            return verbal.IndirectObjects.Any();
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verbal has any indirect objects bound to it which match the given predicate function; otherwise, false.</returns>
        public static bool HasIndirectObject(this IVerbal verbal, Func<IEntity, bool> predicate) {
            return HasBoundEntitiy(verbal.IndirectObjects, predicate);
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any direct OR indirect objects bound to it.
        /// </summary>
        /// <returns>True if the Verbal has any direct OR indirect objects bound to it; otherwise, false.</returns>
        public static bool HasObject(this IVerbal verbal) {
            return verbal.HasDirectObject() || verbal.HasIndirectObject();
        }
        /// <summary>
        /// Return a value indicating if the Verbal has any direct OR indirect objects bound to it which match the given predicate function.
        /// </summary>
        /// <returns>True if the Verbal has any direct OR indirect objects bound to it which match the given predicate function; otherwise, false.</returns>
        public static bool HasObject(this IVerbal verbal, Func<IEntity, bool> predicate) {
            return verbal.HasDirectObject(predicate) || verbal.HasIndirectObject(predicate);
        }

        /// <summary>
        /// Gets a value indicating if the Verbal has at least one subject, direct object, or indirect object.
        /// </summary>
        /// <returns>True if the Verbal has at least one subject, direct object, or indirect object; otherwise, false.</returns>
        public static bool HasSubjectOrObject(this IVerbal verbal) {
            return verbal.HasObject() || verbal.HasSubject();
        }
        /// <summary>
        /// Gets a value indicating if the Verbal has at least one subject, direct object, or indirect object matching the provided predicate.
        /// </summary>
        /// <param name="predicate">A predicate to test each associated subject, direct object, or indirect object..</param>
        /// <param name="verbal">The Verbal to test.</param>
        /// <returns>True if the Verbal has at least one subject, direct object, or indirect object  matching the provided predicate; otherwise, false.</returns>
        public static bool HasSubjectOrObject(this IVerbal verbal, Func<IEntity, bool> predicate) {
            return verbal.HasObject(predicate) || verbal.HasSubject(predicate);
        }
        private static bool HasBoundEntitiy(IEnumerable<IEntity> entities, Func<IEntity, bool> predicate) {
            return entities.Any(predicate) || entities.OfType<IReferencer>().Any(r => r.RefersTo != null && predicate(r.RefersTo));
        }
    }
}