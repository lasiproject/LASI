using System;

namespace LASI.Utilities
{
    public interface IOption<out T>
    {
        bool IsNone { get; }
        bool IsSome { get; }
        T Value { get; }

        //bool Equals(T other);
        //bool Equals(IOption<T> other);
        bool Equals(object obj);
        int GetHashCode();
        IOption<TResult> Select<TResult>(Func<T, TResult> selector);
        IOption<TResult> SelectMany<TResult>(Func<T, IOption<TResult>> selector);
        IOption<TResult> SelectMany<TResult, TOption>(Func<T, IOption<TOption>> optionSelector, Func<T, TOption, TResult> resultSelector);
        IOption<T> Where(Func<T, bool> predicate);
    }
}