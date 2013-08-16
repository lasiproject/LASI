using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Utilities.AlgebraicDecomposition
{
    internal interface IDecompose<T>
    {
        T Value { get; }
    }
    internal interface IDecompose<T, R> : IDecompose<T>
    {
        IEnumerable<R> Results { get; }
    }
    internal struct Decompose<TComposed> : IDecompose<TComposed> where TComposed : class
    {
        internal Decompose(TComposed toDecompose)
            : this() {
            Value = toDecompose;
            typesMatched = new List<Type>();
        }
        internal Decompose<TComposed> As<TComponent>(Action processComponent) where TComponent : class {
            if (!TypeAlreadyMatched(typeof(TComponent))) {
                var component = Value as TComponent;
                if (Value != null && Value is TComponent) {
                    processComponent();
                }
            }
            return this;
        }
        internal Decompose<TComposed> As<TComponent>(Action<TComponent> processComponent) where TComponent : class {
            if (!TypeAlreadyMatched(typeof(TComponent))) {
                var component = Value as TComponent;
                if (component != null) {
                    processComponent(component);
                }
            }
            return this;
        }
        internal IDecompose<TComposed> Base(Action processComponent) {
            if (!TypeAlreadyMatched(typeof(TComposed))) {
                if (Value != null) {
                    processComponent();
                }
            }
            return this;
        }
        internal IDecompose<TComposed> Base(Action<TComposed> processComponent) {
            if (!TypeAlreadyMatched(typeof(TComposed))) {
                if (Value != null) {
                    processComponent(Value);
                }
            }
            return this;
        }
        private bool TypeAlreadyMatched(Type type) {
            var result = typesMatched.Any(t => t.GetTypeInfo() == type.GetTypeInfo() || type.GetTypeInfo().IsSubclassOf(t));
            if (!result) {
                typesMatched.Add(type);
            }
            return result;
        }
        public TComposed Value { get; private set; }
        private List<Type> typesMatched;
    }
    internal struct Decompose<TComposed, TResult> : IDecompose<TComposed, TResult> where TComposed : class
    {
        internal Decompose(TComposed composed)
            : this() {
            Value = composed;
            typesMatched = new List<Type>();
            results = new List<TResult>();
        }
        public Decompose<TComposed, TResult> As<TComponent>(Action processComponent) where TComponent : class {
            if (!TypeAlreadyMatched(typeof(TComponent))) {
                var component = Value as TComponent;
                if (Value != null && Value is TComponent) {
                    processComponent();
                }
            }
            return this;
        }
        public Decompose<TComposed, TResult> As<TComponent>(Action<TComponent> processComponent) where TComponent : class {
            if (!TypeAlreadyMatched(typeof(TComponent))) {
                var component = Value as TComponent;
                if (component != null) {
                    processComponent(component);
                }
            }
            return this;
        }
        public Decompose<TComposed, TResult> As<TComponent>(Func<TResult> processComponent) where TComponent : class {
            if (!TypeAlreadyMatched(typeof(TComponent))) {
                var component = Value as TComponent;
                if (Value != null && Value is TComponent) {
                    results.Add(processComponent());
                }
            }
            return this;
        }
        public Decompose<TComposed, TResult> As<TComponent>(Func<TComponent, TResult> processComponent) where TComponent : class {
            if (!TypeAlreadyMatched(typeof(TComponent))) {
                var component = Value as TComponent;
                if (component != null) {
                    results.Add(processComponent(component));
                }
            }
            return this;
        }
        public IDecompose<TComposed, TResult> Base(Action processComponent) {
            if (!TypeAlreadyMatched(typeof(TComposed))) {
                if (Value != null) {
                    processComponent();
                }
            }
            return this;
        }
        public IDecompose<TComposed, TResult> Base(Action<TComposed> processComponent) {
            if (!TypeAlreadyMatched(typeof(TComposed))) {
                if (Value != null) {
                    processComponent(Value);
                }
            }
            return this;
        }
        public IDecompose<TComposed, TResult> Base(Func<TResult> processComponent) {
            if (!TypeAlreadyMatched(typeof(TComposed))) {
                if (Value != null) {
                    results.Add(processComponent());
                }
            }
            return this;
        }
        public IDecompose<TComposed, TResult> Base(Func<TComposed, TResult> processComponent) {
            if (!TypeAlreadyMatched(typeof(TComposed))) {
                if (Value != null) {
                    results.Add(processComponent(Value));
                }
            }
            return this;
        }
        private bool TypeAlreadyMatched(Type type) {
            var result = typesMatched.Any(t => t.GetTypeInfo() == type.GetTypeInfo() || type.GetTypeInfo().IsSubclassOf(t));
            if (!result) {
                typesMatched.Add(type);
            }
            return result;
        }
        public TComposed Value { get; private set; }
        private List<Type> typesMatched;
        public IEnumerable<TResult> Results { get { return results; } }
        private List<TResult> results;
    }
    internal static class AlgebraicDestructuring
    {
        internal static Decompose<TComposed> Destructure<TComposed>(this TComposed composed) where TComposed : class {
            return new Decompose<TComposed>(composed);
        }
        internal static Decompose<TComposed, TResult> Destructure<TComposed, TResult>(this TComposed composed) where TComposed : class {
            return new Decompose<TComposed, TResult>(composed);
        }
    }
}
