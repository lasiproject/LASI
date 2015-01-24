using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.SupportTypes
{
    /// <summary>
    /// Provides static facilities for the creation of optional values.
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// Lifts the given <paramref name="value" /> into an Option&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="value">The value to lift into an Enumerable.</param>
        /// <returns>A singleton sequence containing the specified element or en empty sequence if the element is <c>null</c>.</returns>
        public static Option<T> ToOption<T>(this T value) => Option<T>.FromValue(value);
        /// <summary>
        /// Performs an identity projection on the given <see cref="Option{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the value to lift.</typeparam>
        /// <param name="value">The <see cref="Option{T}" />.</param>
        /// <returns>A singleton sequence containing the specified element or en empty sequence if the element is <c>null</c>.</returns>
        public static Option<T> ToOption<T>(this Option<T> option) => option;
        /// <summary>
        /// Returns the None Option value for the given type.
        /// </summary>
        /// <typeparam name="T">The type for which to retrieve the corresponding None option.</typeparam>
        /// <returns>The None Option value for the given type.</returns>
        public static Option<T> None<T>() => Option<T>.NoneOfT;
        /// <summary>
        /// Creates an Option&lt;<typeparamref name="T"/>&gt; which wraps the given value. 
        /// Returns an Option of Some&lt;<typeparamref name="T"/>&gt; if <paramref name="value"/> is not <c>null</c>;
        /// otherwise, the None&lt;<typeparamref name="T"/>&gt; singleton.
        /// </summary>
        /// <typeparam name="T">The type of the value being wrapped.</typeparam>
        /// <param name="value">The value to wrap.</param>
        /// <returns>An Option of Some&lt;<typeparamref name="T"/>&gt; if <paramref name="value"/> is not <c>null</c>;
        /// otherwise, the None&lt;<typeparamref name="T"/>&gt; singleton.</returns>
        public static Option<T> Create<T>(T value) => Option<T>.FromValue(value);
    }
    /// <summary>
    /// A class used to represent a value which may or may not be present in a valid state.
    /// </summary>
    /// <remarks>Instances of this type can be used to represent values whose presence or absense does impact validity. 
    /// That is, they not may or may not be specified. Instances of this type either hold a value of type <typeparamref name="T"/> or ARE the singleton <see cref="None"/> instance for their respective <see cref="Option{T}"/>.
    /// </remarks>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    public abstract class Option<T> : IEnumerable<T>, IEquatable<Option<T>>
    {
        /// <summary>
        /// Transforms the <see cref="Option"/>&lt;T&gt; into an <see cref="Option"/>&lt;<typeparamref name="TResult"/>&gt; by applying the given projection to the Option's value if present,
        /// or returning the <see cref="Option"/>&lt;<typeparamref name="TResult"/>&gt;.<see cref="None"/> if is it is not.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public abstract Option<TResult> Select<TResult>(Func<T, TResult> selector);

        public abstract Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector);

        public abstract Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector, Func<T, TOption, TResult> resultSelector);
        /// <summary>
        /// Applies a predicate to the current option yielding an Option&lt;<typeparamref name="T"/>&gt; that has a value if and only if the current option
        /// has a value and that value satisfies the provided predicate.
        /// </summary>
        /// <param name="predicate">The predicate to test.</param>
        /// <returns>
        /// <c>true</c> if and only if the Option&lt;<typeparamref name="T"/>&gt;'s value matches the provided predicate; otherwise <c>false</c>.
        /// </returns>
        public abstract Option<T> Where(Func<T, bool> predicate);
        /// <summary>
        /// Determines if the specified Option&lt;<typeparamref name="T"/>&gt; is equal to the current instance.
        /// </summary>
        /// <param name="other">The Option&lt;<typeparamref name="T"/>&gt; to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified Option&lt;<typeparamref name="T"/>&gt; is equal to the current instance; otherwise <c>false</c>.</returns>
        public abstract bool Equals(Option<T> other);

        public abstract override bool Equals(object obj);
        /// <summary>
        /// Gets the hash code of the Option&lt;<typeparamref name="T"/>&gt;.
        /// </summary>
        /// <returns>The hash code of the Option&lt;<typeparamref name="T"/>&gt;.</returns>
        public abstract override int GetHashCode();
        /// <summary>
        /// Gets the value of the Option&lt;<typeparamref name="T"/>&gt;. An <see cref="InvalidOperationException" /> will be thrown if the Option&lt;<typeparamref name="T"/>&gt; does not have a value.
        /// </summary>
        public abstract T Value { get; }
        /// <summary>
        /// Gets a value indicating if the Option has a value.
        /// </summary>
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
        ///</list>
        /// </remarks>
        internal static readonly Option<T> NoneOfT = new None();
        protected abstract IEnumerator<T> GetEnumerator();
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static bool operator ==(Option<T> left, Option<T> right) => ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.Equals(right);

        public static bool operator !=(Option<T> left, Option<T> right) => !(left == right);

        public static bool operator ==(Option<T> left, Option<Option<T>> right) => right.HasValue ? (left == right.Value) : !left.HasValue;

        public static bool operator !=(Option<T> left, Option<Option<T>> right) => !(left == right);

        public static bool operator ==(Option<Option<T>> left, Option<T> right) => right == left;

        public static bool operator !=(Option<Option<T>> left, Option<T> right) => !(left == right);

        public static bool operator ==(Option<T> left, T right) => left is Some && left.Value.Equals(right);

        public static bool operator !=(Option<T> left, T right) => !(left == right);

        public static bool operator ==(T left, Option<T> right) => right is Some && right.Value.Equals(left);

        public static bool operator !=(T left, Option<T> right) => !(left == right);
        /// <summary>
        /// Represents <see cref="Option{T}"/>s that do not have a value.
        /// </summary>
        /// <remarks>
        /// There is exactly on instance of None for every parameterization of <see cref="Option{T}"/>.
        /// Therefore, if a value of <see cref="Option{T}"/> is an instance of None. The following are equivalent:
        /// <list><item>
        /// value <c>is</c> <see cref="Option{T}.None"/></item><item>
        /// value.Equals<see cref="Option{T}.None"/> (or value <c>==</c> <see cref="Option{T}.None"/>)</item><item>
        /// <c>object.ReferenceEquals(value, <see cref="Option{T}.None"/>)</c></item>
        ///</list>
        /// </remarks>
        private sealed class None : Option<T>
        {
            public override Option<TResult> Select<TResult>(Func<T, TResult> selector) => Option<TResult>.NoneOfT;

            public override Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector) => Option<TResult>.NoneOfT;

            public override Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector,
                Func<T, TOption, TResult> resultSelector) => Option<TResult>.NoneOfT;

            public override Option<T> Where(Func<T, bool> predicate) => NoneOfT;

            public override bool Equals(Option<T> other) => other == NoneOfT;

            public override bool Equals(object obj) => obj is None;

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

            public override bool Equals(object obj) => obj is Some && Equals((Some)obj);

            public override int GetHashCode() => Value.GetHashCode();

            public override bool HasValue => true;

            public override T Value { get; }
            protected override IEnumerator<T> GetEnumerator() { yield return Value; }

            internal Some(T value) { Value = value; }

            internal Some(Some other) { Value = other.Value; }
        }
    }
}