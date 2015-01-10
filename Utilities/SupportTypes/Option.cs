using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Utilities.SupportTypes
{
    public static class Option
    {
        public static Option<T> Lift<T>(this T value) {
            return Option<T>.FromValue(value);
        }

        public static Option<T> Lift<T>(this Option<T> option) => option;
    }

    public abstract class Option<T> : IEnumerable<T>, IEquatable<Option<T>>
    {
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

        public abstract Option<TResult> Select<TResult>(Func<T, TResult> selector);

        public abstract Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector);

        public abstract Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector, Func<T, TOption, TResult> resultSelector);

        public abstract Option<T> Where(Func<T, bool> predicate);

        public abstract bool Equals(Option<T> other);

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();

        public abstract T Value { get; }

        public abstract bool HasValue { get; }

        internal static Option<T> FromValue(T value) => value == null ? NoneInstance : new Some(value);

        internal readonly static Option<T> NoneInstance = new None();

        protected abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        private sealed class None : Option<T>
        {
            public override Option<TResult> Select<TResult>(Func<T, TResult> selector) => Option<TResult>.NoneInstance;

            public override Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector) => Option<TResult>.NoneInstance;

            public override Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector, Func<T, TOption, TResult> resultSelector) => Option<TResult>.NoneInstance;

            public override Option<T> Where(Func<T, bool> predicate) => NoneInstance;

            public override bool Equals(Option<T> other) => other is None;

            public override bool Equals(object obj) => obj is None;

            public override int GetHashCode() => 0;

            public override T Value { get { throw new InvalidOperationException("No Value"); } }

            public override bool HasValue => false;

            protected override IEnumerator<T> GetEnumerator() { yield break; }
        }

        private sealed class Some : Option<T>
        {
            public override Option<TResult> Select<TResult>(Func<T, TResult> selector) => new Option<TResult>.Some(selector(Value));

            public override Option<TResult> SelectMany<TResult>(Func<T, Option<TResult>> selector) => selector(Value);

            public override Option<TResult> SelectMany<TResult, TOption>(Func<T, Option<TOption>> optionSelector, Func<T, TOption, TResult> resultSelector) =>
                optionSelector(Value).Select(resultSelector.Apply(Value));

            public override Option<T> Where(Func<T, bool> predicate) => predicate(Value) ? new Some(Value) : NoneInstance;

            public override bool Equals(Option<T> other) => other is Some && other.Value.Equals(Value);

            public override bool Equals(object obj) => obj is Some && Equals((Some)obj);

            public override int GetHashCode() => Value.GetHashCode();

            public override bool HasValue => true;

            public override T Value { get; }

            internal Some(T value) { Value = value; }

            internal Some(Some other) { Value = other.Value; }

            protected override IEnumerator<T> GetEnumerator() { yield return Value; }
        }
    }
}