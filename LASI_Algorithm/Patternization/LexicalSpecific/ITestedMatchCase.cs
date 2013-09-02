using System;
namespace LASI.Algorithm.Patternization
{
    public interface ITestedMatchCase<T, R>
      where T : class, LASI.Algorithm.ILexical
    {
        IMatchCase<T, R> With<TCase>(R caseResult) where TCase : class, LASI.Algorithm.ILexical;
        IMatchCase<T, R> With<TCase>(Func<R> func) where TCase : class, LASI.Algorithm.ILexical;
        IMatchCase<T, R> With<TCase>(Func<TCase, R> func) where TCase : class, LASI.Algorithm.ILexical;
    }
    public interface ITestedMatchCase<T>
    where T : class, LASI.Algorithm.ILexical
    {
        IMatchCase<T> With<TCase>(Action actn) where TCase : class, LASI.Algorithm.ILexical;
        IMatchCase<T> With<TCase>(Action<TCase> actn) where TCase : class,LASI.Algorithm.ILexical;
    }
}
