using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Algorithm.Patternization
{
    /// <summary>
    /// Provides static access to Type wise Destructuring functionality.
    /// </summary>
    static class Destructure
    {
        /// <summary>
        /// Constructs the head of a possibly result returning decomposition over the specified object.
        /// </summary> 
        /// <param name="composed">The object to decompose.</param>
        /// <returns>The head of a possibly result returning decomposition over the specified object..</returns>
         static FromTo MatchMany(this ILexical composed) {
            return new FromTo(composed);
        }
        /// <summary>
        /// Represents the head of a possibly result returning decomposition expression.
        /// </summary>
          struct FromTo
        {
            internal FromTo(ILexical value) {
                _value = value;
            }
            /// <summary>
            /// Completes the From...To expression by specifying the type of the Result and constructing and returning the head of a result-yielding, Type based Destructure expression.
            /// </summary>
            /// <typeparam name="R">The Type of the Results which may be returned by the expressions appended to the newly created expression.</typeparam>
            /// <returns>
            ///  The head of a result-yielding, Type based Destructure expression.
            ///  </returns>  
              TraitCase<R> Yield<R>() {
                return new TraitCase<R>(_value);
            }
            /// <summary>
            /// Completes the From...To expression by defaulting the Type of the Result to System.Object and returning the head of a result-yielding, Type based Destructure expression.
            /// </summary>
            /// <returns>
            ///  The head of a result-yielding, Type based Destructure expression.
            ///  </returns>  
              TraitCase<object> Yield() {
                return new TraitCase<object>(_value);
            }
            private ILexical _value;
        }
    }
    /// <summary>
    /// Provides the basis for the representation of Type-Wise Destructure expressions over an object of any Lexical Type.
    /// </summary>
    /// <typeparam name="TComplex">The type of the object whose decomposition an implementation represents. This can be any type which implements the ILexical interface.</typeparam>
    public interface ITraitCase<TComplex> where TComplex : class,  LASI.Algorithm.ILexical
    {

    }
    /// <summary>
    /// Provides the basis for the representation of Type-Wise decomposition expressions over an object of any Lexical Type and the yielding of a result for each Trait recognized.
    /// </summary>
    /// <typeparam name="TComplex">The type of the object whose decomposition an implementation represents. This can be any type which implements the ILexical interface.</typeparam>
    /// <typeparam name="TResult">The type of the results which may be evoked.</typeparam>
    public interface ITraitCase<TComplex, TResult> : ITraitCase<TComplex> where TComplex : class,  LASI.Algorithm.ILexical
    {
        /// <summary>
        /// Gets the collection of results which contains all results yielded for any recognized Trait recognition clause that specified a result.
        /// </summary>
        IEnumerable<TResult> Results();
    }
    /// <summary>
    /// Provides for the construction of flexible Type-Wise Destructure expressions which allow for specialized logic to be applied to each Typed aspect of an object if present. 
    /// </summary>
    public struct TraitCase : ITraitCase<ILexical>
    {
        internal TraitCase(ILexical value)
            : this() {
            _value = value;
            typesMatched = new HashSet<Type>();
        }
        /// <summary>
        /// Executes the provided Action if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="action">The Action to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public TraitCase With<TTrait>(Action action) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = _value as TTrait;
                if (_value != null && _value is TTrait) {
                    action();
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed if it has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="action">The Action to invoke on the object being decomposed if the trait is present.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public TraitCase With<TTrait>(Action<TTrait> action) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = _value as TTrait;
                if (component != null) {
                    action(component);
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action and ends the destructuring expression.
        /// </summary> 
        /// <param name="action">The Action to invoke.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical> Base(Action action) {
            if (_value != null) {
                action();
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed and ends the destructuring expression.
        /// </summary> 
        /// <param name="action">The Action to invoke on the object being decomposed.</param>
        /// <returns>The IDecomposition describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical> Base(Action<ILexical> action) {
            if (_value != null) {
                action(_value);
            }
            return this;
        }
        private bool TypeAlreadyMatched(Type type) {
            return typesMatched.Add(type);
        }
        ILexical _value;
        private HashSet<Type> typesMatched;
    }
    /// <summary>
    /// Provides for the construction of flexible Type-Wise Decomposition expressions which allow for specialized logic to be applied to each Typed aspect of an object if present.
    /// <typeparam name="TResult">The type of the elements which may result from decomposing the object.</typeparam>
    /// </summary>
    public struct TraitCase<TResult> : ITraitCase<ILexical, TResult>
    {
        internal TraitCase(ILexical composed)
            : this() {
            value = composed;
            typesMatched = new HashSet<Type>();
            results = new List<TResult>();
        }
        /// <summary>
        /// Executes the provided Action if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="action">The Action to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public TraitCase<TResult> With<TTrait>(Action action) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    action();
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed if it has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="action">The Action to invoke on the object being decomposed if the trait is present.</param>
        /// <returns>The TraitCase&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public TraitCase<TResult> With<TTrait>(Action<TTrait> action) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    action(component);
                }
            }
            return this;
        }
        /// <summary>
        /// Stores the value resulting from the invocation of the supplied function if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="func">The function to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The TraitCase&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public TraitCase<TResult> With<TTrait>(Func<TResult> func) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    results.Add(func());
                }
            }
            return this;
        }
        /// <summary>
        /// Invokes the provided function, storing its result, on the object being decomposed if it has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="func">The function to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The TraitCase&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public TraitCase<TResult> With<TTrait>(Func<TTrait, TResult> func) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    results.Add(func(component));
                }
            }
            return this;
        }
        /// <summary>
        /// Yields the supplied result value if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="traitResult">The result value to yield if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public TraitCase<TResult> With<TTrait>(TResult traitResult) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    results.Add(traitResult);
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action and ends the destructuring expression.
        /// </summary> 
        /// <param name="action">The Action to invoke.</param>
        /// <returns>The ITraitCase&lt;ILexical, TResult&gt; describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical, TResult> Always(Action action) {
            if (value != null) {
                action();
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed and ends the destructuring expression.
        /// </summary> 
        /// <param name="action">The Action to invoke on the object being decomposed.</param>
        /// <returns>The ITraitCase&lt;ILexical, TResult&gt; describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical, TResult> Always(Action<ILexical> action) {
            if (value != null) {
                action(value);
            }
            return this;
        }
        /// <summary>
        /// Ends the destructuring expression invoking the provided function and storing its result.
        /// </summary>
        /// <param name="func">The function which returns a value to store as the base type's result.</param>
        /// <returns>The ITraitCase&lt;ILexical, TResult&gt; describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical, TResult> Always(Func<TResult> func) {
            if (value != null) {
                results.Add(func());
            }
            return this;
        }
        /// <summary>
        /// Ends the destructuring expression invoking the provided function on the object being decomposed and stores its result.
        /// </summary>
        /// <param name="func">The function which returns a value to store as the base type's result.</param>
        /// <returns>The ITraitCase&lt;ILexical, TResult&gt; describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical, TResult> Always(Func<ILexical, TResult> func) {
            if (value != null) {
                results.Add(func(value));
            }
            return this;
        }
        /// <summary>
        /// Ends the destructuring expression and stores the provided result value.
        /// </summary>
        /// <param name="baseResult">The result value to include in the results of the expression.</param>
        /// <returns>The ITraitCase&lt;ILexical&gt;&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public ITraitCase<ILexical, TResult> Always(TResult baseResult) {
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
        /// <summary>
        /// Gets the collection of results which contains all results yielded for any recognized Trait recognition clause that specified a result.
        /// If no Traits were recognized and no result was supplied for the base Type, the result will be empty.
        /// </summary>
        public IEnumerable<TResult> Results() { return results; }
        private List<TResult> results;
    }

}
