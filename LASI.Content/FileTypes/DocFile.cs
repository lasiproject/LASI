using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Content
{
    /// <summary>
    /// A strongly typed wrapper that encapsulates a legacy Word document (.doc).
    /// </summary>
    public sealed class DocFile : InputFile
    {
        /// <summary>
        /// Initializes a new instance of the DocFile class for the given path.
        /// </summary>
        /// <param name="path">The path to a .doc file.</param>
        /// <exception cref="FileTypeWrapperMismatchException">Thrown if the provided path does not end in the .doc extension.</exception>
        public DocFile(string path) : base(path)
        {
            if (!Extension.EqualsIgnoreCase(".doc"))
            {
                throw new FileTypeWrapperMismatchException(GetType().ToString(), Extension);
            }
        }
        /// <summary>
        /// Returns a single string containing all of the text in the DocFile.
        /// </summary>
        /// <returns>A string containing all of the text in the DocFile.</returns>
        public override string LoadText()
        {
            DocXFile docx;
            var todocXConverter = new DocToDocXConverter(this);
            try
            {
                docx = todocXConverter.ConvertFile() as DocXFile;
            }
            catch (Exception e) { throw new FileConversionFailureException(FullPath, "DOC", "DOCX", e); }
            return docx.LoadText();
        }
        /// <summary>
        /// Returns a Task&lt;string&gt; which when awaited yields all of the text in the DocFile.
        /// </summary>
        /// <returns>A Task&lt;string&gt; which when awaited yields all of the text in the DocFile.</returns>
        public override async Task<string> LoadTextAsync()
        {
            DocXFile docx;
            var toDocXConverter = new DocToDocXConverter(this);
            try
            {
                docx = await toDocXConverter.ConvertFileAsync() as DocXFile;
            }
            catch (Exception e) { throw new FileConversionFailureException(FullPath, "DOC", "DOCX", e); }
            return await docx.LoadTextAsync();
        }
    }

}
