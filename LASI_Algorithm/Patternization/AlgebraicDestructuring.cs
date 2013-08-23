using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Algorithm.TraitWiseDecomposition
{
    public interface IDecomposition<TComplex> where TComplex : class,  LASI.Algorithm.ILexical
    {

    }
    public interface IDecomposition<TComplex, TResult> : IDecomposition<TComplex> where TComplex : class,  LASI.Algorithm.ILexical
    {
        IEnumerable<TResult> Results { get; }
    }
    public struct Decomposition : IDecomposition<ILexical>
    {
        public Decomposition(ILexical toDecompose)
            : this() {
            value = toDecompose;
            typesMatched = new HashSet<Type>();
        }
        public Decomposition OnTrait<TTrait>(Action processComponent) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    processComponent();
                }
            }
            return this;
        }
        public Decomposition OnTrait<TTrait>(Action<TTrait> processComponent) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    processComponent(component);
                }
            }
            return this;
        }
        public IDecomposition<ILexical> OnBase(Action processComponent) {
            if (value != null) {
                processComponent();
            }
            return this;
        }
        public IDecomposition<ILexical> OnBase(Action<ILexical> processComponent) {
            if (value != null) {
                processComponent(value);
            }
            return this;
        }
        private bool TypeAlreadyMatched(Type type) {
            return typesMatched.Add(type);
        }
        ILexical value;
        private HashSet<Type> typesMatched;
    }
    public struct Decomposition<TResult> : IDecomposition<ILexical, TResult>
    {
        public Decomposition(ILexical composed)
            : this() {
            value = composed;
            typesMatched = new HashSet<Type>();
            results = new List<TResult>();
        }
        public Decomposition<TResult> OnTrait<TTrait>(Action processComponent) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    processComponent();
                }
            }
            return this;
        }
        public Decomposition<TResult> OnTrait<TTrait>(Action<TTrait> processComponent) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    processComponent(component);
                }
            }
            return this;
        }
        public Decomposition<TResult> OnTrait<TTrait>(Func<TResult> processComponent) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    results.Add(processComponent());
                }
            }
            return this;
        }
        public Decomposition<TResult> OnTrait<TTrait>(Func<TTrait, TResult> processComponent) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    results.Add(processComponent(component));
                }
            }
            return this;
        }
        public Decomposition<TResult> OnTrait<TTrait>(TResult traitResult) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    results.Add(traitResult);
                }
            }
            return this;
        }
        public IDecomposition<ILexical, TResult> OnBase(Action processComponent) {
            if (value != null) {
                processComponent();
            }
            return this;
        }
        public IDecomposition<ILexical, TResult> OnBase(Action<ILexical> processComponent) {
            if (value != null) {
                processComponent(value);
            }
            return this;
        }
        public IDecomposition<ILexical, TResult> OnBase(Func<TResult> processComponent) {
            if (value != null) {
                results.Add(processComponent());
            }
            return this;
        }
        public IDecomposition<ILexical, TResult> OnBase(Func<ILexical, TResult> processComponent) {
            if (value != null) {
                results.Add(processComponent(value));
            }
            return this;
        }
        public IDecomposition<ILexical, TResult> OnBase(TResult baseResult) {
            if (value != null) {
                results.Add(baseResult);
            }
            return this;
        }
        private bool TypeAlreadyMatched(Type type) {
            return typesMatched.Add(type);
        }
        ILexical value;
        private HashSet<Type> typesMatched;
        public IEnumerable<TResult> Results { get { return results; } }
        private List<TResult> results;
    }
    public static class Destructure
    {
        public static Decomposition MatchTraits(ILexical composed) {
            return new Decomposition(composed);
        }
        public static Decomposition<TResult> MatchTraits<TResult>(ILexical composed) {
            return new Decomposition<TResult>(composed);
        }
    }
}
