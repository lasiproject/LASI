﻿using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;

namespace LASI.Content.FileConverters
{
    using LASI.Content.FileTypes;
    using static System.Linq.Enumerable;

    /// <summary>
    /// An input file converter specialized to extract the non optical textual content from a .pdf (Adobe Acrobat) document and create a text file containing this content as raw text.
    /// </summary>
    public class PdfToTextConverter : FileConverter<PdfFile, TxtFile>
    {
        /// <summary>
        /// Constructs a new instance which will govern the conversion of the PdfFile instance provided. The converted file will be placed in the same directory as the original.
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
            new PdfParser().ExtractText(Original.FullPath, newPath);
            return new TxtFile(newPath);
        }

        /// <summary>
        /// Asynchronously converts the document governed by this instance from .pdf format to .txt ASCII text format.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task&lt;InputFile&gt; which, when awaited yields an InputFile object which wraps the newly created .txt file.</returns>
        public override async Task<TxtFile> ConvertFileAsync() => await Task.Run(() => ConvertFile()).ConfigureAwait(false);

        // Found this on CodeProject.com, no strings attached, it works pretty well but it is not very well written.

        /// <summary>
        /// Parses a PDF file and extracts the text from it.
        /// </summary>
        private class PdfParser
        {
            // BT = Beginning of a text object operator ET = End of a text object operator Td move to the start of next line 5 Ts = superscript
            // -5 Ts = subscript

            #region Fields

            /// <summary>
            /// The number of characters to keep, when extracting text.
            /// </summary>
            private const int CHARS_TO_KEEP = 15;

            #endregion Fields

            #region ExtractText

            /// <summary>
            /// Extracts a text from a PDF file.
            /// </summary>
            /// <param name="inFileName"> the full path to the pdf file.</param>
            /// <param name="outFileName">the output file name.</param>
            /// <returns>the extracted text</returns>
            public void ExtractText(string inFileName, string outFileName)
            {
                // Create a reader for the given PDF file
                using (var stream = new FileStream(inFileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, FileOptions.Asynchronous))
                using (var outFile = new StreamWriter(outFileName, append: false, encoding: Encoding.UTF8))
                {
                    var reader = new PdfReader(stream);
                    for (var pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
                    {
                        outFile.Write(ExtractTextFromPDFBytes(reader.GetPageContent(pageNumber)) + " ");
                    }
                }
            }

            #endregion ExtractText

            /// <summary>
            /// This method processes an uncompressed Adobe (text) object and extracts text.
            /// </summary>
            /// <param name="input">uncompressed</param>
            /// <returns>The text extracted from the stream of PDF encoded bytes.</returns>
            private string ExtractTextFromPDFBytes(byte[] input)
            {
                if (input == null || input.Length == 0)
                {
                    return string.Empty;
                }

                var resultString = string.Empty;

                // Flag showing if we are we currently inside a text object
                var inTextObject = false;

                // Flag showing if the next character is key e.g. '\\' to get a '\' character or '\(' to get '('
                var nextLiteral = false;

                // () Bracket nesting level. Text appears inside ()
                var bracketDepth = 0;

                // Keep previous chars to get extract numbers etc.:
                var previousCharacters = Repeat(' ', CHARS_TO_KEEP).ToArray();

                for (var i = 0; i < input.Length; i++)
                {
                    var c = (char)input[i];

                    if (inTextObject)
                    {
                        // Position the text
                        if (bracketDepth == 0)
                        {
                            if (CheckToken(new[] { "TD", "Td" }, previousCharacters, 15))
                            {
                                resultString += "\n\r";
                            }
                            else
                            {
                                if (CheckToken(new[] { "'", "T*", "\"" }, previousCharacters, 15))
                                {
                                    resultString += "\n";
                                }
                                else
                                {
                                    if (CheckToken(new[] { "Tj" }, previousCharacters, 15))
                                    {
                                        resultString += " ";
                                    }
                                }
                            }
                        }

                        // End of a text object, also go to a new line.
                        if (bracketDepth == 0 &&
                            CheckToken(new string[] { "ET" }, previousCharacters, 15))
                        {
                            inTextObject = false;
                            resultString += " ";
                        }
                        else
                        {
                            // Start outputting text
                            if ((c == '(') && (bracketDepth == 0) && (!nextLiteral))
                            {
                                bracketDepth = 1;
                            }
                            else
                            {
                                // Stop outputting text
                                if ((c == ')') && (bracketDepth == 1) && (!nextLiteral))
                                {
                                    bracketDepth = 0;
                                }
                                else
                                {
                                    // Just a normal text character:
                                    if (bracketDepth == 1)
                                    {
                                        // Only print out next character no matter what. Do not interpret.
                                        if (c == '\\' && !nextLiteral)
                                        {
                                            nextLiteral = true;
                                        }
                                        else
                                        {
                                            if (((c >= ' ') && (c <= '~')) ||
                                                ((c >= 128) && (c < 255)))
                                            {
                                                resultString += c.ToString();
                                            }
                                            nextLiteral = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Store the recent characters for when we have to go back for a checking
                    for (var j = 0; j < CHARS_TO_KEEP - 1; j++)
                    {
                        previousCharacters[j] = previousCharacters[j + 1];
                    }
                    previousCharacters[CHARS_TO_KEEP - 1] = c;

                    // Start of a text object
                    if (!inTextObject && CheckToken(new string[] { "BT" }, previousCharacters, 15))
                    {
                        inTextObject = true;
                    }
                }
                return resultString;
            }

            /// <summary>
            /// Check if a certain 2 character currentCharacterToken just came along (e.g. BT)
            /// </summary>
            /// <param name="tokens">     the searched currentCharacterToken</param>
            /// <param name="recent">     the recent character array</param>
            /// <param name="charsToKeep"></param>
            /// <returns></returns>
            private bool CheckToken(string[] tokens, char[] recent, int charsToKeep)
            {
                foreach (var currentCharacterToken in tokens)
                {
                    try
                    {
                        if ((recent[charsToKeep - 3] == currentCharacterToken[0]) &&
                            (recent[charsToKeep - 2] == currentCharacterToken[1]) &&
                            ((recent[charsToKeep - 1] == ' ') ||
                            (recent[charsToKeep - 1] == 0x0d) ||
                            (recent[charsToKeep - 1] == 0x0a)) &&
                            ((recent[charsToKeep - 4] == ' ') ||
                            (recent[charsToKeep - 4] == 0x0d) ||
                            (recent[charsToKeep - 4] == 0x0a))
                            )
                        {
                            return true;
                        }
                    }
                    catch (IndexOutOfRangeException) { return false; }
                }
                return false;
            }
        }
    }
}
