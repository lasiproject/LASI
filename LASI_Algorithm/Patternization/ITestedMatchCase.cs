using System;
namespace LASI.Algorithm.Patternization
{
    public interface ITestedMatchCase<T, R>
      where T : class, LASI.Algorithm.ILexical
    {
        MatchCase<T, R> With<TCase>(R caseResult) where TCase : class, LASI.Algorithm.ILexical;
        MatchCase<T, R> With<TCase>(Func<R> func) where TCase : class, LASI.Algorithm.ILexical;
        MatchCase<T, R> With<TCase>(Func<TCase, R> func) where TCase : class, LASI.Algorithm.ILexical;
    }
}
