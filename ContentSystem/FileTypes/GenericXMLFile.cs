using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Resolvers;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Xml.XmlConfiguration;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace LASI.ContentSystem
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates an XML document (.xml).
    /// </summary>
    sealed class GenericXMLFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the GenericXMLFile class for the given path.
        /// </summary>
        /// <param name="filePath">The path to a .xml file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .xml extension.</exception>
        public GenericXMLFile(string filePath)
            : base(filePath) {
            if (!this.Ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
                throw new FileTypeWrapperMismatchException(GetType().ToString(), this.Ext);

        }

        /// <summary>
        /// Returns a single string containing all of the text in the PdfFile.
        /// </summary>
        /// <returns>A string containing all of the text in the PdfFile.</returns>
        public override string GetText() {
            using (var reader = XmlReader.Create(FullPath)) {
                return reader.ReadContentAsString();
            }
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the PdfFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the PdfFile.</returns>
        public override async Task<string> GetTextAsync() {
            using (var reader = XmlReader.Create(FullPath)) {
                return await reader.ReadContentAsStringAsync();
            }
        }
    }
}
