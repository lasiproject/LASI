using LASI;
using LASI.Utilities;
using LASI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using LASI.Content.Serialization.Json;

namespace LASI.Content.Serialization
{
    /// <summary>
    /// Provides basic JSON serialization of for various configuration of ILexical elements.
    /// </summary>
    public class SimpleJsonSerializer : ILexicalSerializer<ILexical, JArray>
    {
        /// <summary>
        /// Serializes the provided sequence of ILexical instances into JSON elements and returns a single <see cref="JContainer"/> containing them.
        /// </summary>
        /// <param name="source">The sequence of ILexical instances to serialize into a single XElement .</param>
        /// <param name="parentElementTitle">The desired name for the resulting XElement .</param>
        /// <param name="degreeOfOutput">The DegreeOfOutput value specifying the per element amount of detail the serialization will retain.</param>
        /// <returns>A single XElement  containing the serialized representation of the given sequence of elements.</returns>
        public JArray Serialize(IEnumerable<ILexical> source, string parentElementTitle) => new JArray(source.Select(SerializationExtensions.ToJObject));

    }
}
