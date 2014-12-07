using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core.Analysis.PatternMatching.LexicalSpecific.Experimental.TermBased
{
    abstract class Token
    {
        public abstract bool AppliesTo<TCase>(TCase value);
    }
    abstract class LexicalToken : Token
    {
    }
    abstract class LexicalToken<TLexical> : LexicalToken where TLexical : class, ILexical
    {
        private bool guardFailed;

        public override bool AppliesTo<TCase>(TCase value) => !guardFailed && value is TLexical;
        public static LexicalToken<TLexical> operator &(LexicalToken<TLexical> token, bool guard) { token.guardFailed = !guard; return token; }
        public static LexicalToken<TLexical> operator &(LexicalToken<TLexical> token, Func<bool> guard) { token.guardFailed = !guard(); return token; }
    }
    abstract class ValueToken<TValue> : Token
    {
        private readonly TValue value;

        protected ValueToken(TValue value) {
            this.value = value;
        }
        public override bool AppliesTo<TCase>(TCase value) => this.value.Equals(value);
    }

    class TextToken : ValueToken<string>
    {
        public TextToken(string value) : base(value) {
        }
        public static implicit operator TextToken(string text) => new TextToken(text);
    }
    class PhraseToken : LexicalToken<Phrase> { }
    class WordToken : LexicalToken<Word> { }
    class VerbalToken : LexicalToken<IVerbal> { }
    class EntityToken : LexicalToken<IEntity> { }
    class ReferencerToken : LexicalToken<IReferencer>
    {

    }

    class Pattern<TResult>
    {
        private TResult result = default(TResult);
        private readonly ILexical value;

        public Pattern(ILexical value) {
            this.value = value;
        }

        public static TokenTestResult operator |(Pattern<TResult> pattern, TextToken token) {
            return new TokenTestResult(matched: token.AppliesTo(pattern.value.Text), pattern: pattern);
        }
        public static TokenTestResult operator |(Pattern<TResult> pattern, LexicalToken token) {
            return new TokenTestResult(matched: token.AppliesTo(pattern.value.Text), pattern: pattern);
        }
        public static TResult operator |(Pattern<TResult> pattern, Func<TResult> f) {
            return f();
        }

        public class TokenTestResult
        {
            private bool matched;
            private Pattern<TResult> pattern;
            private Func<TResult> deferred;

            public TokenTestResult(bool matched, Pattern<TResult> pattern) {
                this.matched = matched;
                this.pattern = pattern;
            }

            public static Pattern<TResult> operator |(TokenTestResult tokenTestResult, Func<TResult> f) {
                if (tokenTestResult.matched) {
                    tokenTestResult.matched = true;
                    tokenTestResult.pattern.result = f();
                }
                return tokenTestResult.pattern;
            }
            public static Pattern<TResult> operator |(TokenTestResult tokenTestResult, Func<ILexical, TResult> f) {
                if (tokenTestResult.matched) {
                    tokenTestResult.matched = true;
                    tokenTestResult.deferred = () => tokenTestResult.pattern.result = f(tokenTestResult.pattern.value);
                }
                return tokenTestResult.pattern;
            }
            public static Pattern<TResult> operator &(TokenTestResult tokenTestResult, Func<ILexical, bool> f) {
                if (tokenTestResult.matched) {
                    tokenTestResult.matched = f(tokenTestResult.pattern.value);
                }
                if (tokenTestResult.matched) { tokenTestResult.pattern.result = tokenTestResult.deferred(); }
                return tokenTestResult.pattern;
            }
        }
    }
    static class PatternTestingTokenTestingTest
    {
        public static readonly VerbalToken Verbal = new VerbalToken();
        public static readonly EntityToken Entity = new EntityToken();
        public static readonly ReferencerToken Referencer = new ReferencerToken();
        static void Test1(IVerbal verbal) {
            var textToken = new TextToken("test");
            var f = new Func<bool>(() => false);
            var pattern = new Pattern<bool>(verbal);

            var x = pattern
                | Verbal & verbal.Text == "abc" & (() => 1 == 1) | (() => false)
                | textToken | (() => true)
                | Entity | (() => 1 == 4)
                | Verbal | (v => false)
                | (() => true);
        }
    }
}
