using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.ContentSystem
{
    /// <summary>
    /// An input file converter specialized to extract the non optical textual content from a .pdf (Adobe Acrobat) document
    /// and create a text file containing this content as raw text.
    /// </summary>
    public class PdfToTextConverter : FileConverter<PdfFile, TextFile>
    {
        /// <summary>
        /// Constructs a new instance which will govern the conversion of the PdfFile instance provided.
        /// The converted file will be placed in the same diretory as the original.
        /// </summary>
        /// <param name="infile">The PdfFile instance representing the .pdf document to convert.</param>
        public PdfToTextConverter(PdfFile infile)
            : base(infile) {
        }
        /// <summary>
        /// Converts the document governed by this instance from .pdf format to .txt ascii text format.
        /// </summary>
        /// <returns>An InputFile object wrapping the newly created .txt file resulting from the operation.</returns>
        public override TextFile ConvertFile() {
            var newPath = Original.PathSansExt + ".txt";
            new PDFParser().ExtractText(Original.FullPath, newPath);
            return new TextFile(newPath);
        }
        /// <summary>
        /// Asynchronously converts the document governed by this instance from .pdf format to .txt ascii text format.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task&lt;InputFile&gt; which, when awaited yields an InputFile object which wrpas the newly created .txt file.</returns>
        public override async Task<TextFile> ConvertFileAsync() {
            return await Task.Run(() => ConvertFile());
        }
        /// <summary>
        /// Gets an InputFile which acts as a wrapper around the ultimate fruit of the conversion process
        /// This additional method of accessing the new file is primarily provided to facilitate asynchronous programming
        /// and any access attempts before the conversion is complete will raise a NullReferenceException.
        /// </summary>
        public override TextFile Converted {
            get;
            protected set;
        }

        // Found this on CodeProject.com, no strings attached, it works pretty well but it is not very well written.

        /// <summary>
        /// Parses a PDF file and extracts the text from it.
        /// </summary>
        private class PDFParser
        {
            // BT = Beginning of a text object operator 
            // ET = End of a text object operator
            // Td move to the start of next line
            //  5 Ts = superscript
            // -5 Ts = subscript

            #region Fields

            #region _numberOfCharsToKeep
            /// <summary>
            /// The number of characters to keep, when extracting text.
            /// </summary>
            private const int _numberOfCharsToKeep = 15;
            #endregion

            #endregion

            #region ExtractText
            /// <summary>
            /// Extracts a text from a PDF file.
            /// </summary>
            /// <param name="inFileName">the full path to the pdf file.</param>
            /// <param name="outFileName">the output file name.</param>
            /// <returns>the extracted text</returns>
            public void ExtractText(string inFileName, string outFileName) {
                // Create a reader for the given PDF file   
                PdfReader reader = new PdfReader(inFileName);
                Console.Write("Processing: ");
                using (var outFile = new System.IO.StreamWriter(outFileName, false, System.Text.Encoding.UTF8)) {
                    for (int page = 1; page <= reader.NumberOfPages; page++) {
                        outFile.Write(ExtractTextFromPDFBytes(reader.GetPageContent(page)) + " ");
                    }
                }
            }
            #endregion

            /// <summary>
            /// This method processes an uncompressed Adobe (text) object 
            /// and extracts text.
            /// </summary>
            /// <param name="input">uncompressed</param>
            /// <returns></returns>
            private string ExtractTextFromPDFBytes(byte[] input) {
                if (input == null || input.Length == 0)
                    return "";

                //try {
                string resultString = "";

                // Flag showing if we are we currently inside a text object
                bool inTextObject = false;

                // Flag showing if the next character is key 
                // e.g. '\\' to get a '\' character or '\(' to get '('
                bool nextLiteral = false;

                // () Bracket nesting level. Text appears inside ()
                int bracketDepth = 0;

                // Keep previous chars to get extract numbers etc.:
                char[] previousCharacters = new char[_numberOfCharsToKeep];
                for (int j = 0; j < _numberOfCharsToKeep; j++)
                    previousCharacters[j] = ' ';


                for (int i = 0; i < input.Length; i++) {
                    char c = (char)input[i];

                    if (inTextObject) {
                        // Position the text
                        if (bracketDepth == 0) {
                            if (CheckToken(new string[] { "TD", "Td" }, previousCharacters, 15)) {
                                resultString += "\n\r";
                            } else {
                                if (CheckToken(new string[] { "'", "T*", "\"" }, previousCharacters, 15)) {
                                    resultString += "\n";
                                } else {
                                    if (CheckToken(new string[] { "Tj" }, previousCharacters, 15)) {
                                        resultString += " ";
                                    }
                                }
                            }
                        }

                        // End of a text object, also go to a new line.
                        if (bracketDepth == 0 &&
                            CheckToken(new string[] { "ET" }, previousCharacters, 15)) {

                            inTextObject = false;
                            resultString += " ";
                        } else {
                            // Start outputting text
                            if ((c == '(') && (bracketDepth == 0) && (!nextLiteral)) {
                                bracketDepth = 1;
                            } else {
                                // Stop outputting text
                                if ((c == ')') && (bracketDepth == 1) && (!nextLiteral)) {
                                    bracketDepth = 0;
                                } else {
                                    // Just a normal text character:
                                    if (bracketDepth == 1) {
                                        // Only print out next character no matter what. 
                                        // Do not interpret.
                                        if (c == '\\' && !nextLiteral) {
                                            nextLiteral = true;
                                        } else {
                                            if (((c >= ' ') && (c <= '~')) ||
                                                ((c >= 128) && (c < 255))) {
                                                resultString += c.ToString();
                                            }

                                            nextLiteral = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Store the recent characters for 
                    // when we have to go back for a checking
                    for (int j = 0; j < _numberOfCharsToKeep - 1; j++) {
                        previousCharacters[j] = previousCharacters[j + 1];
                    }
                    previousCharacters[_numberOfCharsToKeep - 1] = c;

                    // Start of a text object
                    if (!inTextObject && CheckToken(new string[] { "BT" }, previousCharacters, 15)) {
                        inTextObject = true;
                    }
                }
                return resultString;
                //}
                //catch (Exception) {
                //    return "";
                //}
            }



            ///// <summary>
            ///// Check if a certain 2 character currentCharacterToken just came along (e.g. BT)
            ///// </summary>
            ///// <param name="NounText">the searched currentCharacterToken</param>
            ///// <param name="recent">the recent character array</param>
            ///// <returns></returns>
            private bool CheckToken(string[] tokens, char[] recent, int _numberOfCharsToKeep) {
                foreach (string currentCharacterToken in tokens) {
                    try {
                        if ((recent[_numberOfCharsToKeep - 3] == currentCharacterToken[0]) &&
                            (recent[_numberOfCharsToKeep - 2] == currentCharacterToken[1]) &&
                            ((recent[_numberOfCharsToKeep - 1] == ' ') ||
                            (recent[_numberOfCharsToKeep - 1] == 0x0d) ||
                            (recent[_numberOfCharsToKeep - 1] == 0x0a)) &&
                            ((recent[_numberOfCharsToKeep - 4] == ' ') ||
                            (recent[_numberOfCharsToKeep - 4] == 0x0d) ||
                            (recent[_numberOfCharsToKeep - 4] == 0x0a))
                            ) {
                            return true;
                        }
                    } catch (IndexOutOfRangeException) {
                        return false;
                    }
                }
                return false;
            }

        }
    }
}
