using LASI;
using LASI.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LASI.ContentSystem.Serialization.XML
{
    /// <summary>
    /// Defines the behavioral requirements for an XML output stream which provides synchronous and asynchronous output functionality for serializing covarient sequences of ILexical elements.
    /// </summary>
    /// <typeparam name="S">The Type of the collections generic containing elements to be serialized. This Type parameter is contravariant.</typeparam>
    /// <typeparam name="T">The Type of the elements in the generic collections. This Type parameter is covariant.</typeparam>
    /// <typeparam name="W">The type of the underlying xmlWriter object. This Type parameter is contravariant.</typeparam>
    public interface ILexicalWriter<in S, out T, in W> : IDisposable
        where S : IEnumerable<T>
        where T : ILexical
        where W : System.Xml.XmlWriter
    {
        /// <summary>
        /// Writes a sequence of elements to the underlying xmlWriter, using the provided title string and Degree of output.
        /// </summary>
        /// <param name="elements">The sequence S&lt;T&gt; containing the values to serialize.</param>
        /// <param name="resultSetTitle">The title string which will represent a parent node of which the serialized elements will be child nodes.</param>
        /// <param name="degreeOfOutput">Controls the level of output detail to which elements will be serialized.</param>
        void Write(S elements, string resultSetTitle, DegreeOfOutput degreeOfOutput);
        /// <summary>
        /// Writes a sequence of elements to the underlying xmlWriter, using the provided title string and Degree of output.
        /// </summary>
        /// <param name="elements">The sequence S&lt;T&gt; containing the values to serialize.</param>
        /// <param name="resultSetTitle">The title string which will represent a parent node of which the serialized elements will be child nodes.</param>
        /// <param name="degreeOfOutput">Controls the level of output detail to which elements will be serialized.</param>
        /// <rereturns>A Task representing the ongoing asynchronous operation.</rereturns>
        Task WriteAsync(S elements, string resultSetTitle, DegreeOfOutput degreeOfOutput);
        /// <summary>
        /// Gets the underlying System.Xml.XmlWriter to which all output is written.
        /// </summary>
        System.Xml.XmlWriter Target {
            get;
        }
    }
    /// <summary>
    /// Controls the quantity and level detail which will be serialized into the XML representations of each lexical element.
    /// </summary>
    public enum DegreeOfOutput
    {
        /// <summary>
        /// Only the top weighted elements in the input set will be serialized.
        /// </summary>
        TopWeighted,
        /// <summary>
        /// All elements will in the input set will be serialized.
        /// </summary>
        Comprehensive, 
    }
}
