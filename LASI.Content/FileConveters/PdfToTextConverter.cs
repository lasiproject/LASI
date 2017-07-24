using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Linq.Enumerable;
using LASI.Utilities;
using System.Linq;
using System.Collections.Generic;
using LASI.Core.Configuration;

namespace LASI.Content
{
    /// <summary>
    /// An input file converter specialized to extract the non optical textual content from a .pdf (Adobe Acrobat) document
    /// and create a text file containing this content as raw text.
    /// </summary>
    public sealed class PdfToTextConverter : FileConverter<PdfFile, TxtFile>
    {
        /// <summary>
        /// Constructs a new instance which will govern the conversion of the PdfFile instance provided.
        /// The converted file will be placed in the same directory as the original.
        /// </summary>
        /// <param name="infile">The PdfFile instance representing the .pdf document to convert.</param>
        public PdfToTextConverter(PdfFile infile)
            : base(infile) { }

        /// <summary>
        /// Converts the document governed by this instance from .pdf format to .txt ASCII text format.
        /// </summary>
        /// <returns>An InputFile object wrapping the newly created .txt file resulting from the operation.</returns>
        public override TxtFile ConvertFile()
        {
            var newPath = Original.PathSansExt + ".txt";

            var pages = new PdfParser().ExtractPagesWithTextContent(Original.FullPath);

            using (var outFile = new StreamWriter(path: newPath, append: false, encoding: Encoding.UTF8))
            {
                foreach (var page in pages)
                {
                    outFile.Write(page);
                }
                return new TxtFile(newPath);
            }

        }
        /// <summary>
        /// Asynchronously converts the document governed by this instance from .pdf format to .txt ASCII text format,
        /// returning a System.Threading.Tasks.Task&lt;<see cref="TxtFile"></see>&gt; representing the ongoing asynchronous operation and which,
        /// when awaited, evaluates to a <see cref="TxtFile"/> providing access to the result.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{TxtFile}"/> System.Threading.Tasks.Task&lt;<see cref="TxtFile"></see>&gt; representing the ongoing asynchronous operation and which,
        /// when awaited, evaluates to a <see cref="TxtFile"/> providing access to the result.
        /// </returns>
        public override async Task<TxtFile> ConvertFileAsync() => await Task.Run(() => ConvertFile()).ConfigureAwait(false);

        // Found this on CodeProject.com, no strings attached, it works pretty well but it is not very well written.

        /// <summary>
        /// Parses a PDF file and extracts the text from it.
        /// </summary>
        private class PdfParser
        {
            #region ExtractText
            /// <summary>
            /// Extracts a text from a PDF file.
            /// </summary>
            /// <param name="inputPath">the full path to the pdf file.</param>
            /// <returns>the extracted text</returns>
            public IEnumerable<string> ExtractPagesWithTextContent(string inputPath)
            {
                // Create a reader for the given PDF file
                using (var stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, options: FileOptions.Asynchronous, bufferSize: 1024))
                {
                    // Since I forgot for the umpteenth time, Note: `PdfReader does not implement `IDisposable`
                    var reader = new PdfReader(stream);

                    var pages = from pageNumber in Range(1, reader.NumberOfPages).AsParallel().WithDegreeOfParallelism(Concurrency.Max)
                                let bytes = reader.GetPageContent(pageNumber)
                                let pageText = ExtractTextFromPDFBytes(bytes)
                                select pageText + " ";

                    foreach (var page in pages)
                    {
                        yield return page;
                    }
                }
            }
            #endregion

