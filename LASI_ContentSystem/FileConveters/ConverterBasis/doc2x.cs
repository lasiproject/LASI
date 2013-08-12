//*
// * Copyright (c) 2008, DIaLOGIKa
// * All rights reserved.
// *
// * Redistribution and use in source and binary forms, with or without
// * modification, are permitted provided that the following conditions are met:
// *     * Redistributions of source code must retain the above copyright
// *        notice, this list of conditions and the following disclaimer.
// *     * Redistributions in binary form must reproduce the above copyright
// *       notice, this list of conditions and the following disclaimer in the
// *       documentation and/or other materials provided with the distribution.
// *     * Neither the name of DIaLOGIKa nor the
// *       names of its contributors may be used to endorse or promote products
// *       derived from this software without specific prior written permission.
// *
// * THIS SOFTWARE IS PROVIDED BY DIaLOGIKa ''AS IS'' AND ANY
// * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// * DISCLAIMED. IN NO EVENT SHALL DIaLOGIKa BE LIABLE FOR ANY
// * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// */

//using DIaLOGIKa.b2xtranslator.DocFileFormat;
//using DIaLOGIKa.b2xtranslator.OpenXmlLib;
//using DIaLOGIKa.b2xtranslator.OpenXmlLib.WordprocessingML;
//using DIaLOGIKa.b2xtranslator.Shell;
//using DIaLOGIKa.b2xtranslator.StructuredStorage.Common;
//using DIaLOGIKa.b2xtranslator.StructuredStorage.Reader;
//using DIaLOGIKa.b2xtranslator.Tools;
//using DIaLOGIKa.b2xtranslator.WordprocessingMLMapping;
//using DIaLOGIKa.b2xtranslator.ZipUtils;
//using LASI;
//using System;
//using System.Globalization;
//using System.IO;

//namespace DIaLOGIKa.b2xtranslator.doc2x
//{
//    internal class doc2x : CommandLineTranslator
//    {
//        internal static string ToolName = "doc2x";
//        internal static string RevisionResource = "DIaLOGIKa.b2xtranslator.doc2x.revision.txt";
//        internal static string ContextMenuInputExtension = ".doc";
//        internal static string ContextMenuText = "Convert to .docx";

//        public static void ConvertFiles(string[] args) {
//            ParseArgs(args, ToolName);

//            //InitializeLogger();

//            //PrintWelcome(ToolName, RevisionResource);

//            try {
//                //copy processing file
//                ProcessingFile procFile = new ProcessingFile(InputFile);

//                //make output file name
//                if (ChoosenOutputFile == null) {
//                    if (InputFile.Contains(".")) {
//                        ChoosenOutputFile = InputFile.Remove(InputFile.LastIndexOf(".")) + ".docx";
//                    } else {
//                        ChoosenOutputFile = InputFile + ".docx";
//                    }
//                }

//                //open the reader
//                using (StructuredStorageReader reader = new StructuredStorageReader(procFile.File.FullName)) {
//                    //parse the input document
//                    WordDocument doc = new WordDocument(reader);

//                    //prepare the output document
//                    OpenXmlPackage.DocumentType outType = Converter.DetectOutputType(doc);
//                    string conformOutputFile = Converter.GetConformFilename(ChoosenOutputFile, outType);
//                    WordprocessingDocument docx = WordprocessingDocument.Create(conformOutputFile, outType);

//                    //start time
//                    DateTime start = DateTime.Now;
//                    Output.WriteLine("Converting file {0} into {1}", InputFile, conformOutputFile);

//                    //convert the document
//                    Converter.Convert(doc, docx);

//                    DateTime end = DateTime.Now;
//                    TimeSpan diff = end.Subtract(start);
//                    Output.WriteLine("Conversion of file {0} finished in {1} seconds", InputFile, diff.TotalSeconds.ToString(CultureInfo.InvariantCulture));
//                }
//            } catch (DirectoryNotFoundException ex) {
//                Output.WriteLine(ex.Message);
//                Output.WriteLine(ex.ToString());
//            } catch (FileNotFoundException ex) {
//                Output.WriteLine(ex.Message);
//                Output.WriteLine(ex.ToString());
//            } catch (ReadBytesAmountMismatchException ex) {
//                Output.WriteLine("Input file {0} is not a valid Microsoft Word 97-2003 file.", InputFile);
//                Output.WriteLine(ex.ToString());
//            } catch (MagicNumberException ex) {
//                Output.WriteLine("Input file {0} is not a valid Microsoft Word 97-2003 file.", InputFile);
//                Output.WriteLine(ex.ToString());
//            } catch (UnspportedFileVersionException ex) {
//                Output.WriteLine("File {0} has been created with a Word version older than Word 97.", InputFile);
//                Output.WriteLine(ex.ToString());
//            } catch (ByteParseException ex) {
//                Output.WriteLine("Input file {0} is not a valid Microsoft Word 97-2003 file.", InputFile);
//                Output.WriteLine(ex.ToString());
//            } catch (MappingException ex) {
//                Output.WriteLine("There was an error while converting file {0}: {1}", InputFile, ex.Message);
//                Output.WriteLine(ex.ToString());
//            } catch (ZipCreationException ex) {
//                Output.WriteLine("Could not create output file {0}.", ChoosenOutputFile);
//                //TraceLogger.Error("Perhaps the specified outputfile was a directory or contained invalid characters.");
//                Output.WriteLine(ex.ToString());
//            } catch (Exception ex) {
//                Output.WriteLine("Conversion of file {0} failed.", InputFile);
//                Output.WriteLine(ex.ToString());
//            }

//        }


//    }
//}
