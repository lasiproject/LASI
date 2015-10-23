using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Core.Heuristics;
using LASI.Utilities;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace LASI.Content
{
    /// <summary>
    /// Converts Microsoft word .doc binary files to modern Microsoft word .docx open XML files.
    /// This allows for easy extraction of the raw textual content which must be passed to the tagging module.
    /// </summary>
    public class DocToDocXConverter : FileConverter<DocFile, DocXFile>
    {

        /// <summary>
        /// Initializes a new instance of DocToDocXConverter which will handle the conversion of the given .doc document.
        /// </summary>
        /// <param name="infile">The DocFile instance representing the document to convert.</param>
        public DocToDocXConverter(DocFile infile)
            : base(infile)
        {

        }

        /// <summary>
        /// Initializes a new instance of DocToDocXConverter which will handle the conversion of the given .doc document
        /// </summary>
        /// <param name="infile">The DocFile instance representing the document to convert.</param>
        /// <param name="DocxFilesDir">The path of the directory in which to store the converted file.</param>
        public DocToDocXConverter(DocFile infile, string DocxFilesDir)
            : base(infile, DocxFilesDir)
        {
        }

        /// <summary>
        /// Converts the document held by this instance from .doc binary format to .docx open XML format
        /// The newly converted file is automatically saved in the same directory as the original
        /// </summary>
        /// <returns>An input document object representing the newly converted file
        /// Note that both the original and converted document objects can be also be accessed independently via instance properties</returns>
        public override DocXFile ConvertFile()
        {
            var process = new Process
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo
                {
                    FileName = doc2xPath,
                    Arguments = Original.FullPath,
                    WorkingDirectory = Original.Directory,
                    CreateNoWindow = true,
                    UseShellExecute = false,

                }
            };
            process.OutputDataReceived += (sender, e) => Logger.Log(e.Data);
            var stopWatch = Stopwatch.StartNew();
            process.Start();
            process.WaitForExit();
            Converted = new DocXFile(Original.PathSansExt + ".docx");
            Logger.Log("Converted {0} to {1} in {2}", Original.FileName, Converted.FileName, stopWatch.Elapsed);
            return Converted;
        }

        /// <summary>
        /// This method invokes the file conversion routine asynchronously, generally in a separate thread.
        /// Use with the await operator in an async method to retrieve the new file object and specify a continuation function to be executed when the conversion is complete.
        /// </summary>
        /// <returns>A Task&lt;InputFile&gt; object which functions as a proxy for the actual InputFile while the conversion routine is in progress.
        /// Access the internal input file encapsulated by the Task by using syntax such as : var file = await myConverter.ConvertFileAsync()
        /// </returns>
        public override async Task<DocXFile> ConvertFileAsync() => await Task.Run(() => ConvertFile());
        internal static Utilities.Configuration.IConfig Config { get; set; }

        private static string doc2xPathField;
        private static string doc2xPath =>
            doc2xPathField ?? (doc2xPathField = (Config != null ?
            Config["ResourcesDirectory"] + Config["ConvertersDirectory"] :
            ConfigurationManager.AppSettings["ResourcesDirectory"] + ConfigurationManager.AppSettings["ConvertersDirectory"]) + "doc2x.exe");




    }
}
