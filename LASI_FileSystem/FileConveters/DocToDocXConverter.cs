﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.FileSystem
{
    /// <summary>
    /// Converts Microsoft Word .doc binary files to modern Microsoft Word .docx open XML files
    /// This allows for easy extraction of the raw textual data which must be passed to the tagging module
    /// </summary>
    public class DocToDocXConverter : InputFileConverter
    {

        /// <summary>
        /// Creates a new instance of DocToDocXConverter which will handle the conversion of a given .doc document
        /// </summary>
        /// <param name="sourcePaths">The InputDocument instance representing the document to convert</param>
        public DocToDocXConverter(InputFile infile)
            : base(infile) {

        }

        public DocToDocXConverter(InputFile infile, string DocxFilesDir)
            : base(infile, DocxFilesDir) {
        }

        /// <summary>
        /// Converts the document held by this instance from .doc binary format to .docx open XML format
        /// The newly converted file is automatically saved in the same directory as the original
        /// </summary>
        /// <returns>An input document object representing the newly converted file
        /// Note that both the original and converted document objects can be also be accessed independtly via instance properties</returns>
        public override InputFile ConvertFile() {
            var proc = new Process();
            proc.EnableRaisingEvents = true;
            proc.StartInfo = new ProcessStartInfo {
                FileName = @"..\..\..\ThirdPartyComponents\FileFormatConversion\doc2x\doc2x.exe",
                Arguments = Original.Name,
                WorkingDirectory = Original.Directory,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            proc.Start();
            proc.WaitForExit();
            Converted = new DocXFile(Original.PathSansExt + ".docx");
            return Converted;
        }

        /// <summary>
        /// This method invokes the file conversion routine asynchronously, gernerally in a serparate thread.
        /// Use with the await operator in an asnyc method to retrieve the new file object and specify a continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>A Task of InputFile object which functions as a proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public override async Task<InputFile> ConvertFileAsync() {
            var result = await Task.Run(() => ConvertFile());
            return result;
        }

        /// <summary>
        /// Gets the document object which is the fruit of the conversion process
        /// This additional method of accessing the new document is primarily provided to facilitate asynchronous programming
        /// and any access attempts before the conversion is complete will raise a NullReferenceException.
        /// </summary>
        public override InputFile Converted {
            get;
            protected set;
        }




    }
}