            /// <summary>
            /// This method processes an uncompressed Adobe (text) object 
            /// and extracts text.
            /// </summary>
            /// <param name="input">uncompressed</param>
            /// <returns>The text extracted from the stream of PDF encoded bytes.</returns>
            /// <remarks>
            /// 
            /// BT = Beginning of a text object operator 
            /// ET = End of a text object operator
            /// Td move to the start of next line
            ///  5 Ts = superscript
            /// -5 Ts = subscript
            /// </remarks>
            private string ExtractTextFromPDFBytes(byte[] input)
            {
                var empty = string.Empty;
                if (input == null || input.Length == 0)
                {
                    return empty;
                }
                var resultString = empty;

                // Flag showing if we are we currently inside a text object
                var inTextObject = false;

                // Flag showing if the next character is key 
                // e.g. '\\' to get a '\' character or '\(' to get '('
                var nextLiteral = false;

                // () Bracket nesting level. Text appears inside ()
                var bracketDepth = 0;

                // Keep previous chars to get extract numbers etc.: 
                var previousCharacters = Repeat(' ', CharsToKeep).ToArray();

                foreach (char c in input)
                {
                    if (inTextObject)
                    {
                        var (nextIsLiteral, content) = format(c);
                        nextLiteral = nextIsLiteral;
                        resultString += content;
                    }

                    // Store the recent characters for 
                    // when we have to go back for checking
                    for (var j = 0; j < CharsToKeep - 1; j++)
                    {
                        previousCharacters[j] = previousCharacters[j + 1];
                    }
                    previousCharacters[CharsToKeep - 1] = c;

                    // Start of a text object
                    inTextObject ^= CheckToken("BT", previousCharacters, 15);
                }
                return resultString;

                (bool nextIsLiteral, string content) format(char c)
                {
                    switch (bracketDepth)
                    {
                        case 0 when CheckToken("ET", previousCharacters, 15):
                            return (nextLiteral, " ");
                        case 0 when c is '(' && !nextLiteral: // Start outputting text
                            bracketDepth = 1;
                            return (nextLiteral, empty);
                        case 0:
                            // Position the text
                            var str = CheckToken(new[] { "TD", "Td" }, previousCharacters, 15) ? "\n\r"
                                : CheckToken(new[] { "'", "T*", "\"" }, previousCharacters, 15) ? "\n"
                                : CheckToken("Tj", previousCharacters, 15) ? " " : empty;
                            return (nextLiteral, str);

                        case 1 when !nextLiteral && c is ')': // Stop outputting text
                            bracketDepth = 0;
                            return (nextLiteral, empty);
                        case 1 when !nextLiteral && c is '\\':
                            // Just a normal text character:
                            return (true, default);
                        case 1 when c >= ' ' && c <= '~' || c >= 128 && c < 255:
                            // Only print out next character no matter what. 
                            // Do not interpret.
                            return (false, c.ToString());
                        default:
                            return (nextLiteral, c.ToString());
                    }
                }
            }



            /// <summary>
            /// Check if a certain 2 character currentCharacterToken just came along (e.g. BT)
            /// </summary>
            /// <param name="token">the searched currentCharacterToken</param>
            /// <param name="recent">the recent character array</param>
            /// <param name="charsToKeep"></param>
            /// <returns></returns>
            private bool CheckToken(string token, char[] recent, int charsToKeep) => CheckToken(new[] { token }, recent, charsToKeep);

            /// <summary>
            /// Check if a certain 2 character currentCharacterToken just came along (e.g. BT)
            /// </summary>
            /// <param name="tokens">the searched currentCharacterToken</param>
            /// <param name="recent">the recent character array</param>
            /// <param name="charsToKeep">The number of being retained.</param>
            /// <returns></returns>
            private bool CheckToken(string[] tokens, char[] recent, int charsToKeep)
            {
                return tokens.Any(tokenMatches);

                bool tokenMatches(string currentCharacterToken)
                {
                    try
                    {
                        return test(currentCharacterToken);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Logger.Log(e);
                        return false;
                    }
                }

                bool test(string currentCharacterToken) =>
                    recent[charsToKeep - 3] == currentCharacterToken[0]
                    && recent[charsToKeep - 2] == currentCharacterToken[1]
                    &&
                    (
                        recent[charsToKeep - 1] == ' '
                        || recent[charsToKeep - 1] == 0x0d
                        || recent[charsToKeep - 1] == 0x0a
                    )
                    &&
                    (
                        recent[charsToKeep - 4] == ' ' ||
                        recent[charsToKeep - 4] == 0x0d ||
                        recent[charsToKeep - 4] == 0x0a
                    );
            }

            #region Fields
            /// <summary>
            /// The number of characters to keep, when extracting text.
            /// </summary>
            private const int CharsToKeep = 15;
            #endregion
        }
    }
}
