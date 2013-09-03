using System;
namespace LASI.Algorithm.Patternization
{
    public interface IPredicatedPatternMatching<T, R>
      where T : class, LASI.Algorithm.ILexical
    {
        IPatternMatching<T, R> Then<TCase>(R caseResult) where TCase : class, T;
        IPatternMatching<T, R> Then<TCase>(Func<R> func) where TCase : class, T;
        IPatternMatching<T, R> Then<TCase>(Func<TCase, R> func) where TCase : class, T;
    }
    public interface IPredicatedPatternMatching<T>
    where T : class, LASI.Algorithm.ILexical
    {
        IPatternMatching<T> Then<TCase>(Action actn) where TCase : class, T;
        IPatternMatching<T> Then<TCase>(Action<TCase> actn) where TCase : class, T;
    }
}
