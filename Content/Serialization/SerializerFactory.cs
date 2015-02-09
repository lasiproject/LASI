using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core;
using LASI.Utilities;

namespace LASI.Content.Serialization
{
    /// <summary>
    /// Provides static methods for the creation of <see cref="ILexicalSerializer{T, TResult}"/> objects.
    /// </summary>
    public static class SerializerFactory
    {
        /// <summary>
        /// Creates a new <see cref="ILexicalSerializer{T, TResult}"/> which map ILexical instances to the specified format. 
        /// </summary>
        /// <param name="targetFormat">The format to serialize to.</param>
        /// <returns>A new <see cref="ILexicalSerializer{T, TResult}"/> which map ILexical instances to the specified format. </returns>
        public static ILexicalSerializer<ILexical, object> Create(string targetFormat)
        {
            Format format;
            Enum.TryParse(
                value: targetFormat,
                ignoreCase: true,
                result: out format
            );
            switch (format)
            {
                case Format.XML: return new SimpleXmlSerializer();
                case Format.JSON: return new SimpleJsonSerializer();
                default: throw new ArgumentException($"The target format must be one of{Enum.GetNames(typeof(Format)).Format()}", targetFormat);
            }
        }

        enum Format
        {
            JSON,
            XML
        }
    }
}
