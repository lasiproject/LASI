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
        public static LexicalToken<TLexical> operator &(LexicalToken<TLexical> token, bool guard)
        {
            token.guardFailed = !guard; return token;
        }
        public static LexicalToken<TLexical> operator &(LexicalToken<TLexical> token, Func<TLexical, bool> guard)
        {
            token.guardFailed = !guard(default(TLexical)); return token;
        }
        public static Pattern<object>.TokenTestResult<TLexical> operator |(Pattern<object> tokenTestResult, LexicalToken<TLexical> token) => new Pattern<dynamic>.TokenTestResult<TLexical>(true, tokenTestResult);
        public static Pattern<object>.TokenTestResult<TLexical> operator |(LexicalToken<TLexical> token, Pattern<object> tokenTestResult) => new Pattern<dynamic>.TokenTestResult<TLexical>(true, tokenTestResult);

        abstract class ValueToken<TValue> : Token
        {
            private readonly TValue value;

            protected ValueToken(TValue value)
            {
                this.value = value;
            }
            public override bool AppliesTo<TCase>(TCase value) => this.value.Equals(value);
            public static Pattern<object>.TokenTestResult<ILexical> operator |(Pattern<object> pattern, ValueToken<TValue> token) => new Pattern<object>.TokenTestResult<ILexical>(false, pattern);
        }

        class TextToken : ValueToken<string>
        {
            public TextToken(string value) : base(value)
            {
            }
            public static implicit operator TextToken(string text) => new TextToken(text);
        }
        class PhraseToken : LexicalToken<Phrase> { }
        class WordToken : LexicalToken<Word> { }
        class VerbalToken : LexicalToken<IVerbal> { }
        class EntityToken : LexicalToken<IEntity> { }
        class ReferencerToken : LexicalToken<IReferencer> { }

        public class Pattern<TResult>
        {
            private TResult result = default(TResult);
            private readonly ILexical value;

            public Pattern(ILexical value)
            {
                this.value = value;
            }
            public class TokenTestResult<TCase> where TCase : class, ILexical
            {
                private bool matched;
                private Pattern<TResult> pattern;
                private Func<TResult> deferred;

                public TokenTestResult(bool matched, Pattern<TResult> pattern)
                {
                    this.matched = matched;
                    this.pattern = pattern;
                }

                public static Pattern<TResult> operator |(TokenTestResult<TCase> tokenTestResult, Func<TResult> f)
                {
                    if (tokenTestResult.matched)
                    {
                        tokenTestResult.matched = true;
                        tokenTestResult.pattern.result = f();
                    }
                    return tokenTestResult.pattern;
                }

                public static Pattern<TResult> operator |(TokenTestResult<TCase> tokenTestResult, Func<TCase, TResult> f)
                {
                    if (tokenTestResult.matched)
                    {
                        tokenTestResult.matched = true;
                        tokenTestResult.deferred = () => tokenTestResult.pattern.result = f(tokenTestResult.pattern.value as TCase);
                    }
                    return tokenTestResult.pattern;
                }
                public static TokenTestResult<TCase> operator &(TokenTestResult<TCase> tokenTestResult, Func<TCase, bool> f)
                {
                    if (tokenTestResult.matched)
                    {
                        tokenTestResult.matched = f(tokenTestResult.pattern.value as TCase);
                    }
                    if (tokenTestResult.matched) { tokenTestResult.pattern.result = tokenTestResult.deferred(); }
                    return tokenTestResult;
                }
                //public static TokenTestResult<TCase> operator |(TokenTestResult<TCase> pattern, TextToken token) {
                //    return new TokenTestResult<TCase>(token.AppliesTo(pattern.ToString()), pattern.pattern);
                //}
                //public static TokenTestResult<TCase> operator &(TokenTestResult<TCase> pattern, TextToken token) {
                //    return new TokenTestResult<TCase>(token.AppliesTo(pattern.ToString()), pattern.pattern);
                //}
            }
            static VerbalToken Verbal = new VerbalToken();
            static EntityToken Entity = new EntityToken();
            //static class PatternTestingTokenTestingTest
            //{

            //    static void Test1(IVerbal verbal) {
            //        var textToken = new TextToken("test");
            //        var f = new Func<bool>(() => false);
            //        var pattern = new Pattern<string>(verbal);

            //        var x = pattern
            //            | Verbal | (v => v.Text)
            //            | textToken | (() => "")
            //            | Entity | ((e) => e.Text)
            //            | Verbal | (v => false.ToString())
            //            | (() => true.ToString());
            //    }
            //}
        }
    }
}