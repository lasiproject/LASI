using LASI;
using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem.TaggerEncapsulation
{
    using WordCreator = Func<string, Word>;
    /// <summary>
    /// Represents a tagset-to-runtime-type-mapping context which translates between a Part Of Speech
    /// Tagger's tagset and the classes whose instances provide their runtime representations of the tag.
    /// This class represents the tagset => runtime-type mapping for word occurances.
    /// <see cref="WordTagsetMap"/>
    /// <seealso cref="PhraseTagsetMap"/>  
    /// <seealso cref="WordMapper"/>
    /// <example>
    /// Example:
    ///<code> 
    /// var wordMap = new WordTagSetMap();
    /// var constructorFunction = map["TAG"];
    /// var runtimeWord = constructorFunction(itemText);
    /// </code>
    /// </example>
    /// </summary>
    public abstract class WordTagsetMap
    {
        #region Properties and Indexers
        /// <summary>
        /// When overriden in a derrived class, Provides POS-Tag indexed access to a constructor function which can be invoked to create an instance of the class which provides its run-time representation.
        /// </summary>
        /// <param name="tag">The textual representation of a Part Of Speech tag.</param>
        /// <returns>A function which creates an isntance of the run-time type associated with the textual tag.</returns>
        /// <exception cref="UnknownWordTagException">Implementors should Throw this exception if and only if when the index string is not a tag defined by the tagset being provided.</exception>
        public abstract WordCreator this[string tag] { get; }

        /// <summary>
        /// When overriden in a derrived class, Gets the PosTag string corresponding to the runtime System.Type of the Return Type of given function of type { System.string => LASI.Algorithm.Word }.
        /// </summary>
        /// <param name="mappedConstructor">The function of type { System.string => LASI.Algorithm.Word } for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the runtime System.Type of the Return Type of given function of type { System.string => LASI.Algorithm.Word }.</returns>
        public abstract string this[WordCreator mappedConstructor] { get; }
        /// <summary>
        /// Gets the PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.
        /// </summary>
        /// <param name="word">The LASI.Algorithm.Word for which to get the corresponding tag.</param>
        /// <returns>The PosTag string corresponding to the System.Type of the given LASI.Algorithm.Word.</returns>
        public abstract string this[Word word] { get; }
        /// <summary>
        /// When overriden in a derrived class, Gets the Read Only Dictionary which represents the mapping between Part Of Speech tags and the cunstructors which instantiate their run-time representations.
        /// </summary>
        protected abstract IReadOnlyDictionary<string, WordCreator> TypeDictionary { get; }
        #endregion

    }
}
