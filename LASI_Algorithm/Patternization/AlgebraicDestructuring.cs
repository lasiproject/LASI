using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LASI.Algorithm.TraitWiseDecomposition
{
    /// <summary>
    /// Provides the basis for the representation of Type-Wise decomposition expressions over an object of any Lexical Type.
    /// </summary>
    /// <typeparam name="TComplex">The type of the object whose decomposition an implementation represents. This can be any type which implements the ILexical interface.</typeparam>
    public interface IDecomposition<TComplex> where TComplex : class,  LASI.Algorithm.ILexical
    {

    }
    /// <summary>
    /// Provides the basis for the representation of Type-Wise decomposition expressions over an object of any Lexical Type and the yielding of a result for each Trait recognized.
    /// </summary>
    /// <typeparam name="TComplex">The type of the object whose decomposition an implementation represents. This can be any type which implements the ILexical interface.</typeparam>
    /// <typeparam name="TResult">The type of the results which may be evoked.</typeparam>
    public interface IDecomposition<TComplex, TResult> : IDecomposition<TComplex> where TComplex : class,  LASI.Algorithm.ILexical
    {
        /// <summary>
        /// Gets the collection of results which contains all results yielded for any recognized Trait recognition clause that specified a result.
        /// </summary>
        IEnumerable<TResult> Results { get; }
    }
    /// <summary>
    /// Provides for the construction of flexible Type-Wise Decomposition expressions which allow for specialized logic to be applied to each Typed aspect of an object if present. 
    /// </summary>
    public struct Decomposition : IDecomposition<ILexical>
    {
        internal Decomposition(ILexical toDecompose)
            : this() {
            value = toDecompose;
            typesMatched = new HashSet<Type>();
        }
        /// <summary>
        /// Executes the provided Action if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="processComponent">The Action to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public Decomposition OnTrait<TTrait>(Action processComponent) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    processComponent();
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed if it has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="processComponent">The Action to invoke on the object being decomposed if the trait is present.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public Decomposition OnTrait<TTrait>(Action<TTrait> processComponent) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    processComponent(component);
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action and ends the destructuring expression.
        /// </summary> 
        /// <param name="processComponent">The Action to invoke.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public IDecomposition<ILexical> OnBase(Action processComponent) {
            if (value != null) {
                processComponent();
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed and ends the destructuring expression.
        /// </summary> 
        /// <param name="processComponent">The Action to invoke on the object being decomposed.</param>
        /// <returns>The IDecomposition describing the destructuring expression so far.</returns> 
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
    /// <summary>
    /// Provides for the construction of flexible Type-Wise Decomposition expressions which allow for specialized logic to be applied to each Typed aspect of an object if present.
    /// <typeparam name="TResult">The type of the elements which may result from decomposing the object.</typeparam>
    /// </summary>
    public struct Decomposition<TResult> : IDecomposition<ILexical, TResult>
    {
        internal Decomposition(ILexical composed)
            : this() {
            value = composed;
            typesMatched = new HashSet<Type>();
            results = new List<TResult>();
        }
        /// <summary>
        /// Executes the provided Action if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="processComponent">The Action to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public Decomposition<TResult> OnTrait<TTrait>(Action processComponent) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    processComponent();
                }
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed if it has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="processComponent">The Action to invoke on the object being decomposed if the trait is present.</param>
        /// <returns>The Decomposition describing the destructuring expression so far.</returns> 
        public Decomposition<TResult> OnTrait<TTrait>(Action<TTrait> processComponent) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    processComponent(component);
                }
            }
            return this;
        }
        /// <summary>
        /// Stores the value resulting from the invocation of the supplied function if the object being decomposed has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="processComponent">The function to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public Decomposition<TResult> OnTrait<TTrait>(Func<TResult> processComponent) where TTrait : class ,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (value != null && value is TTrait) {
                    results.Add(processComponent());
                }
            }
            return this;
        }
        /// <summary>
        /// Invokes the provided function, storing its result, on the object being decomposed if it has an inheritence or implementation relationship with the provided Type.
        /// </summary>
        /// <typeparam name="TTrait">The type representing a trait to match.</typeparam>
        /// <param name="processComponent">The function to invoke if the trait is present on the object being decomposed.</param>
        /// <returns>The Decomposition&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public Decomposition<TResult> OnTrait<TTrait>(Func<TTrait, TResult> processComponent) where TTrait : class,  LASI.Algorithm.ILexical {
            if (!TypeAlreadyMatched(typeof(TTrait))) {
                var component = value as TTrait;
                if (component != null) {
                    results.Add(processComponent(component));
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
        public Decomposition<TResult> OnTrait<TTrait>(TResult traitResult) where TTrait : class,  LASI.Algorithm.ILexical {
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
        /// <param name="processComponent">The Action to invoke.</param>
        /// <returns>The Decomposition&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public IDecomposition<ILexical, TResult> OnBase(Action processComponent) {
            if (value != null) {
                processComponent();
            }
            return this;
        }
        /// <summary>
        /// Executes the provided Action on the object being decomposed and ends the destructuring expression.
        /// </summary> 
        /// <param name="processComponent">The Action to invoke on the object being decomposed.</param>
        /// <returns>The Decomposition&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public IDecomposition<ILexical, TResult> OnBase(Action<ILexical> processComponent) {
            if (value != null) {
                processComponent(value);
            }
            return this;
        }
        /// <summary>
        /// Ends the destructuring expression invoking the provided function and storing its result.
        /// </summary>
        /// <param name="processComponent">The function which returns a value to store as the base type's result.</param>
        /// <returns>The IDecomposition&lt;ILexical&gt;&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public IDecomposition<ILexical, TResult> OnBase(Func<TResult> processComponent) {
            if (value != null) {
                results.Add(processComponent());
            }
            return this;
        }
        /// <summary>
        /// Ends the destructuring expression invoking the provided function on the object being decomposed and stores its result.
        /// </summary>
        /// <param name="processComponent">The function which returns a value to store as the base type's result.</param>
        /// <returns>The IDecomposition&lt;ILexical&gt;&lt;TResult&gt; describing the destructuring expression so far.</returns> 
        public IDecomposition<ILexical, TResult> OnBase(Func<ILexical, TResult> processComponent) {
            if (value != null) {
                results.Add(processComponent(value));
            }
            return this;
        }
        /// <summary>
        /// Ends the destructuring expression and stores the provided result value.
        /// </summary>
        /// <param name="baseResult">The result value to include in the results of the expression.</param>
        /// <returns>The IDecomposition&lt;ILexical&gt;&lt;TResult&gt; describing the destructuring expression so far.</returns> 
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
        /// <summary>
        /// Gets the collection of results which contains all results yielded for any recognized Trait recognition clause that specified a result.
        /// If no Traits were recognized and no result was supplied for the base Type, the result will be empty.
        /// </summary>
        public IEnumerable<TResult> Results { get { return results; } }
        private List<TResult> results;
    }
    /// <summary>
    /// Provides static access to Type wise decomposition functionality.
    /// </summary>
    public static class Destructure
    {
        /// <summary>
        /// Constructs the head of a non result returning decomposition over the specified object.
        /// </summary> 
        /// <param name="composed">The object to decompose.</param>
        /// <returns>The head of a non result returning decomposition over the specified object.</returns>
        public static Decomposition MatchTraits(ILexical composed) {
            return new Decomposition(composed);
        }
        /// <summary>
        /// Constructs the head of a possibly result returning decomposition over the specified object.
        /// </summary> 
        /// <typeparam name="TResult">The type of the elements which may result.</typeparam>
        /// <param name="composed">The object to decompose.</param>
        /// <returns>The head of a possibly result returning decomposition over the specified object..</returns>
        public static Decomposition<TResult> MatchTraits<TResult>(ILexical composed) {
            return new Decomposition<TResult>(composed);
        }
    }
}
