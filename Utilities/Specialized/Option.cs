using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.Specialized.Option
{
    /// <summary>Provides static facilities for the creation of optional values.</summary>
    public static class Option
    {
        /// <summary>Lifts the given <paramref name="value" /> into an Option&lt; <typeparamref name="T" />&gt;.</summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="value">The value to lift into an Enumerable.</param>
        /// <returns>
        /// A singleton sequence containing the specified element or en empty sequence if the
        /// element is <c>null</c>.
        /// </returns>
        public static Option<T> ToOption<T>(this T value) => Option<T>.FromValue(value);
        /// <summary>Performs an identity projection on the given <see cref="Option{T}" />.</summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="option">The <see cref="Option{T}" />.</param>
        /// <returns>
        /// A singleton sequence containing the specified element or en empty sequence if the
        /// element is <c>null</c>.
        /// </returns>
        public static Option<T> ToOption<T>(this Option<T> option) => option;
        /// <summary>Returns the None Option value for the given type.</summary>
        /// <typeparam name="T">The type for which to retrieve the corresponding None option.</typeparam>
        /// <returns>The None Option value for the given type.</returns>
        public static Option<T> None<T>() => Option<T>.NoneOfT;
        /// <summary>
        /// Creates an Option&lt; <typeparamref name="T" />&gt; which wraps the given value. Returns
        /// an Option of Some&lt; <typeparamref name="T" />&gt; if <paramref name="value" /> is not
        /// <c>null</c>; otherwise, the None&lt; <typeparamref name="T" />&gt; singleton.
        /// </summary>
        /// <typeparam name="T">The type of the value being wrapped.</typeparam>
        /// <param name="value">The value to wrap.</param>
        /// <returns>
        /// An Option of Some&lt; <typeparamref name="T" />&gt; if <paramref name="value" /> is not
        /// <c>null</c>; otherwise, the None&lt; <typeparamref name="T" />&gt; singleton.
        /// </returns>
        public static Option<T> Create<T>(T value) => Option<T>.FromValue(value);
    }
    /// <summary>A class used to represent a value which may or may not exist.</summary>
    /// <remarks>
    /// Instances of this type can be used to represent values whose presence or absense does impact
    /// validity. That is, they not may or may not be specified. Instances of this type either hold
    /// a value of type <typeparamref name="T" /> or ARE the singleton <see cref="None" /> instance
    /// for their respective <see cref="Option{T}" />.
    /// </remarks>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    public abstract class Option<T> : IEnumerable<T>, IEquatable<Option<T>>, IEquatable<T>, IEnumerable
    {
        /// <summary>
        /// Transforms the <see cref="Option" />&lt;T&gt; into an <see cref="Option" />&lt;
        /// <typeparamref name="TResult" />&gt; by applying the given projection to the Option's
        /// value if present, or returning the <see cref="Option" />&lt;
        /// <typeparamref name="TResult" />&gt;. <see cref="None" /> if is it is not.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public abstract Option<TResult> Select<TResult>(Func<T, TResult> selector);
        /// <summary>
        /// Projects the option by invoking the specified selector function on its value and
        /// flattening the result into an Option&lt; <typeparamref name="TResult" />&gt;.
        /// </summary>
        /// <typeparam name="TResult">
        /// The type of the value resulting from applying the projection to a value of type <typeparamref name="T" />.
        /// </typeparam>
        /// <param name="selector">The function to transform the value stored in the option.</param>
        /// <returns>
        /// An option containing the result of applying the given selector function to the value
        /// represented by the option.
        /// </returns>
        public abstract Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector);
        /// <summary>
        /// Projects the option by invoking the specified option selector function on its value to
        /// produce an intermediate optional value of type <typeparamref name="TOption" /> and then
        /// flattening the result into an Option&lt; <typeparamref name="TResult" />&gt; by applying
        /// the specified result selector function.
        /// </summary>
        /// <typeparam name="TOption">
        /// The type of the value resulting from applying the option selector function to a value of
        /// type <typeparamref name="T" />.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the value resulting from applying the result selector function to a value of
        /// type <typeparamref name="TOption" />.
        /// </typeparam>
        /// <param name="optionSelector">The function to transform the value stored in the option.</param>
        /// <param name="resultSelector">
        /// The function to transform the a value of type stored in the option.
        /// </param>
        /// <returns>
        /// An option containing the result of applying the given selector function to the value
        /// represented by the option.
        /// </returns>
        public abstract Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector, Func<T, TOption, TResult> resultSelector);
        /// <summary>
        /// Applies a predicate to the current option yielding an Option&lt;
        /// <typeparamref name="T" />&gt; that has a value if and only if the current option has a
        /// value and that value satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to test.</param>
        /// <returns>
        /// <c>true</c> if and only if the Option&lt; <typeparamref name="T" />&gt;'s value matches
        /// the provided predicate; otherwise <c>false</c>.
        /// </returns>
        public abstract Option<T> Where(Func<T, bool> predicate);

        public abstract IEnumerable<TResult> Join<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector);
        public abstract IEnumerable<TResult> Join<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer);
        public abstract IEnumerable<TResult> GroupJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, IEnumerable<TInner>, TResult> resultSelector);
        public abstract IEnumerable<TResult> GroupJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer);

        /// <summary>
        /// Determines if the specified Option&lt; <typeparamref name="T" />&gt; is equal to the
        /// current instance.
        /// </summary>
        /// <param name="other">
        /// The Option&lt; <typeparamref name="T" />&gt; to compare with the current instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified Option&lt; <typeparamref name="T" />&gt; is equal to the
        /// current instance; otherwise <c>false</c>.
        /// </returns>
        public abstract bool Equals(Option<T> other);
        /// <summary>Determines if the specified object is equal to the current instance.</summary>
        /// <param name="obj">The object; to compare with the current instance.</param>
        /// <returns>
        /// <c>true</c> if the specified object is equal to the current instance; otherwise <c>false</c>.
        /// </returns>
        public abstract override bool Equals(object obj);
        /// <summary>Gets the hash code of the Option&lt; <typeparamref name="T" />&gt;.</summary>
        /// <returns>The hash code of the Option&lt; <typeparamref name="T" />&gt;.</returns>
        public abstract override int GetHashCode();
        /// <summary>
        /// Gets the value of the Option&lt; <typeparamref name="T" />&gt;. An
        /// <see cref="InvalidOperationException" /> will be thrown if the Option&lt;
        /// <typeparamref name="T" />&gt; does not have a value.
        /// </summary>
        public abstract T Value { get; }
        /// <summary>Gets a value indicating if the Option has a value.</summary>
        public abstract bool HasValue { get; }
        internal static Option<T> FromValue(T value) => value == null ? NoneOfT : new Some(value);
        /// <summary>
        /// The None case for Options representing a possible value of type <typeparamref name="T"/>
        /// </summary>
        /// <remarks>
        /// There is exactly on instance of None for every parameterization of <see cref="Option{T}"/>.
        /// Therefore, if a value of <see cref="Option{T}"/> is an instance of None. The following are equivalent:
        /// <list><item>
        /// value <c>is</c> <see cref="Option{T}.None"/></item><item>
        /// value.Equals<see cref="Option{T}.None"/> (or value <c>==</c> <see cref="Option{T}.None"/>)</item><item>
        /// <c>object.ReferenceEquals(value, <see cref="Option{T}.None"/>)</c></item>
        /// </list>
        /// </remarks>
        internal static readonly Option<T> NoneOfT = new None();
        /// <summary>
        /// When overriden in a derrived type, gets an enumerator which, if the option has a value,
        /// yields that value.
        /// </summary>
        /// <returns>An enumerator which, if the option has a value, yields that value.</returns>
        protected abstract IEnumerator<T> GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        /// <summary>
        /// Determines if the specified <typeparamref name="T" /> is equal to the value represented
        /// by the instance.
        /// </summary>
        /// <param name="other">The value to compare.</param>
        /// <returns>
        /// <c>true</c> if the sepecified value is equal to the value represented by the current
        /// instance; otherwise <c>false</c>.
        /// </returns>
        public abstract bool Equals(T other);

        /// <summary>Performs an equality comparison between two <see cref="Option{T}" /> instances.</summary>
        /// <param name="left">The first value to compare for equality.</param>
        /// <param name="right">The second value to compare for equality.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="Option{T}" /> values are equal; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The following table outlines the equality semantics for options representing optional
        /// values of the same type. Comparing options representing optional values of different
        /// types always results in <c>false</c>.
        /// <list type="table"><listheader><term>left</term><description>The option on the
        /// left.</description></listheader><listheader><term>right</term><description>The option on
        /// the right.</description></listheader><listheader><term>result.</term><c>true</c> or
        /// <c>false.</c></listheader><item>None</item><item>None</item><item><c>true</c></item><item>Some</item><item>None</item><item><c>false</c></item><item>Some</item><item>Some</item><item><c>true</c>
        /// if left.Value.Equals(right.Value); otherwise <c>false</c>.</item></list>
        /// </remarks>
        public static bool operator ==(Option<T> left, Option<T> right) => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);
        /// <summary>
        /// Performs an inequality comparison between two <see cref="Option{T}" /> instances.
        /// </summary>
        /// <param name="left">The first value to compare for equality.</param>
        /// <param name="right">The second value to compare for equality.</param>
        /// <returns>
        /// <c>true</c> if the two <see cref="Option{T}" /> values not are equal; otherwise <c>false</c>.
        /// </returns>
        /// <remarks>
        /// The following table outlines the equality semantics for options representing optional
        /// values of the same type. Comparing options representing optional values of different
        /// types always results in <c>false</c>.
        /// <list type="table"><listheader><term>left</term><description>The option on the
        /// left.</description></listheader><listheader><term>right</term><description>The option on
        /// the right.</description></listheader><listheader><term>result.</term><c>true</c> or
        /// <c>false.</c></listheader><item>None</item><item>None</item><item><c>true</c></item><item>Some</item><item>None</item><item><c>false</c></item><item>Some</item><item>Some</item><item><c>true</c>
        /// if left.Value.Equals(right.Value); otherwise <c>false</c>.</item></list>
        /// </remarks>
        public static bool operator !=(Option<T> left, Option<T> right) => !(left == right);
        /// <summary>
        /// Peforms an equality comparison between an <see cref="Option{T}" /> and a value of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="left">The option to compare.</param>
        /// <param name="right">The value to compare.</param>
        /// <returns>
        /// <c>true</c> if the option represents a value equal to <paramref name="right" />;
        /// otherwise <c>false</c>.
        /// </returns>
        public static bool operator ==(Option<T> left, T right) => left is Some && left.Value.Equals(right);
        /// <summary>
        /// Peforms an inequality comparison between an <see cref="Option{T}" /> and a value of type <typeparamref name="T" />.
        /// </summary>
        /// <param name="left">The option to compare.</param>
        /// <param name="right">The value to compare.</param>
        /// <returns>
        /// <c>true</c> if the option has no value or represents a value not equal to
        /// <paramref name="right" />; otherwise <c>false</c>.
        /// </returns>
        public static bool operator !=(Option<T> left, T right) => !(left == right);
        /// <summary>
        /// Peforms an equality comparison between a value of type <typeparamref name="T" /> and an <see cref="Option{T}" />.
        /// </summary>
        /// <param name="left">The value to compare.</param>
        /// <param name="right">The option to compare.</param>
        /// <returns>
        /// <c>true</c> if the option represents a value equal to <paramref name="left" />;
        /// otherwise <c>false</c>.
        /// </returns>
        public static bool operator ==(T left, Option<T> right) => right is None ? ReferenceEquals(left, null) : right is Some && right.Value.Equals(left);
        /// <summary>
        /// Peforms an inequality comparison between a value of type <typeparamref name="T" /> and
        /// an <see cref="Option{T}" />.
        /// </summary>
        /// <param name="left">The value to compare.</param>
        /// <param name="right">The option to compare.</param>
        /// <returns>
        /// <c>true</c> if the option has no value or represents a value not equal to
        /// <paramref name="left" />; otherwise <c>false</c>.
        /// </returns>
        public static bool operator !=(T left, Option<T> right) => !(left == right);
        /// <summary>
        /// Defines an implicit conversion from an <see cref="Option{T}" /> to an Option&lt;
        /// <see cref="Option{T}" /> Option&gt;.
        /// </summary>
        /// <param name="option">The option undergoing the conversion.</param>
        public static implicit operator Option<Option<T>>(Option<T> option) => option.ToOption();
        ///// <summary>
        ///// Defines an implicit conversion from an <see cref="Option{T}"/> to an Option&lt;<see cref="Option{T}"/>Option&gt;.
        ///// </summary>
        ///// <param name="option">The option undergoing the conversion.</param>
        public static implicit operator Option<T>(Option<Option<T>> option) => option.Value;

        /// <summary>
        /// Defines <see cref="Option{T}"/>s that do not have a value.
        /// </summary>
        /// <remarks>
        /// There is exactly on instance of None for every parameterization of <see cref="Option{T}"/>.
        /// Therefore, if a value of <see cref="Option{T}"/> is an instance of None. The following are equivalent:
        /// <list type="bullet"><item>
        /// value <c>is</c> <see cref="Option{T}.None"/></item><item>
        /// value.Equals<see cref="Option{T}.None"/> (or value <c>==</c> <see cref="Option{T}.None"/>)</item><item>
        /// <c>object.ReferenceEquals(value, <see cref="Option{T}.None"/>)</c></item>
        /// </list>
        /// </remarks>
        private sealed class None : Option<T>
        {
            public override Option<TResult> Select<TResult>(Func<T, TResult> selector) => Option<TResult>.NoneOfT;

            public override Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector) => Option<TResult>.NoneOfT;

            public override Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector,
                Func<T, TOption, TResult> resultSelector) => Option<TResult>.NoneOfT;

            public override Option<T> Where(Func<T, bool> predicate) => NoneOfT;
            public override IEnumerable<TResult> Join<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector) => Option<TResult>.NoneOfT;
            public override IEnumerable<TResult> Join<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer) => Option<TResult>.NoneOfT; public override IEnumerable<TResult> GroupJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, IEnumerable<TInner>, TResult> resultSelector) => Option<TResult>.NoneOfT;
            public override IEnumerable<TResult> GroupJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer) => Option<TResult>.NoneOfT;
            public override bool Equals(Option<T> other) => other == NoneOfT;

            public override bool Equals(object obj) => obj is None || obj is Option<None>;
            public override bool Equals(T other) => false;
            public override int GetHashCode() => 0;

            public override T Value { get { throw new InvalidOperationException("None does not have a value."); } }

            public override bool HasValue => false;

            protected override IEnumerator<T> GetEnumerator() { yield break; }
        }

        private sealed class Some : Option<T>
        {
            public override Option<TResult> Select<TResult>(Func<T, TResult> selector) => new Option<TResult>.Some(selector(Value));

            public override Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector) => selector(Value);

            public override Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector,
                Func<T, TOption, TResult> resultSelector) => optionSelector(Value).Select(resultSelector.Apply(Value));

            public override Option<T> Where(Func<T, bool> predicate) => predicate(Value) ? new Some(Value) : NoneOfT;

            public override bool Equals(Option<T> other) => other is Some && other.Value.Equals(Value);
            public override bool Equals(T other) => Value.Equals(other);

            public override bool Equals(object obj) =>
                obj is Some ?
                Equals((Some)obj) :
                obj is Option<Option<T>>.Some ?
                Equals(((Option<Option<T>>.Some)obj).Value, obj) :
                obj is T ? Equals((T)obj) : false;

            public override int GetHashCode() => Value.GetHashCode();

            public override bool HasValue => true;

            public override T Value { get; }
            protected override IEnumerator<T> GetEnumerator() { yield return Value; }

            public override IEnumerable<TResult> Join<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector)
            {
                return from value in this join i in inner on outerKeySelector(value) equals innerKeySelector(i) select resultSelector(value, i);
            }
            public override IEnumerable<TResult> Join<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
            {
                foreach (var i in inner)
                {
                    if (comparer.Equals(outerKeySelector(Value), innerKeySelector(i)))
                    {
                        yield return resultSelector(Value, i);
                    }
                }
            }
            public override IEnumerable<TResult> GroupJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, IEnumerable<TInner>, TResult> resultSelector)
            {
                return Join(inner, outerKeySelector, innerKeySelector, (x, y) => resultSelector(x, y.ToOption()));
            }
            public override IEnumerable<TResult> GroupJoin<TInner, TKey, TResult>(IEnumerable<TInner> inner, Func<T, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<T, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer) =>
                Join(inner, outerKeySelector, innerKeySelector, (x, y) => resultSelector(x, y.ToOption()), comparer);

            internal Some(T value) { Value = value; }

            internal Some(Some other) { Value = other.Value; }
        }
    }
}