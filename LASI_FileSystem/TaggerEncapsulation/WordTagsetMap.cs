using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;

namespace LASI.FileSystem
{
    /// <summary>
    /// Represents a tagset-to-runtime-type-mapping context which translates between a Part Of Speech
    /// Tagger'd provided tags and their runtime type equivalents. 
    /// This class represents the tagset => runtime-type mapping for w occurances
    /// <see cref="WordTagsetMap"/>
    ///<seealso cref="WordMapper"/>
    /// <example>
    ///<code> 
    /// var constructorFunction = myContext["TAG"];
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
        /// <exception cref="UnknownPOSException">Implementors should Throw this exception if and only if when the index string is not a tag defined by the tagset being provided.</exception>
        public abstract Func<string, Word> this[string tag] {
            get;
        }


        public abstract string this[Func<string, Word> mappedConstructor] {
            get;
        }

        /// <summary>
        /// When overriden in a derrived class, Gets a Read Only Dictionary which represents the mapping between Part Of Speech tags and the cunstructors which instantiate their run-time representations.
        /// </summary>
        public abstract IReadOnlyDictionary<string, Func<string, Word>> TypeDictionary {
            get;
        }
        #endregion

    }
}
