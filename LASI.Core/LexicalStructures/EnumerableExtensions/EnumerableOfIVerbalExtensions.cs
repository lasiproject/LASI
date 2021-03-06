﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core
{
    /// <summary>
    /// Defines extension methods for sequences of objects implementing the IVerbal interface.
    /// </summary>
    /// <seealso cref="IVerbal"/>
    public static partial class LexicalEnumerable
    {
        /// <summary>
        /// Creates an <see cref="IAggregateVerbal"/> from the sequence of verbals.
        /// </summary>
        /// <param name="verbals">The sequence of verbals to aggregate.</param>
        /// <returns>An <see cref="IAggregateVerbal"/> formed from the sequence of verbals.</returns>
        public static IAggregateVerbal ToAggregate(this IEnumerable<IVerbal> verbals) => new AggregateVerbal(verbals);

        #region Sequential Implementations

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound
        /// direct object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal instances to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one direct object.</returns>
        public static IEnumerable<TVerbal> WithDirectObject<TVerbal>(this IEnumerable<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasDirectObject()
                                       select verbal;

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one direct
        /// object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">
        /// The function specifying the match verbalSelector. Any function which takes an IEntity
        /// and return a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one direct object which matches the condition.
        /// </returns>
        public static IEnumerable<TVerbal> WithDirectObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => from verbal in verbals.WithDirectObject()
                                       where verbal.HasDirectObject(condition)
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound
        /// indirect object.
        /// </summary>
        /// <param name="verbals">The Enumerable of Verb objects to filter.</param>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <returns>The subset of IVerbal constructs bound to an indirect object.</returns>
        public static IEnumerable<TVerbal> WithIndirectObject<TVerbal>(this IEnumerable<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasIndirectObject()
                                       select verbal;

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one
        /// indirect object which matches the provided object testing function
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="condition">
        /// The function specifying the criteria an entity bound to the Verbal must match for it to
        /// be included. Any function which takes an IEntity and return a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one indirect object which matches the verbalSelector.
        /// </returns>
        public static IEnumerable<TVerbal> WithIndirectObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => from verbal in verbals.WithIndirectObject()
                                       where verbal.HasIndirectObject(condition)
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound
        /// direct OR indirect object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal instances to filter.</param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one bound direct OR indirect object.
        /// </returns>
        public static IEnumerable<TVerbal> WithObject<TVerbal>(this IEnumerable<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasObject()
                                       select verbal;

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one bound
        /// direct OR indirect object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">
        /// The function specifying the match verbalSelector. Any function which takes an IEntity
        /// and return a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one bound direct OR indirect object
        /// which matches the condition.
        /// </returns>
        public static IEnumerable<TVerbal> WithObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => from verbal in verbals.WithObject()
                                       where verbal.HasObject(condition)
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound subject.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of T instances to filter.</param>
        /// <returns>The subset bound to some subject.</returns>
        public static IEnumerable<TVerbal> WithSubject<TVerbal>(this IEnumerable<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasSubject()
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs returning those who have at least one subject
        /// matching the provided subject testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">The function used to match subjects of the verbal.</param>
        /// <returns>All verbals whose subject match the verbalSelector.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example>
        /// Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject(N =&gt; N.Text == "banana");
        /// </code>
        /// </example>
        /// <remarks>
        /// This provided function is used to filter the IVerbal constructs based on their subjects.
        /// </remarks>
        public static IEnumerable<TVerbal> WithSubject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => from verbal in verbals.WithSubject()
                                       where verbal.HasSubject(condition)
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one subject,
        /// direct object, or indirect object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of Verb objects to filter.</param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one subject, direct object, or
        /// indirect object.
        /// </returns>
        public static IEnumerable<TVerbal> WithSubjectOrObject<TVerbal>(this IEnumerable<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasSubject() || verbal.HasObject()
                                       select verbal;

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one
        /// subject, direct object, or indirect object that matches the provided IEntity testing function
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="condition">
        /// The function specifying the criteria an entity bound to the Verbal must match for it to
        /// be included. Any function which takes an IEntity and return a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one subject, direct object, or
        /// indirect object which matches the provided condition.
        /// </returns>
        public static IEnumerable<TVerbal> WithSubjectOrObject<TVerbal>(this IEnumerable<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => verbals.Where(verbal => verbal.HasSubjectOrObject(condition));

        #endregion Sequential Implementations

        #region Parallel Implementations

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound
        /// direct object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal instances to filter.</param>
        /// <returns>The subset of IVerbal constructs bound to at least one direct object.</returns>
        public static ParallelQuery<TVerbal> WithDirectObject<TVerbal>(this ParallelQuery<TVerbal> verbals)
            where TVerbal : IVerbal => verbals.Where(verbal => verbal.HasDirectObject());

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one direct
        /// object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">
        /// The function specifying the match verbalSelector. Any function which takes an IEntity
        /// and returns a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one direct object which matches the condition.
        /// </returns>
        public static ParallelQuery<TVerbal> WithDirectObject<TVerbal>(this ParallelQuery<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => from verbal in verbals.WithDirectObject()
                                       where verbal.HasDirectObject(condition)
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound
        /// indirect object.
        /// </summary>
        /// <param name="verbals">The Enumerable of Verb objects to filter.</param>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <returns>The subset of IVerbal constructs bound to an indirect object.</returns>
        public static ParallelQuery<TVerbal> WithIndirectObject<TVerbal>(this ParallelQuery<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasIndirectObject()
                                       select verbal;

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one
        /// indirect object which matches the provided object testing function
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="condition">
        /// The function specifying the criteria an entity bound to the Verbal must match for it to
        /// be included. Any function which takes an IEntity and return a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one indirect object which matches the verbalSelector.
        /// </returns>
        public static ParallelQuery<TVerbal> WithIndirectObject<TVerbal>(this ParallelQuery<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => from verbal in verbals.WithIndirectObject()
                                       where verbal.HasIndirectObject(condition)
                                       select verbal;

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound
        /// direct OR indirect object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal instances to filter.</param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one bound direct OR indirect object.
        /// </returns>
        public static ParallelQuery<TVerbal> WithObject<TVerbal>(this ParallelQuery<TVerbal> verbals)
            where TVerbal : IVerbal => verbals.Where(verbal => verbal.HasObject());

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one bound
        /// direct OR indirect object matching the provided object testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">
        /// The function specifying the match verbalSelector. Any function which takes an IEntity
        /// and returns a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one bound direct OR indirect object
        /// which matches the condition.
        /// </returns>
        public static ParallelQuery<TVerbal> WithObject<TVerbal>(this ParallelQuery<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => verbals.WithObject().Where(verbal => verbal.HasObject(condition));

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one bound subject.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of T instances to filter.</param>
        /// <returns>The subset bound to some subject.</returns>
        public static ParallelQuery<TVerbal> WithSubject<TVerbal>(this ParallelQuery<TVerbal> verbals)
            where TVerbal : IVerbal => verbals.Where(verbal => verbal.HasSubject());

        /// <summary>
        /// Filters the sequence of IVerbal constructs returning those who have at least one subject
        /// matching the provided subject testing function.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs to filter.</param>
        /// <param name="condition">
        /// The function specifying the match verbalSelector. Any function which takes an IEntity
        /// and return a bool.
        /// </param>
        /// <returns>All verbals whose subject match the verbalSelector.</returns>
        /// The argument may be either a named function or a lambda expression.
        /// <example>
        /// Demonstrates how to use this method.
        /// <code>
        /// var filtered = myVerbs.WithSubject(N =&gt; N.Text == "banana");
        /// </code>
        /// </example>
        /// <remarks>
        /// This provided function is used to filter the IVerbal constructs based on their subjects.
        /// </remarks>
        public static ParallelQuery<TVerbal> WithSubject<TVerbal>(this ParallelQuery<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => verbals.WithSubject().Where(verbal => verbal.HasSubject(condition));

        /// <summary>
        /// Filters the sequence of IVerbal constructs selecting those with at least one subject,
        /// direct object, or indirect object.
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of Verb objects to filter.</param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one subject, direct object, or
        /// indirect object.
        /// </returns>
        public static ParallelQuery<TVerbal> WithSubjectOrObject<TVerbal>(this ParallelQuery<TVerbal> verbals)
            where TVerbal : IVerbal => from verbal in verbals
                                       where verbal.HasSubject() || verbal.HasObject()
                                       select verbal;

        /// <summary>
        /// Filters a collection of IVerbal constructs returning those who have at least one
        /// subject, direct object, or indirect object that matches the provided IEntity testing function
        /// </summary>
        /// <typeparam name="TVerbal">Any Type which implements the IVerbal interface.</typeparam>
        /// <param name="verbals">The Enumerable of IVerbal constructs instances to filter.</param>
        /// <param name="condition">
        /// The function specifying the criteria an entity bound to the Verbal must match for it to
        /// be included. Any function which takes an IEntity and return a bool is compatible.
        /// </param>
        /// <returns>
        /// The subset of IVerbal constructs bound to at least one subject, direct object, or
        /// indirect object which matches the provided condition.
        /// </returns>
        public static ParallelQuery<TVerbal> WithSubjectOrObject<TVerbal>(this ParallelQuery<TVerbal> verbals, Func<IEntity, bool> condition)
            where TVerbal : IVerbal => verbals.Where(verbal => verbal.HasSubjectOrObject(condition));

        #endregion Parallel Implementations
    }
}