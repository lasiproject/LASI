using System;
namespace LASI.Algorithm.Patternization
{
    public interface IMatchCase<T>
       where T : class
    {
        void Default(Action action);
        void Default(Action<T> action);
        IMatchCase<T> With<TCase>(Action action) where TCase : class;
        IMatchCase<T> With<TCase>(Action<TCase> action) where TCase : class;
        IMatchCase<T> With<TCase>(Func<TCase, bool> condition, Action action) where TCase : class;
        IMatchCase<T> With<TCase>(Func<TCase, bool> condition, Action<TCase> action) where TCase : class;
    }
}
