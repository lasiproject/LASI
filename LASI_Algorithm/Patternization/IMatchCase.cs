using System;
namespace LASI.Algorithm.Patternization
{
    public interface IMatchCase<T>
       where T : class, LASI.Algorithm.ILexical
    {
        void Default(Action action);
        void Default(Action<T> action);
        IMatchCase<T> With<TCase>(Action action) where TCase : class, LASI.Algorithm.ILexical;
        IMatchCase<T> With<TCase>(Action<TCase> action) where TCase : class, LASI.Algorithm.ILexical;
        IMatchCase<T> With<TCase>(Func<TCase, bool> condition, Action action) where TCase : class, LASI.Algorithm.ILexical;
        IMatchCase<T> With<TCase>(Func<TCase, bool> condition, Action<TCase> action) where TCase : class, LASI.Algorithm.ILexical;
    }
}
